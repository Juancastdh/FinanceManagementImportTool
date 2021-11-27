using FinanceManagementImportTool.Services;
using FinanceManagementImportTool.Services.Implementations;
using System.Security;

namespace FinanceManagementImportTool
{
    public partial class Import : Form
    {
        private OpenFileDialog openFileDialog1;
        private readonly IImportService ImportService;

        public Import(string apiBaseUrl)
        {
            InitializeComponent();
            openFileDialog1 = new OpenFileDialog();
            ImportService = new XMLImportService(apiBaseUrl);
            bImport.Enabled = false;

        }

        private void bSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SetPathText(openFileDialog1.FileName);
                    bImport.Enabled = true;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void SetPathText(string pathText)
        {
            tbFilePath.Text = pathText;   
        }

        private async void bImport_Click(object sender, EventArgs e)
        {

            try
            {
                if (tbFilePath.Text.Length > 0)
                {
                    StreamReader? fileStreamReader = new StreamReader(tbFilePath.Text);
                    await ImportService.ImportFile(fileStreamReader);
                    tbFilePath.Text = "";
                    bImport.Enabled = false;
                    MessageBox.Show("File imported successfully");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
            }



        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbFilePath_TextChanged(object sender, EventArgs e)
        {
            if(tbFilePath.Text.Length > 0)
            {
                bImport.Enabled = true;
            }
            else
            {
                bImport.Enabled = false;
            }
        }
    }
}