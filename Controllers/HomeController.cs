using AwsTextract.api.Models;
using AwsTextract.api.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using static System.Net.Mime.MediaTypeNames;

namespace AwsTextract.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IDocumentValidationService _documentValidationService;
        public HomeController(IDocumentValidationService documentValidationService)
        {
            _documentValidationService = documentValidationService;
        }

        [HttpPost("UploadDocument")]
        public IActionResult UploadDocument([FromForm] DocumentViewModel document)
        {
            var res=_documentValidationService.ValidateDocument(document);
            if (res.IsValid)
            {
                //call the AWS S3 upload service

                return Ok(res);
            }
            
            return BadRequest(res);
        }
        

        [HttpGet("Test")]
        public IActionResult Test()
        {
            //var userid = User.Identity.IsAuthenticated
            //return Ok(new { Message = userid});
            return Ok(new { Message = "Hello world" });
           
        }

        
    }
}
