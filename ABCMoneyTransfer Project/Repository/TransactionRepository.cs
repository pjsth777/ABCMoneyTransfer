using ABCMoneyTransfer_Project.Data;
using ABCMoneyTransfer_Project.Models;
using Microsoft.EntityFrameworkCore;


namespace ABCMoneyTransfer_Project.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        // ----------------------------------------------------------------------------------------------------------------------------------

        private readonly AppDbContext _context;

        // ----------------------------------------------------------------------------------------------------------------------------------

        public TransactionRepository(AppDbContext context)
        {
            _context = context;     
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<Transaction>> GetAllTransactionAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.FirstOrDefaultAsync(testc => testc.Id == id);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task AddTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task UpdateTransactionAsync(Transaction transaction)
        {
            var existingTransaction = await _context.Transactions.FindAsync(transaction.Id);
            if (existingTransaction != null)
            {

                existingTransaction.SenderFirstName = transaction.SenderFirstName;
                existingTransaction.SenderMiddleName = transaction.SenderMiddleName;
                existingTransaction.SenderLastName = transaction.SenderLastName;

                existingTransaction.ReceiverFirstName = transaction.ReceiverFirstName;
                existingTransaction.ReceiverMiddleName = transaction.ReceiverMiddleName;
                existingTransaction.ReceiverLastName = transaction.ReceiverLastName;

                existingTransaction.SenderAddress = transaction.SenderAddress;
                existingTransaction.SenderCountry = transaction.SenderCountry;

                existingTransaction.ReceiverAddress = transaction.ReceiverAddress;
                existingTransaction.ReceiverCountry = transaction.ReceiverCountry;

                existingTransaction.BankName = transaction.BankName;
                existingTransaction.AccountNumber = transaction.AccountNumber;
                existingTransaction.TransferAmount = transaction.TransferAmount;
                existingTransaction.ExchangeRate = transaction.ExchangeRate;
                existingTransaction.PayoutAmount = transaction.PayoutAmount;
            }
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<Transaction>> GetTransactionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {

            return await _context.Transactions
                .Where(t => t.TransferDate >= startDate && t.TransferDate <= endDate)
                .ToListAsync();
        }

        // ----------------------------------------------------------------------------------------------------------------------------------
    }
}
