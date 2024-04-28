using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Scan2Go.Api.BaseClasses;
using Scan2Go.Enums;
using System.Net;
using Newtonsoft.Json;
using Scan2Go.Mapper.Managers;
using Utility.Core;
using Utility.Extensions;

namespace Scan2Go.Api.Controllers
{
   [Route("[controller]")]
    [ApiController]
    public class AppController : BaseController
    {

        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("UploadIdDocument")]
        public async Task<IActionResult> UploadIdDocument()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            string logMessge = $"UploadIdDocument was called at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} ";
            logMessge += $"From IP {ipAddress}";

            _logger.LogInformation(logMessge);

            var httpRequest = HttpContext.Request;
            OperationResult operationResult = new OperationResult();

            if (httpRequest.Form.Files.Count <= 0)
            {
                operationResult.Message = "No files were found!";
                operationResult.State = false;
                return StatusCode(HttpStatusCode.ExpectationFailed.AsInt(), operationResult);
            }

            //TODO lets make a control that only one file could be sent.

            byte[] documentData = null;

            

            foreach (IFormFile file in httpRequest.Form.Files)
            {
                _logger.LogInformation("Entered foreach");
                var postedFile = file;

                if (postedFile.Length == 0)
                {
                    _logger.LogInformation("Broke out of foreach since posted file Length is 0 ");
                    continue;
                }
                
                using (BinaryReader reader = new BinaryReader(postedFile.OpenReadStream()))
                {
                    documentData = reader.ReadBytes(postedFile.Length.AsInt());
                }

                _logger.LogInformation($"Byte[] size is: {documentData.Length.ToString()}");
            }

            operationResult = await new AppManager(this.CurrentUser).UploadIdDocument(documentData);

            var jsonString =JsonConvert.SerializeObject(operationResult.ResultObject);
            _logger.LogInformation($"Returning response as: {jsonString}");

            return this.ReturnOperationResult(operationResult);
        }
    }
}
