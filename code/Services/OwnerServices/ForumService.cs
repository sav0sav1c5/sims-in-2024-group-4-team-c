using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.OwnerServices
{
    public class ForumService
    {
        private readonly IForumRepository _forumRepository = DependencyContainer.GetInstance<IForumRepository>();
        private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        private readonly IForumAlertRepository _forumAlertRepository = DependencyContainer.GetInstance<IForumAlertRepository>();
        private readonly ICommentRepository _commentRepository = DependencyContainer.GetInstance<ICommentRepository>();
        public void alertOwnerNewForum(int forumId)
        {
            List<int> ownersId = new List<int>();
            var forum = _forumRepository.GetById(forumId);

            if (forum != null)
            {
                var accommodations = _accommodationRepository.GetByLocationId(forum.LocationId);
                foreach (var accommodation in accommodations)
                {
                    if (!ownersId.Contains(accommodation.OwnerId))
                    {
                        ownersId.Add(accommodation.OwnerId);
                    }
                }
            }

            foreach(var ownerId in ownersId)
            {
                ForumAlert forumAlert = new ForumAlert(forumId,ownerId,false);
                _forumAlertRepository.Save(forumAlert);
            }
            
        }

        public List<Forum> GetAlerts(int ownerId)
        {
            var forumAlerts = _forumAlertRepository.GetAll().Where(forumAlert => forumAlert.OwnerId == ownerId);
            List<Forum> forums = new List<Forum>();

            foreach(var forumAlert in forumAlerts)
            {
                Forum forum = _forumRepository.GetById(forumAlert.ForumId);
                forums.Add(forum);
            }

            return forums;

        }

        public void OpenAlert(int forumAlertId)
        {

        }


        public bool isVeryUsefulOwner(int forumId)
        {
            var comments = _commentRepository.GetAll().Where(comment => comment.ForumId == forumId);
            int ownerComments = 0;

            foreach(var comment in comments)
            {
                if(comment.IsOwnerComment)
                {
                    ownerComments++;
                }
            }

            return (ownerComments >= 10);
        }

        public bool isVeryUsefulGuest(int forumId)
        {
            var comments = _commentRepository.GetAll().Where(comment => comment.ForumId == forumId);
            int guestComments = 0;

            foreach (var comment in comments)
            {
                if (!comment.IsOwnerComment)
                {
                    guestComments++;
                }
            }

            return (guestComments >= 20);
        }

        public void ReportComment(int commentId)
        {
            Comment comment = _commentRepository.GetAll().Where(comment => comment.Id == commentId).FirstOrDefault();

            if (comment != null)
            {
                comment.NumOfReports = comment.NumOfReports + 1;
            }

        }

        public int GetReports(int commentId)
        {
            Comment comment = _commentRepository.GetAll().Where(comment => comment.Id == commentId).FirstOrDefault();

            return comment.NumOfReports;
        }



    }
}
