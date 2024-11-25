

using ABCMoneyTransfer_Project.Models;

namespace ABCMoneyTransfer_Project.Repository
{
    public interface ITransactionRepository
    {
        // ----------------------------------------------------------------------------------------------------------------------------------

        Task AddTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
        Task<Transaction> GetTransactionByIdAsync(int id);
        Task<List<Transaction>> GetAllTransactionAsync();
        Task SaveChangesAsync();
        Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate);

        // ----------------------------------------------------------------------------------------------------------------------------------
    }
}
