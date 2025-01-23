using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("ForumAlert")]
    public class ForumAlert
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public int ForumId { get; set; }

        public int OwnerId { get; set; }

        public bool isOpened { get; set; } = false;

        public ForumAlert()
        { }
        public ForumAlert(int forumId, int ownerId, bool isOpened)
        {
            ForumId = forumId;
            OwnerId = ownerId;
            this.isOpened = isOpened;
        }

        public ForumAlert(int id, int forumId, int ownerId, bool isOpened)
        {
            Id = id;
            ForumId = forumId;
            OwnerId = ownerId;
            this.isOpened = isOpened;
        }
    }
}
