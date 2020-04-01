using MyDatabaser.Interfaces;
using MyDatabaser.Services;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MyDatabaser
{
    public partial class Editor : Form
    {
        private readonly IStageStorage _stageStorage;
        private readonly string _stageName;
        private readonly ISqlProvider _sqlProvider;
        private readonly string _commonDescriptor;

        public Editor(IStageStorage stageStorage, string stageName)
        {
            _stageName = stageName;
            _stageStorage = stageStorage;

            var stage = _stageStorage.GetStage(_stageName);
            _commonDescriptor = $"{stage.Host} : {stage.Database} ({stage.UserName})";
            _sqlProvider = new SqlProvider(stage.Host, stage.UserName, stage.Password, stage.Database);

            InitializeComponent();
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            var query = this.queryTextBox.Text;

            var sb = new StringBuilder();
            var sbError = new StringBuilder();

            _sqlProvider.Execute(query, 
                s => sb = sb.AppendLine(s), 
                s => sbError = sbError.AppendLine(s));

            if (sbError.Length > 0)
            {
                this.informRichTextBox.Text = $"При выполнении запроса произошла ошибка : {sbError.ToString()}";
            }
            else
            {
                this.informRichTextBox.Text = sb.ToString();
            }
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            var existStages = _stageStorage.GetStageNames();
            if (!existStages.Any(x => string.Equals(x, _stageName, StringComparison.InvariantCultureIgnoreCase)))
            {
                MessageBox.Show($"Не найдена конфигурация для {_stageName}");
                this.Close();
            }

            this.commonLabel.Text = _commonDescriptor;
        }
    }
}
