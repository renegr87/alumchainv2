using Alumbrado.BLL.Abstracts;
using Alumbrado.BLL.Models;
using Microsoft.Extensions.Configuration;

namespace Alumbrado
{
    public partial class Main : Form
    {
        // File Dialog
        private string _InitialDirectory = "c:\\";
        private string _Filter = "Json files (*.json)|*.json";
        // Readings
        private string Source;
        private Reading[] Readings;
        // Services 
        private IPublishService _PublishService;
        private IConfiguration _Configuration;

        public Main(IPublishService publishService, IConfiguration configuration)
        {
            _PublishService = publishService;
            _Configuration = configuration;

            InitializeComponent();
        }

        #region Actions

        private void bt_load_Click(object sender, EventArgs e)
        {
            Source = GetSourceFromFile();
            if(Source is not null)
            {
                try 
                {
                    Readings = _PublishService.LoadReadings(Source);
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }                
            }            
        }

        private void bt_validate_Click(object sender, EventArgs e)
        {
            var isValid = _PublishService.ValidateReadings(Readings);
            var message = isValid ? "Valido" : "Invalido";
            lb_valid.Text = message;
        }

        private void bt_publish_Click(object sender, EventArgs e)
        {
            _PublishService.PublishToIPFSAsync(Source);
        }

        #endregion

        #region private methods

        private string? GetSourceFromFile()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = _InitialDirectory;
                openFileDialog.Filter = _Filter;
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                var option = openFileDialog.ShowDialog();

                if (option == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    SetFile(filePath);
                    var fileStream = openFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        return fileContent = reader.ReadToEnd();
                    }
                }
                return null;
            }
        }

        private void SetFile(string filePath) {
            lb_file.Text = filePath;
            lb_valid.Text = "-";
        }

        #endregion

    }
}