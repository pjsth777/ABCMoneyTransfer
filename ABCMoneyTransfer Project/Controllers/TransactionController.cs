using Microsoft.AspNetCore.Mvc;
using ABCMoneyTransfer_Project.Services;
using ABCMoneyTransfer_Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace ABCMoneyTransfer_Project.Controllers
{
    // ----------------------------------------------------------------------------------------------------------------------------------

    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ExchangeRateService _exchangeRateService;

        // ----------------------------------------------------------------------------------------------------------------------------------

        public TransactionController(ITransactionService transactionService, ExchangeRateService exchangeRateService)
        {
            _transactionService = transactionService;
            _exchangeRateService = exchangeRateService;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            return View(transactions);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return View(transaction);
            }

            try
            {
                var exchangeRate = await _exchangeRateService.GetExchangeRatesAsync();

                if (exchangeRate == null || !exchangeRate.HasValue)
                {
                    ModelState.AddModelError("", "Unable to retrieve the current exchange rate. Please try again later.");
                    return View(transaction);
                }

                transaction.ExchangeRate = exchangeRate.Value;
                transaction.PayoutAmount = transaction.TransferAmount * transaction.ExchangeRate;

                await _transactionService.AddTransactionAsync(transaction);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occured while processing the transaction: {ex.Message}");
            }

            return View(transaction);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transaction model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _transactionService.UpdateTransactionAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _transactionService.DeleteTransactionAsync(id);
            return RedirectToAction("Index");
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        [HttpGet]
        public IActionResult TransactionReport()
        {
            return View();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TransactionReport(DateTime startDate, DateTime endDate)
        {
            startDate = startDate.Date;
            endDate = endDate.Date.AddDays(1).AddMilliseconds(-1);

            var transactions = await _transactionService.GetTransactionsByDateRangeAsync(startDate, endDate);
            return View("TransactionReport", transactions);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

    }

    // ----------------------------------------------------------------------------------------------------------------------------------
}
