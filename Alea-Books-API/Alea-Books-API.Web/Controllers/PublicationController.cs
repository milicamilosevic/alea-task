using Alea_Books_API.Web.Data;
using Alea_Books_API.Web.Data.Models;
using Alea_Books_API.Web.Dtos;
using Alea_Books_API.Web.RequestModels;
using Alea_Books_API.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IConfiguration _configuration;

        public PublicationController
        (
            BlobService blobService,
            BooksDbContext context,
            IConfiguration configuration
        )
        {
            _blobService = blobService;
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetTopRated(string type)
        {
            var typeId = _context.PublicationTypes
                .Where(e => e.TypeNeme.ToLower() == type.ToLower())
                .Select(e => e.Id)
                .FirstOrDefault();

            string sqlCommand = @$"
            select top 10
	            p.Id,
	            p.Title,
	            p.Image,
	            p.Description,
	            coalesce(AVG(Cast(r.Stars as Float)), 0) as ""Rating"",
                p.PublishedOn,
	            w.FirstName,
	            w.LastName,
	            w.DateOfBirth,
	            w.Image as ""AuthorImage""
            from Publications p
            left join Ratings r on p.Id=r.PublicationId
            join Writers w on w.Id=p.AuthorId
            where TypeId=@typeId
            group by
                p.Id,
	            p.Title,
	            p.Image,
	            p.Description,
	            p.PublishedOn,
	            w.FirstName,
	            w.LastName,
	            w.DateOfBirth,
	            w.Image
                order by ""Rating"" desc, p.Title";

            var publications = new List<PublicationDto>();

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BooksDatabase")))
            {
                using (var cmd = new SqlCommand()
                {
                    CommandText = sqlCommand,
                    CommandType = CommandType.Text,
                    Connection = connection
                })
                {
                    cmd.Parameters.Add("@typeId", SqlDbType.Int).Value = typeId;
                    connection.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            publications.Add(new PublicationDto
                            {
                                Id = Convert.ToInt32(reader[0]),
                                Title = reader[1].ToString(),
                                Image = reader[2].ToString(),
                                Description = reader[3].ToString(),
                                Rating = Convert.ToDecimal(reader[4]),
                                PublishedOn = Convert.ToDateTime(reader[5]),
                                Author = new Writer
                                {
                                    FirstName = reader[6].ToString(),
                                    LastName = reader[7].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader[8]),
                                    Image = reader[9].ToString()
                                }
                            });
                        }
                    }
                }
            }
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

            var ratings = _context.Ratings
                .Where(e => e.PublicationId == publication.Id)
                .Select(e => e.Stars).ToList();

            double rating = ratings.Count > 0 ? ratings.Average() : 0;

            return Ok(new PublicationDto
            {
                Author = publication.Author,
                Rating = Convert.ToDecimal(rating),
                Id = publication.Id,
                Description = publication.Description,
                Image = publication.Image,
                PublishedOn = publication.PublishedOn,
                Title = publication.Title
            });
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
