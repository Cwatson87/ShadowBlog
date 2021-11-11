using ShadowBlog.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShadowBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogPostId { get; set; }


        // We need to record the Id of the registred and logged in user who is creating these comments

        public string BlogUserId { get; set; }
        public string ModeratorId { get; set; }

        //Descriptive proprs
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CommentBody { get; set; }

        //The Moderator related proprties
        public DateTime? Deleted { get; set; }
        public DateTime? Moderated { get; set; }
        public string ModeratedBody { get; set; }
        public ModType ModerationType { get; set; }

        //Navigational props
        public virtual BlogPost BlogPost { get; set; }
        public virtual BlogUser BlogUser { get; set; }
        public virtual BlogUser Moderator { get; set; }

    }
}
