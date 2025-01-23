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
    public class CheckPointRepository : ICheckpointRepository
    {
        private List<Checkpoint> checkpoints;
        private readonly IDataHandler<Checkpoint> checkpointDataHandler;

        public CheckPointRepository()
        {
            checkpointDataHandler = new CheckpointDataHandler();
            checkpoints = checkpointDataHandler.GetAll().ToList();
        }
        public List<Checkpoint> GetAll()
        {
            return checkpoints;
        }

        public Checkpoint? GetById(int id)
        {
            return checkpoints.FirstOrDefault(checkPoint => checkPoint.Id == id);
        }

        public List<Checkpoint> GetByTourId(int tourId)
        {
            List<Checkpoint> filteredCheckPoints = new List<Checkpoint>();
            foreach (Checkpoint checkPoint in checkpoints)
            {
                if (checkPoint.TourId == tourId)
                {
                    filteredCheckPoints.Add(checkPoint);
                }
            }
            return filteredCheckPoints;
        }
        public List<Checkpoint> GetByLocationId(int locationId)
        {
            List<Checkpoint> filteredCheckPoints = new List<Checkpoint>();
            foreach (Checkpoint checkPoint in checkpoints)
            {
                if (checkPoint.LocationId == locationId)
                {
                    filteredCheckPoints.Add(checkPoint);
                }
            }
            return filteredCheckPoints;
        }

        public Checkpoint Save(Checkpoint checkPoint)
        {

            checkpoints.Add(checkPoint);
            return checkpointDataHandler.SaveOneEntity(checkPoint);
        }

        public void Update(Checkpoint checkPoint)
        {
            checkpointDataHandler.Update(checkPoint);
        }

        void IBaseRepository<Checkpoint>.Save(Checkpoint entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Checkpoint checkpoint)
        {
            checkpoints.Remove(checkpoint);
            checkpointDataHandler.Delete(checkpoint);
        }
    }
}
