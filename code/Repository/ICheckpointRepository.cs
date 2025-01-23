using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public interface ICheckpointRepository : IBaseRepository<Checkpoint>
    {
        public new Checkpoint? GetById(int id);
        public List<Checkpoint>? GetByTourId(int tourId);
        public List<Checkpoint>? GetByLocationId(int locationId);
        public Checkpoint? Save(Checkpoint checkPoint);
        public void Update(Checkpoint checkPoint);
        public void Delete(Checkpoint checkpoint);

    }
}
