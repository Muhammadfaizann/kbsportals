using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Logic.Models;

namespace KBS.Portals.Calculator.Logic
{
    public interface ICalculator
    {
        CalculatorData Calculate();

        void Reload(CalculatorData input);
    }
}
