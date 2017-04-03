﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using KBS.Portals.Calculator.Models;
using KBS.Portals.Calculator.Services;

namespace KBS.Portals.Calculator.PageModels
{
    public class FeedbackPageModel : FreshBasePageModel
    {
        private readonly IFeedbackService _feedbackService;
        public CalculatorModel CalculatorModel { get; set; }

        public FeedbackPageModel(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public override void Init(object initData)
        {
            CalculatorModel = (CalculatorModel) initData;
        }

        public async Task SubmitFeedback(string userFeedback, bool includeModel)
        {
            HttpResponseMessage httpResponseMessage;
            if (includeModel)
            {
                httpResponseMessage = await _feedbackService.SendFeedback(userFeedback, CalculatorModel);
            }
            else
            {
                httpResponseMessage = await _feedbackService.SendFeedback(userFeedback);
            }

            if (httpResponseMessage.StatusCode == HttpStatusCode.Created)
            {
                await CoreMethods.DisplayAlert("Successfully submitted feedback", "Thanks for the feedback. Your message has been submitted to KBS for review.", "Okay");
            }
            else
            {
                await CoreMethods.DisplayAlert("Failed to send", "The server can't be reached currently.", "Okay");
            }
        }
    }
}
