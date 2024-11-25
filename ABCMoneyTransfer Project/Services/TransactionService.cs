using ABCMoneyTransfer_Project.Data;
using ABCMoneyTransfer_Project.Models;
using ABCMoneyTransfer_Project.Repository;
using Microsoft.EntityFrameworkCore;

namespace ABCMoneyTransfer_Project.Services
{
    // ----------------------------------------------------------------------------------------------------------------------------------

    public class TransactionService : ITransactionService
    {
        // ----------------------------------------------------------------------------------------------------------------------------------

        private readonly ITransactionRepository _repository;

        // ----------------------------------------------------------------------------------------------------------------------------------

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task AddTransactionAsync(Transaction transaction)
        {

            transaction.PayoutAmount = transaction.TransferAmount * transaction.ExchangeRate;

            await _repository.AddTransactionAsync(transaction);
            await _repository.SaveChangesAsync();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public decimal CalculatePayout(decimal transferAmount, decimal exchangeRate)
        {
            return transferAmount * exchangeRate;
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _repository.GetAllTransactionAsync();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _repository.GetTransactionByIdAsync(id);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task DeleteTransactionAsync(int id)
        {
            await _repository.DeleteTransactionAsync(id);
            await _repository.SaveChangesAsync();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            transaction.PayoutAmount = CalculatePayout(transaction.TransferAmount, transaction.ExchangeRate);

            await _repository.UpdateTransactionAsync(transaction);
            await _repository.SaveChangesAsync();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _repository.GetTransactionsByDateRangeAsync(startDate, endDate);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------
    }

    // ----------------------------------------------------------------------------------------------------------------------------------
}
