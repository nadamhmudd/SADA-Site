using Stripe.Checkout;

namespace SADA.Web.Areas.Client.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IURLHelper _urlHelper;

        public PaymentController(IURLHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public SessionCreateOptions Stripe(string successUrl, string cancelUrl)
        {
            return new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>{
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = _urlHelper.Url(successUrl),
                CancelUrl = _urlHelper.Url(cancelUrl),
            };
        }
    }
}
