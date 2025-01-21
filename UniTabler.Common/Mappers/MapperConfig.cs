using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UniTabler.Common.DTOs;
using UniTabler.Common.Models;

namespace UniTabler.Common.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<TripRecordModel, TripRecordDTO>().ReverseMap();
            CreateMap<TripRecordModel, BestLocationByAverageTipModel>();
        }
    }
}
