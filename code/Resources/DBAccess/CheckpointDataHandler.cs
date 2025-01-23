using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Resources.DBAccess
{
    public class CheckpointDataHandler : IDataHandler<Checkpoint>
    {
        private DataContext dataContext = new DataContext();

        public Checkpoint UpdateEntity(Checkpoint entity)
        {
            throw new NotImplementedException();
        }

        void IDataHandler<Checkpoint>.Delete(Checkpoint entity)
        {
            var existingCheckpoint = dataContext.Checkpoints.FirstOrDefault(checkpoint => checkpoint.Id == entity.Id);

            if (existingCheckpoint != null)
            {
                dataContext.Checkpoints.Remove(existingCheckpoint);
                dataContext.SaveChanges();
            }
        }


        void IDataHandler<Checkpoint>.DeleteById(int entityId)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Checkpoint> IDataHandler<Checkpoint>.GetAll()
        {
            return dataContext.Checkpoints.ToList();
        }

        void IDataHandler<Checkpoint>.Save(IEnumerable<Checkpoint> entities)
        {
            throw new NotImplementedException();
        }

        Checkpoint IDataHandler<Checkpoint>.SaveOneEntity(Checkpoint checkpoint)
        {
            dataContext.Checkpoints.Add(checkpoint);
            dataContext.SaveChanges();
            return checkpoint;
        }

        void IDataHandler<Checkpoint>.Update(Checkpoint entity)
        {
            var existringCheckpoint = dataContext.Checkpoints.FirstOrDefault(checkpoint => checkpoint.Id == entity.Id);

            if (existringCheckpoint != null)
            {
                existringCheckpoint.IsActive = entity.IsActive;

                dataContext.SaveChanges();
            }
        }
    }
}
