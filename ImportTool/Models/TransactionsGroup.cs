using System.Xml.Serialization;

namespace FinanceManagementImportTool.Models
{
    [XmlRoot(ElementName = "transactions")]
    public class TransactionsGroup
    {
        [XmlElement(ElementName = "transaction")]
        public List<FinancialTransaction> Transactions { get; set; }
    }
}
