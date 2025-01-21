using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniTabler.Common.Models;

namespace UniTabler.Common.Interfaces.RideInformation
{
    public interface IRideInformationRepository
    {
        Task<List<TripRecordModel>> GetTopLongestFaresByDistance();
        Task<List<TripRecordModel>> GetTopLongestFaresByTime();
        Task<List<TripRecordModel>> GetRideByPickUpId(int pickUpId);
        Task<TripRecordModel> GetPickUpLocationByAverageTipAmount();
    }
}
