using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using KBS.Portals.Calculator.Models;

namespace KBS.Portals.Calculator.PageModels
{
    public class FeedbackPageModel : FreshBasePageModel
    {
        private CalculatorModel _calculatorModel;

        public override void Init(object initData)
        {
            base.Init(initData);
            _calculatorModel = (CalculatorModel) initData;
        }
    }
}
