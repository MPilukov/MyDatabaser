using MyDatabaser.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MyDatabaser
{
    public partial class ManageConnection : Form
    {
        private readonly IStageStorage _stageStorage;

        public ManageConnection(IStageStorage stageStorage)
        {
            _stageStorage = stageStorage;

            InitializeComponent();

            this.hostTextBox.Text = "1433";
        }

        private static void InformUser(string message)
        {
            MessageBox.Show(message);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var name = this.nameTextBox.Text;
            var address = this.addressTextBox.Text;
            var host = this.hostTextBox.Text;
            var database = this.databaseTextBox.Text;
            var userName = this.userNameTextBox.Text;
            var password = this.passwordTextBox.Text;

            if (string.IsNullOrWhiteSpace(address))
            {
                InformUser("Enter name DB address");
                return;
            }        

            if (string.IsNullOrWhiteSpace(userName))
            {
                InformUser("Enter user name");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                InformUser("Enter user password");
                return;
            }

            var existStageName = _stageStorage.GetStageNames();
            if (existStageName.Any(x => x.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
            {
                InformUser($"The connection name ({name}) is already occupied");
                return;
            }

            _stageStorage.AddStage(new Models.SqlConnectionData
            {
                Name = name,
                Database = database,                
                Host = string.IsNullOrWhiteSpace(host) ? address : $"{address}, {host}",
                UserName = userName,
                Password = password,
            });

            this.Close();
        }
    }
}
