using FinanceManagementImportTool.Models;

namespace FinanceManagementImportTool.Services
{
    public interface IFinancialTransactionsService
    {
        Task AddFinancialTransaction(FinancialTransaction financialTransaction);
    }
}
