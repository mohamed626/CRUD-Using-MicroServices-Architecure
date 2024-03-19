using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
        {
			try
			{
                HttpClient client = httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage requestMessage = new();


                switch (requestDTO.ApiType)
                {
                    case ApiType.POST:
                        requestMessage.Method=HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        requestMessage.Method=HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        requestMessage.Method=HttpMethod.Delete;
                        break;
                    default:
                        requestMessage.Method=HttpMethod.Get;
                        break;
                }

                requestMessage.Headers.Add("Accept", "application/json");

                requestMessage.RequestUri =new Uri(requestDTO.Url);

                if(requestDTO.Data != null)
                {
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage httpResponse = new();

                httpResponse = await client.SendAsync(requestMessage);

                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new(){Message = "Not Found"};
                    case HttpStatusCode.Unauthorized:
                        return new() { Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { Message = "Internal Server Error" };
                    default:
                        var apicontent = await httpResponse.Content.ReadAsStringAsync();
                        var responseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apicontent);
                        return responseDTO;
                }


			}
			catch (Exception ex)
			{

				return new() { Message = ex.Message };
			}
        }
    }
}
