using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("Comment")]
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int ForumId { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime PublishedDate { get; set; }

        public bool IsOwnerComment { get; set; }
        public string CommentText { get; set; } = string.Empty;

        public int NumOfReports { get; set; } = 0;

        public bool IsUseful { get; set; } = false;
        public Comment()
        {
        }
        public Comment(int forumId, int userId)
        {
            ForumId = forumId;
            UserId = userId;
        }
    }
}
