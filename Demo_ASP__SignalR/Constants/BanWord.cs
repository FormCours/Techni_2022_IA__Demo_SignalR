namespace Demo_ASP__SignalR.Constants
{
    public static class BanWord
    {

        private static List<string> _Data = new List<string>
        {
            "Demo"
        };

        public static IEnumerable<string> Data => _Data.AsReadOnly();
    }
}
