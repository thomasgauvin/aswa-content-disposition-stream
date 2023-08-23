// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileDownloadFunction.cs" company="Black Marble">
//   Copyright (c) Black Marble Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AzureStaticSitesContentDispositionHeader
{
    public static class FileDownloadFunction
    {
        [FunctionName(nameof(FileDownloadFunction))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            if (!req.Query.TryGetValue("filename", out Microsoft.Extensions.Primitives.StringValues filename))
            {
                return new BadRequestObjectResult(new { error = "missing filename" });
            }

            var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello world!"));
            return new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = filename
            };
        }
    }
}
