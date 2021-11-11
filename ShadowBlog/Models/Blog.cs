using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Models
{

    //This repesents a category of indiviual Posts or BlogPosts
    //Security in MVC <---Blog
    // Role based security <-- posts
    // Front end security <--Posts
    // Tag
    // Comment1 <---Comments
    // Comment2 <--Comments

    public class Blog     //Class creates type
    {
        //Non-descriptive administrative property

        public int Id { get; set; }    //In every model(?)

        //Security in MVC
        [Required]
        [StringLength(100, ErrorMessage = "The {0} property must be at least {2} character and at most {1} characters.", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} property must be at least {2} character and at most {1} characters.", MinimumLength = 5)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }


        //Add a navigational property to referece all of my children
        public ICollection<BlogPost> BlogPosts = new HashSet<BlogPost>();

    }
}
