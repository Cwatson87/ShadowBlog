using System.ComponentModel.DataAnnotations;

namespace ShadowBlog.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public int BlogPostId { get; set; }



        //Foreign Id, needed for multi-tenant apps
        //public string AuthorId {get; set;}

        [Required] 
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]

        public string Text { get; set; }

        //Nav props
        public virtual BlogPost BlogPost { get; set; }

    }
}
