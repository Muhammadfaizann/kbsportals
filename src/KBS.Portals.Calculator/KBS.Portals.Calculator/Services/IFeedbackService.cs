using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KBS.Portals.Calculator.Models;

namespace KBS.Portals.Calculator.Services
{
    public interface IFeedbackService
    {
        Task<bool> SendFeedback(string message, CalculatorModel calculatorModel = null);
    }
}
