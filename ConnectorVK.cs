using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Wake_up
{
    public class ConnectorVK
    {
        public ConnectorVK()
        {
            api = new VkApi(new ServiceCollection().AddAudioBypass());
        }

        private const long AppID = 7111965;
        private const int GroupID = 185885412;
        private const string Domain = "evgengorb";

        private readonly VkApi api;

        public bool Connect(string login, string password)
        {
            try
            {
                api.Authorize(new ApiAuthParams
                {
                    ApplicationId = AppID,
                    Login = login,
                    Password = password,
                    Settings = Settings.All
                });

                return true;
            }
            catch (VkApiAuthorizationException e)
            {
                Log.Write(e);
                return false;
            }
            catch (VkApiException e)
            {
                Log.Write(e);
                return false;
            }
        }

        public Dictionary<long, string> GetWallPosts()
        {
            var posts = new Dictionary<long, string>();
            try
            {
                var wallPosts = api.Wall.Get(new WallGetParams
                {
                    OwnerId = -GroupID,
                    Domain = Domain,
                    Offset = 0,
                    Count = 99,
                });

                foreach (var x in wallPosts.WallPosts)
                {
                    posts.Add((long)x.Id, x.Text);
                }

                return posts;
            }
            catch(VkApiException e)
            {
                Log.Write(e);
                return null;
            }
        }

        public void SendMessage(string message)
        {
            int randomId = new Random().Next(1, int.MaxValue);
            try
            {
                api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                {
                    RandomId = randomId,
                    GroupId = GroupID,
                    Domain = Domain,
                    Message = "Today: " + message.Split(' ')[1]
                });
            }
            catch(VkApiException e)
            {
                Log.Write(e);
            }
        }
    }
}
