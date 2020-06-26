using Stripe;
using Stripe.BillingPortal;
using StripeSample.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace task6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            

            return View();
        }
        [HttpPost]
        public ActionResult index(FormCollection PC)
        {
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = "API KEY HERE";

            var options = new SessionCreateOptions
            {
                Customer = "cus_HWWS7IYETJBWRd",
                ReturnUrl = "http://localhost:56459/Home/Index",
            };
            var service = new SessionService();
            Session test = service.Create(options);

            var options1 = new WebhookEndpointCreateOptions
            {
                Url = "https://git.heroku.com/sptask6.git",
                EnabledEvents = new List<string>
            {
                "charge.failed",
                "charge.succeeded",
            },
            };
            var service1 = new WebhookEndpointService();
            service1.Create(options1);

            //service1.Get("we_1GxplED1GhlR3Zf7y0VwRON8");

            return Redirect(test.Url);
        }

        //[HttpPost]
        //public async Task<ActionResult> Index()
        //{
        //    var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        //    try
        //    {
        //        var stripeEvent = EventUtility.ParseEvent(json);

        //        // Handle the event
        //        if (stripeEvent.Type == Events.PaymentIntentSucceeded)
        //        {
        //            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
        //            // Then define and call a method to handle the successful payment intent.
        //            // handlePaymentIntentSucceeded(paymentIntent);
        //        }
        //        else if (stripeEvent.Type == Events.PaymentMethodAttached)
        //        {
        //            var paymentMethod = stripeEvent.Data.Object as PaymentMethod;
        //            // Then define and call a method to handle the successful attachment of a PaymentMethod.
        //            // handlePaymentMethodAttached(paymentMethod);
        //        }
        //        // ... handle other event types
        //        else
        //        {
        //            // Unexpected event type
        //            return BadRequest();
        //        }
        //        return Ok();
        //    }
        //    catch (StripeException e)
        //    {
        //        return BadRequest();
        //    }
        //}

        public ActionResult Charge()
        {
            ViewBag.Message = "Learn how to process payments with Stripe";

            return View(new StripeChargeModel());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Charge(StripeChargeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var chargeId = await ProcessPayment(model);
            return View("Index");
        }

        //private async Task<string> ProcessPayment(StripeChargeModel model)
        //{
        //    return await Task.Run(() =>
        //    {
        //        var myCharge = new StripeChargeCreateOptions
        //        {
        //            // convert the amount of £12.50 to pennies i.e. 1250
        //            Amount = (int)(model.Amount * 100),
        //            Currency = "gbp",
        //            Description = "Description for test charge",
        //            Source = new StripeSourceOptions{
        //                TokenId = model.Token
        //            }
        //        };

        //        var chargeService = new StripeChargeService("sk_test_51Gub1CD1GhlR3Zf7VQPN0qwG32O9gEEovXXduNah0f6xHHwVlaf9DsuNHzLOpDXgsLvyBCurxjA33xEOUJGTILCZ005ANR5mTY");
        //        var stripeCharge = chargeService.Create(myCharge);

        //        return stripeCharge.Id;
        //   });
        //}

    }
}
