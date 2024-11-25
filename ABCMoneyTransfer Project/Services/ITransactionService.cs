using ABCMoneyTransfer_Project.Models;

namespace ABCMoneyTransfer_Project.Services
{
    public interface ITransactionService
    {

        // ----------------------------------------------------------------------------------------------------------------------------------

        decimal CalculatePayout(decimal transferAmount, decimal exchangeRate);
        Task AddTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);   
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);

        // ----------------------------------------------------------------------------------------------------------------------------------
    }
}
