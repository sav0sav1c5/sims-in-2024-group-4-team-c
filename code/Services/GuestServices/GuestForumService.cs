using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.DTOs;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.GuestServices
{
    public class GuestForumService
    {
        #region Data
        //private readonly IAccommodationRepository _accommodationRepository = DependencyContainer.GetInstance<IAccommodationRepository>();
        //private readonly IAccommodationReservationModificationRequestRepository _accommodationReservationModificationRequestRepository = DependencyContainer.GetInstance<IAccommodationReservationModificationRequestRepository>();
        private readonly IAccommodationReservationRepository _accommodationReservationRepository = DependencyContainer.GetInstance<IAccommodationReservationRepository>();
        private readonly ICommentRepository _commentRepository = DependencyContainer.GetInstance<ICommentRepository>();
        private readonly IForumRepository _forumRepository = DependencyContainer.GetInstance<IForumRepository>();
        private readonly ICurrentDateRepository _currentDateRepository = DependencyContainer.GetInstance<ICurrentDateRepository>();
        private readonly DateTime CurrentDate;
        #endregion

        public GuestForumService()
        {
            CurrentDate = _currentDateRepository.Get()!.Date;
        }

        public List<ForumDTO> GetForumDTOsByCountryAndCity(string country, string city)
        {
            List<Forum> forums = _forumRepository.GetByCountryAndCity(country,city);
            List<ForumDTO> forumDTOs = new List<ForumDTO>();
            foreach (Forum forum in forums)
            {
                forumDTOs.Add(new ForumDTO(forum));
            }
            return forumDTOs;
        }

        public List<ForumDTO> GetForumDTOsByCountry(string country)
        {
            List<Forum> forums = _forumRepository.GetByCountry(country);
            List<ForumDTO> forumDTOs = new List<ForumDTO>();
            foreach (Forum forum in forums)
            {
                forumDTOs.Add(new ForumDTO(forum));
            }
            return forumDTOs;
        }

        public List<ForumDTO> GetForumDTOsByCity(string city)
        {
            List<Forum> forums = _forumRepository.GetByCity(city);
            List<ForumDTO> forumDTOs = new List<ForumDTO>();
            foreach (Forum forum in forums)
            {
                forumDTOs.Add(new ForumDTO(forum));
            }
            return forumDTOs;
        }

        public List<ForumDTO> GetAllForumDTOs()
        {
            List<Forum> forums = _forumRepository.GetAll();
            List<ForumDTO> forumDTOs = new List<ForumDTO>();
            foreach (Forum forum in forums)
            {
                forumDTOs.Add(new ForumDTO(forum));
            }
            return forumDTOs;
        }

        /// <summary>
        /// Returns empty string on success
        /// </summary>
        /// <param name="forumDTO">forum to be closed</param>
        /// <param name="guestDTO">the logged guest</param>
        /// <returns></returns>
        public string CloseForum(ForumDTO forumDTO, GuestDTO guestDTO)
        {
            if (guestDTO.Id != forumDTO.AuthorId)
            {
                return "You are not the owner";
            }
            if (forumDTO.IsClosed)
            {
                return "Forum is already closed";
            }

            forumDTO.IsClosed = true;
            _forumRepository.Update(forumDTO._forum);

            return "";
        }

        public string PostForumComment(ForumDTO forumDTO, CommentDTO commentDTO, GuestDTO LoggedGuest)
        {
            if (forumDTO._forum.IsClosed)
            {
                return "Forum is closed!";
            }
            commentDTO._comment.ForumId = forumDTO.Id;
            commentDTO._comment.UserId = LoggedGuest.Id;
            commentDTO._comment.User = LoggedGuest._guest;
            commentDTO._comment.PublishedDate = CurrentDate;
            commentDTO._comment.IsOwnerComment = false;
            commentDTO._comment.IsUseful = _accommodationReservationRepository.GetAllByGuestIdAndLocationId(LoggedGuest.Id, forumDTO.LocationId).Count > 0 ? true : false;
            if (commentDTO._comment.IsUseful)
            {
                forumDTO._forum.GuestComments++; //automaticaly updates the forums usefulness status 
                //forumDTO._forum.CheckAndUpdateUsefulStatus(); 
            }
            _commentRepository.Save(commentDTO._comment);
            _forumRepository.Update(forumDTO._forum);
            return "";
        }

        public void SaveForum(Forum forum)
        {
            _forumRepository.Save(forum);
        }

    }
}
