using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApp.Infrastructure
{
    public  static class HttpStatusCodeExtentions
    {
        public static ActionResult Result(this HttpStatusCode statusCode) =>
            new StatusCodeResult((int)statusCode);
    }
}
