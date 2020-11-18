using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
// using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MB_Model;

namespace BLL
{
    public class BLL
    {
        #region Init 
        public string URL { get; set; }

        public string HELP_URL { get; set; }
        public HttpClient client { get; set; }
        public string ServerMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public BLL()
        {

            URL = @"http://www.jerrybhill.com/Mystery/Service1.svc/";
            HELP_URL = @"http://www.jerrybhill.com/Mystery/Service1.svc/help";

            client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); client.Timeout = new TimeSpan(0, 5, 0);

        }
        #endregion
        public async Task<List<DTO_RecipeList>> GetRecipes(DTO_UserID token)
        {
            var list = await GetDataList<DTO_UserID, DTO_RecipeList >(token, "WS_GetRecipeList");

            return list;
        }
   
        public async Task<DTO_UserInfo> Login(DTO_Login token)
        {
            var prod = await GetData<DTO_Login, DTO_UserInfo>(token, "WS_LoginValidation");
            return prod;
        }
      
        #region Main Method
        public async Task<List<T1>> GetDataList<T, T1>(T token, string remoteMethod)
        {
            List<T1> retData = new List<T1>();

            int retryCount = 1;
            while (retryCount <= 5)
            {
                try
                {
                    var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                    CancellationToken cancellationToken = CancellationToken.None;

                    HttpResponseMessage response = await client.PostAsJsonAsync
                        (
                        string.Format(@"{0}{1}", URL, remoteMethod), token
                        );
                    StatusCode = response.StatusCode;

                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();

                    retData = (List<T1>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(List<T1>));

                    //retData = wrapList.Data.ToList();
                    ServerMessage = string.Empty;
                    break;
                }
                catch (HttpRequestException hre)
                {
                    ServerMessage = hre.Message;

                    string im = string.Empty;
                    if (hre.InnerException != null)
                        im = hre.InnerException.Message;

                    Debug.WriteLine(string.Format("Exception: {0} {1} {2}", remoteMethod, ServerMessage, im));

                    retryCount++;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return retData;
        }

        public async Task<T1> GetData<T, T1>(T token, string remoteMethod) where T1 : new()
        {
            T1 retData = new T1();

            try
            {
                var settings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                HttpResponseMessage response = await client.PostAsJsonAsync(string.Format(@"{0}{1}", URL, remoteMethod), token);
                StatusCode = response.StatusCode;

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                retData = (T1)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(T1));

            }
            catch (HttpRequestException hre)
            {
                Debug.WriteLine(hre.Message);
            }

            return retData;


        }

        #endregion


    }

}
