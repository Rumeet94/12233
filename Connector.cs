using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.AudioBypassService.Extensions;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace WindowsFormsApp1
{
    public class ConnectorVK
    {
        public ConnectorVK(string login, string password)
        {
            this.login = login;
            this.password = password;

            api = Connect();
        }

        private const long AppID = 7111965;

        private VkApi api = null;
        private IDictionary<long, string> posts = new Dictionary<long, string>();

        private string login,
                       password;

        private VkApi Connect()
        {
            try
            {
                var services = new ServiceCollection();
                services.AddAudioBypass();

                var api = new VkApi(services);
                api.Authorize(new ApiAuthParams{
                                                 ApplicationId = AppID,
                                                 Login = login,
                                                 Password = password,
                                                 Settings = Settings.All
                                               }
                             );

                return api;
            }
            catch (VkApiAuthorizationException e)
            {
                return null;
            }
            catch (VkApiException e)
            {
                return null;
            }
        }

        public void GetWallPosts()
        {
            var wallPosts = api.Wall.Get(new WallGetParams{
                                                            OwnerId = -110242131/*185885412*/,
                                                            Domain = "evgengorb",
                                                            Offset = 0,
                                                            Count = 99,
                                                          }
                                        );
            foreach (var x in wallPosts.WallPosts)
            {
                posts.Add((long)x.Id, x.Text);
            }
        }

        public void SendMessage()
        {
            int randomId = new Random().Next(1, int.MaxValue);

            api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
            {
                RandomId = randomId,
                UserId = 147130935,//223398928,
                Domain = "evgengorb",
                Message = "message"
            });
        }



        
        
    }
}
