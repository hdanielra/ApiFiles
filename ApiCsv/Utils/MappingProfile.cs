using AutoMapper;
using Entities.Models;
using Entities.Requests;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ApiCsv.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            _ = CreateMap<SalesFile, Sales>()
                .ForMember(d => d.DealNumber, o => o.MapFrom(s => s.DealNumber))
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s.CustomerName))
                .ForMember(d => d.DealershipName, o => o.MapFrom(s => s.DealershipName))
                .ForMember(d => d.Vehicle, o => o.MapFrom(s => s.Vehicle))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price))
                .ForMember(d => d.Date, o =>
                    o.MapFrom(s => DateTime.ParseExact(s.Date, "M/d/yyyy", new CultureInfo("en-US"), DateTimeStyles.None)));

            _ = CreateMap<string[], SalesFile>()
                .ForMember(d => d.DealNumber, o => o.MapFrom(s => s[0]))
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s[1]))
                .ForMember(d => d.DealershipName, o => o.MapFrom(s => s[2]))
                .ForMember(d => d.Vehicle, o => o.MapFrom(s => s[3]))
                .ForMember(d => d.Price, o => o.MapFrom(s => s[4]))
                .ForMember(d => d.Date, o => o.MapFrom(s => s[5]));

            _ = CreateMap<List<string>, Sales>()
                .ForMember(d => d.DealNumber, o => o.MapFrom(s => s[0]))
                .ForMember(d => d.CustomerName, o => o.MapFrom(s => s[1]))
                .ForMember(d => d.DealershipName, o => o.MapFrom(s => s[2]))
                .ForMember(d => d.Vehicle, o => o.MapFrom(s => s[3]))
                .ForMember(d => d.Price, o => o.MapFrom(s => Convert.ToDouble(s[4])))
                .ForMember(d => d.Date, o =>
                    o.MapFrom(s => DateTime.ParseExact(s[5], "M/d/yyyy", new CultureInfo("en-US"), DateTimeStyles.None)));

        }
    }
}
