using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using BookingApp.Resources.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    internal class CommentRepository : ICommentRepository
    {
        private List<Comment> _comments;
        private readonly IDataHandler<Comment> _commentDataHandler;

        public CommentRepository()
        {
            _commentDataHandler = new CommentDataHandler();
            _comments = _commentDataHandler.GetAll().ToList();
        }
        public void Delete(Comment entity)
        {
            _commentDataHandler.Delete(entity);
            _comments.Remove(entity);
            
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAll()
        {
            return _comments;
        }

        public Comment? GetById(int id)
        {
            return _comments.FirstOrDefault(comment => comment.Id == id);
        }

        public void Save(Comment entity)
        {
            if (entity == null) return;
            _commentDataHandler.SaveOneEntity(entity);
            _comments.Add(entity);

        }

        public void Update(Comment entity)
        {
            throw new NotImplementedException();
        }
    }
}
