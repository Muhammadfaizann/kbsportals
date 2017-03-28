using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using KBS.Portals.Calculator.Logic.Models;
using KBS.Portals.Calculator.Models;

namespace KBS.Portals.Calculator.Services
{
    public class MappingService : IMappingService
    {
        public CalculatorData Map(CalculatorModel model)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CalculatorModel, CalculatorData>()
                    .ForMember(dest => dest.PurchaseFee,
                        opts => opts.MapFrom(src => src.PurFee))
                    .ForMember(dest => dest.NoOfInstallments,
                        opts => opts.MapFrom(src => src.Term / (int)src.Frequency));
            });
            var calculatorData = Mapper.Map<CalculatorData>(model);
            return calculatorData;
        }

        public CalculatorModel Map(CalculatorData data)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CalculatorData, CalculatorModel>()
                    .ForMember(dest => dest.PurFee,
                        opts => opts.MapFrom(src => src.PurchaseFee));
            });
            var calculatorModel = Mapper.Map<CalculatorModel>(data);
            return calculatorModel;
        }

        public void MapInto(CalculatorModel source, CalculatorModel destination)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CalculatorModel, CalculatorModel>();
            });
            Mapper.Map(source, destination);
        }

    }
}