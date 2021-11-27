using FinanceManagementImportTool.Models;

namespace FinanceManagementImportTool.Services
{
    public interface IPeriodsService
    {
        Task<IEnumerable<Period>> GetPeriods();
    }
}
