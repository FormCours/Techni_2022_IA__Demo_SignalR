using Microsoft.AspNetCore.Mvc;

namespace Demo_ASP__SignalR.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
