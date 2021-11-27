namespace FinanceManagementImportTool.Services
{
    public interface IImportService
    {
        Task ImportFile(StreamReader streamReader);
    }
}
