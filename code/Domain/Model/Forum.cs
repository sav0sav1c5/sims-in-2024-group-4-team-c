using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    [Table("Forum")]
    public class Forum
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public Guest? Author { get; set; }


        public int LocationId { get; set; }
        public Location? Location { get; set; }

        public List<Comment> Comments { get; set; }

        public bool IsClosed { get; set; } = false;
        public bool IsUseful { get; set; } = false;


        private int _ownerComments;
        /// <summary>
        /// How many owners have left a comment on this forum
        /// </summary>
        public int OwnerComments
        {
            get => _ownerComments;
            set
            {
                _ownerComments = value;
                CheckAndUpdateUsefulStatus();
            }
        }

        private int _guestComments;
        /// <summary>
        /// How many guests WITH A RESERVATION on the same location have left a comment
        /// </summary>
        public int GuestComments
        {
            get => _guestComments;
            set
            {
                _guestComments = value;
                CheckAndUpdateUsefulStatus();
            }
        }

        #region UpdateForumUsefulnessStatus
        /// <summary>
        /// Minimum number of 'GuestComments' required for the forum to become useful
        /// </summary>
        private const int MinNumberOfUsefulGuestComments = 20;
        /// <summary>
        /// Minimum number of 'OwnerComments' required for the forum to become useful
        /// </summary>
        private const int MinNumberOfUsefulOwnerComments = 10;
        private void CheckAndUpdateUsefulStatus()
        {
            IsUseful = (OwnerComments > MinNumberOfUsefulOwnerComments) || (GuestComments > MinNumberOfUsefulGuestComments)
                ? true : false;
        }
        #endregion

    }
}
