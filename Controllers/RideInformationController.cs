using Microsoft.AspNetCore.Mvc;
using UniTabler.Common.Interfaces.RideInformation;
using UniTabler.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace UniTabler.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RideInformationController : ControllerBase
    {
        private readonly IRideInformationManager _rideInformationManager;
        private readonly IMapper _mapper;

        public RideInformationController(IRideInformationManager rideInformationManager, IMapper mapper)
        {
            _rideInformationManager = rideInformationManager;
            _mapper = mapper;
        }

        [HttpGet("highest-average-tip")]
        public async Task<IActionResult> GetPickUpLocationByAverageTipAmount()
        {
            var result = _mapper.Map<BestLocationByAverageTipModel>(await _rideInformationManager.GetPickUpLocationByAverageTipAmount());
            if (result == null)
            {
                return NotFound("No data found.");
            }
            return Ok(result);
        }

        [HttpGet("rides-by-pickup/{pickUpId}")]
        public async Task<IActionResult> GetRideByPickUpId(int pickUpId)
        {
            var result = await _rideInformationManager.GetRideByPickUpId(pickUpId);
            if (result == null || result.Count == 0)
            {
                return NotFound($"No trips were found for PULocationId {pickUpId}.");
            }
            return Ok(result);
        }

        [HttpGet("top-longest-distance")]
        public async Task<IActionResult> GetTopLongestFaresByDistance()
        {
            var result = await _rideInformationManager.GetTopLongestFaresByDistance();
            if (result == null || result.Count == 0)
            {
                return NotFound("No data found.");
            }
            return Ok(result);
        }

        [HttpGet("top-longest-duration")]
        public async Task<IActionResult> GetTopLongestFaresByTime()
        {
            var result = await _rideInformationManager.GetTopLongestFaresByTime();
            if (result == null || result.Count == 0)
            {
                return NotFound("No data found.");
            }
            return Ok(result);
        }
    }
}