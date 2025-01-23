using BookingApp.DependencyInjection;
using BookingApp.Domain.Model;
using BookingApp.Repository;
using BookingApp.Resources.DBAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.GuideServices
{
    public class CheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository = DependencyContainer.GetInstance<ICheckpointRepository>();

        public CheckpointService() {}

        public List<Checkpoint> GetAllCheckpoints()
        {
            return _checkpointRepository.GetAll();
        }

        public Checkpoint? GetCheckpointById(int id)
        {
            return _checkpointRepository.GetById(id);
        }

        public List<Checkpoint> GetCheckpointsByTourId(int tourId)
        {
            return _checkpointRepository.GetByTourId(tourId);
        }

        public List<Checkpoint> GetCheckpointsByLocationId(int locationId)
        {
            return _checkpointRepository.GetByLocationId(locationId);
        }

        public Checkpoint SaveCheckpoint(Checkpoint checkpoint)
        {
            return _checkpointRepository.Save(checkpoint);
        }

        public void UpdateCheckpoint(Checkpoint checkpoint)
        {
            _checkpointRepository.Update(checkpoint);
        }
        public void DeleteCheckpoint(Checkpoint checkpoint)
        {
            _checkpointRepository.Delete(checkpoint);
        }
    }
}
