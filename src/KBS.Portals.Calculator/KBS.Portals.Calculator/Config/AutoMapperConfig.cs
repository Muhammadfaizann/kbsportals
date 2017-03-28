using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KBS.Portals.Calculator.Logic.Models;
using KBS.Portals.Calculator.Models;

namespace KBS.Portals.Calculator.Config
{
    public static class AutoMapperConfig
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CalculatorModel, CalculatorData>()
                    .ForMember(dest => dest.PurchaseFee,
                        opts => opts.MapFrom(src => src.PurFee))
                    .ForMember(dest => dest.NoOfInstallments,
                        opts => opts.MapFrom(src => src.Term / (int)src.Frequency));

                cfg.CreateMap<CalculatorData, CalculatorModel>()
                    .ForMember(dest => dest.PurFee,
                        opts => opts.MapFrom(src => src.PurchaseFee));

                cfg.CreateMap<CalculatorModel, CalculatorModel>();
            });
        }
    }
}