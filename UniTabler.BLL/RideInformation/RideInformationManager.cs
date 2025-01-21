using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniTabler.Common.Interfaces.RideInformation;
using UniTabler.Common.Models;

namespace UniTabler.BLL.RideInformation
{
    public class RideInformationManager : IRideInformationManager
    {
        private readonly IRideInformationRepository _rideInformationRepository;

        public RideInformationManager(IRideInformationRepository rideInformationRepository)
        {
            _rideInformationRepository = rideInformationRepository;
        }

        public async Task<TripRecordModel> GetPickUpLocationByAverageTipAmount()
        {
            return await _rideInformationRepository.GetPickUpLocationByAverageTipAmount();
        }

        public async Task<List<TripRecordModel>> GetRideByPickUpId(int pickUpId)
        {
            return await _rideInformationRepository.GetRideByPickUpId(pickUpId);
        }

        public async Task<List<TripRecordModel>> GetTopLongestFaresByDistance()
        {
            return await _rideInformationRepository.GetTopLongestFaresByDistance();
        }

        public async Task<List<TripRecordModel>> GetTopLongestFaresByTime()
        {
            return await _rideInformationRepository.GetTopLongestFaresByTime();
        }
    }
}
