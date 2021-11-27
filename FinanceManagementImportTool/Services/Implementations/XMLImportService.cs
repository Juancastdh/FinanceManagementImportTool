using FinanceManagementImportTool.Models;
using System.Xml.Serialization;

namespace FinanceManagementImportTool.Services.Implementations
{
    public class XMLImportService : IImportService
    {
        private readonly IFinancialTransactionsService FinancialTransactionsService;
        private readonly IPeriodsService PeriodsService;
        private IEnumerable<Period> Periods;


        public XMLImportService(string apiBaseUrl)
        {

            FinancialTransactionsService = new APIFinancialTransactionsService(apiBaseUrl);
            PeriodsService = new APIPeriodsService(apiBaseUrl);
            Periods = new List<Period>();

        }

        public async Task ImportFile(StreamReader streamReader)
        {
            await SetPeriods();

            IEnumerable<FinancialTransaction> financialTransactions = GetFinancialTransactionsFromXML(streamReader);
            foreach (FinancialTransaction financialTransaction in financialTransactions)
            {
                FinancialTransaction financialTransactionToImport = CompleteFinancialTransactionProperties(financialTransaction);
                await FinancialTransactionsService.AddFinancialTransaction(financialTransactionToImport);
            }
        }

        private IEnumerable<FinancialTransaction> GetFinancialTransactionsFromXML(StreamReader streamReader)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(TransactionsGroup));

            TransactionsGroup transactionsGroup = (TransactionsGroup?)deserializer.Deserialize(streamReader) ?? new TransactionsGroup();

            List<FinancialTransaction> financialTransactions = transactionsGroup.Transactions;

            return financialTransactions;
        }

        private async Task SetPeriods()
        {
            Periods = await PeriodsService.GetPeriods();
        }

        private FinancialTransaction AddIsExpenseValueToTransaction(FinancialTransaction financialTransaction)
        {
            if (financialTransaction.Value < 0)
            {
                financialTransaction.IsExpense = false;
            }
            else
            {
                financialTransaction.IsExpense = true;
            }

            return financialTransaction;
        }

        private Period GetPeriodByDate(DateTime date)
        {
            return Periods.FirstOrDefault(period => date >= period.StartDate && date < period.EndDate) ?? new Period();
        }

        private FinancialTransaction AddPeriodIdToTransaction(FinancialTransaction financialTransaction)
        {
            Period transactionPeriod = GetPeriodByDate(financialTransaction.Date);

            financialTransaction.PeriodId = transactionPeriod.Id;

            return financialTransaction;
        }

        private FinancialTransaction CompleteFinancialTransactionProperties(FinancialTransaction financialTransaction)
        {
            financialTransaction = AddIsExpenseValueToTransaction(financialTransaction);
            financialTransaction = AddPeriodIdToTransaction(financialTransaction);

            return financialTransaction;
        }


    }
}
