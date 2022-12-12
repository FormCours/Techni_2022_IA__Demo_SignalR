using Demo_ASP__SignalR.Constants;
using Microsoft.AspNetCore.SignalR;
using System.Security.Cryptography;

namespace Demo_ASP__SignalR.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string pseudo, string message)
        // Pour l'invoquer la méthode coté JS → "sendMessage"
        {
            if(BanWord.Data.Any(word => message.Contains(word, StringComparison.OrdinalIgnoreCase)))
            {
                await Clients.Caller.SendAsync("receiveMessage", pseudo, "Message interdit");
                return;
            }

            // Le serveur peut déclancher des méthodes du coté du client
            await Clients.All.SendAsync("receiveMessage", pseudo, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("receiveWelcome", "Bienvenue");
            await Clients.Others.SendAsync("receiveWelcome", "Une personne s'est connectée");
        }



        public async Task JoinGroup(string groupName) {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);


            await Clients.Groups(groupName).SendAsync(
                "receiveWelcome",
                $"Une personne à rejoint le groupe {groupName}"
            );
        }

        public async Task LeaveGroup(string groupName) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Groups(groupName).SendAsync(
                "receiveWelcome",
                $"Une personne à quitter le groupe {groupName}"
            );
        }

        public async Task SendMessageGroup(string groupName, string pseudo, string message) {
            string groupPseudo = $"[{groupName.ToUpper()}] {pseudo}";
            await Clients.Group(groupName).SendAsync("receiveMessage", groupPseudo, message);
        }
    }
}
