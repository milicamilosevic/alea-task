using Alea_Books_API.Web.Data;
using Alea_Books_API.Web.RequestModels;
using Alea_Books_API.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PublicationController : ControllerBase
    {
        private readonly BlobService _blobService;

        public PublicationController(BlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetPublications()
        {
            //return Ok(_publicationData.GetPublications());
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddPublication([FromForm] AddPublication_RequestModel request)
        {
            var imageName = request.Image.FileName;
            var imageStream = request.Image.OpenReadStream();
            await _blobService.UploadImage(imageStream, imageName);
            return Ok();
        }

    }
}
