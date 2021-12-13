using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShadowBlog.Data;
using ShadowBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Controllers.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogSvcController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BlogSvcController(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Returns the most recent X number of BlogPosts
        /// </summary>
        /// <remarks> Cameron 12/13/2021.</remarks>
        /// <param name="num">The number of BlogPosts you want </param>
        /// <returns>List of type BlogPost</returns>

        //Here is the GetTopXPosts Action or Endpoint
        [HttpGet("/GetTopXPosts/{num}")]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetTopXPosts(int num)
        {
            //Return the most recent num blogposts
            return await _context.BlogPosts.OrderByDescending(p => p.Created).Take(num).ToListAsync();
        }
    }
}
