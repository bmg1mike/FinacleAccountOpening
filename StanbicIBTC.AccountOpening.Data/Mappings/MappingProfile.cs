using AutoMapper;
using StanbicIBTC.AccountOpening.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanbicIBTC.AccountOpening.Data.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BulkAccountRequest, BulkAccountDto>().ReverseMap();
            CreateMap<RMIdentity, RmIdentityDto>().ReverseMap();
        }
    }
}
