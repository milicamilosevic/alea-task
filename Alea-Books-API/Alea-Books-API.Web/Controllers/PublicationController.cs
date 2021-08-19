using Alea_Books_API.Web.Data;
using Alea_Books_API.Web.RequestModels;
using Alea_Books_API.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Alea_Books_API.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PublicationController : ControllerBase
    {
        private readonly BlobService _blobService;
        private readonly BooksDbContext _context;

        public PublicationController(BlobService blobService, BooksDbContext context)
        {
            _blobService = blobService;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTopRated(string type, int pageSize)
        {
            var typeId = _context.PublicationTypes
                .Where(e => e.TypeNeme.ToLower() == type.ToLower())
                .Select(e => e.Id)
                .FirstOrDefault();

            var publications = _context.Publications
                .Include(e => e.Author)
                .Where(e => e.Type.Id == typeId)
                .OrderBy(e => e.Title)
                .Take(pageSize)
                .ToList();

            return Ok(publications);
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            var publication = _context.Publications
                .Include(e => e.Author)
                .Where(e => e.Id == id)
                .OrderBy(e => e.Title)
                .FirstOrDefault();

            return Ok(publication);
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
