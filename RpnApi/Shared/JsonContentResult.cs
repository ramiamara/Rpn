using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace RpnApi.Shared
{
    public sealed class JsonContentResult : ContentResult
    {
        public JsonContentResult()
        {
            ContentType = "application/json";
        }

        public static JsonContentResult Success()
        {
            return new JsonContentResult
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public static JsonContentResult Success(object response)
        {
            return new JsonContentResult
            {
                StatusCode = (int)HttpStatusCode.OK,
                Content = JsonSerializer.SerializeObject(response)
            };
        }

        public static JsonContentResult Error(object response)
        {
            return new JsonContentResult
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Content = JsonSerializer.SerializeObject(response)
            };
        }
    }
}
