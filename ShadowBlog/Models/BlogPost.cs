using Microsoft.AspNetCore.Http;
using ShadowBlog.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Models
{
    public class BlogPost
    {
        //The Primary Key (PK)
        public int Id { get; set; }

        //The Forgein Key (FK)
        public int BlogId { get; set; }

       [Required]
       [StringLength(50, ErrorMessage = "The {0} property must be at least {2} character and at most {1} characters.", MinimumLength = 4)]
        public string Title { get; set; }

        //A property to get the user interested without forcing them to read the entire post...
        [Required]
        [StringLength(200, ErrorMessage = "The {0} property must be at least {2} character and at most {1} characters.", MinimumLength = 4)]
        public string Abstract { get; set; }

        [Required]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Created Date")]
        public DateTime Created { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Updated Date")]
        public DateTime? Updated { get; set; }  // ? Nullable property


        //Add a property of type bool named ProductionReady
       [Required]
       [Display(Name ="Ready Status")]
        public ReadyState ReadyStatus { get; set; }

        //This will basically be the title run through a formatter
        //Role Base Security - role-based-security
        public string Slug { get; set; }

        //This represents the byte data no the physical file
        public byte[] ImageData { get; set; }

        public string ImageType { get; set; }

        //This property represents a physical file chosen by the user
        [NotMapped]
        public IFormFile Image { get; set; }


       //Naviagational Properties
       //Parent
        public virtual Blog Blog { get; set; }

        //Children
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    }
}
