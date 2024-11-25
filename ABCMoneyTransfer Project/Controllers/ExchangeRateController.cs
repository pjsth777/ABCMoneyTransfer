using Microsoft.AspNetCore.Mvc;
using ABCMoneyTransfer_Project.Services;
using ABCMoneyTransfer_Project.Models;
using System.Threading.Tasks;

namespace ABCMoneyTransfer_Project.Controllers
{
    // ----------------------------------------------------------------------------------------------------------------------------------

    public class ExchangeRateController : Controller
    {

        // ----------------------------------------------------------------------------------------------------------------------------------

        private readonly ExchangeRateService _exchangeRateService;

        // ----------------------------------------------------------------------------------------------------------------------------------

        public ExchangeRateController(ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Index()
        {
            var exchangeRate = await _exchangeRateService.GetExchangeRatesAsync();
            if (exchangeRate.HasValue)
            {
                ViewBag.ExchangeRate = exchangeRate.Value;
            }
            else
            {
                ViewBag.ErrorMessage = "Unable to fetch the exchange rate.";
            }

            return View();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------
    }
}
