using System;

namespace Wake_up
{
    class Handler
    {
        private readonly ConnectorVK connector = new ConnectorVK();

        public bool Connect(string login, string password)
        {
           return connector.Connect(login, password);
        }

        public void SendMessage()
        {
            var posts = connector.GetWallPosts();

            if (posts == null)
            {
                return;
            }

            foreach (var x in posts)
            {
                if (CheckDate(x.Value))
                {
                    connector.SendMessage(x.Value);
                }
            }
        }

        private bool CheckDate(string text)
        {
            string curDate = DateTime.Now.ToString("d");

            return text.Contains(curDate);
        }
    }  
}
