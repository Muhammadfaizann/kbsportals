using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Models;
using KBS.Portals.Calculator.Models;

namespace KBS.Portals.Calculator.Services
{
    public interface IMappingService
    {
        CalculatorData Map(CalculatorModel model);
        CalculatorModel Map(CalculatorData data);
        void MapInto(CalculatorModel source, CalculatorModel destination);
    }
}
