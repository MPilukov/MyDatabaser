using MyDatabaser.Interfaces;
using MyDatabaser.Services;
using System;
using System.Drawing;
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
            informRichTextBox.Clear();

            var query = this.queryTextBox.Text;

            var sb = new StringBuilder();
            var sbError = new StringBuilder();

            _sqlProvider.Execute(query, 
                s => sb = sb.AppendLine(s), 
                s => sbError = sbError.AppendLine(s));

            if (sbError.Length > 0)
            {
                AppendText(informRichTextBox, $"Failed to execute the query : ", Color.DarkRed);
                AppendText(informRichTextBox, Environment.NewLine);
                AppendText(informRichTextBox, sbError.ToString());
            }
            else
            {
                AppendText(informRichTextBox, sb.ToString());
            }
        }

        private void AppendText(RichTextBox box, string text, Color? color = null)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color ?? Color.DarkGreen;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            var existStages = _stageStorage.GetStageNames();
            if (!existStages.Any(x => string.Equals(x, _stageName, StringComparison.InvariantCultureIgnoreCase)))
            {
                MessageBox.Show($@"Not found configuration {_stageName}");
                this.Close();
            }

            this.commonLabel.Text = _commonDescriptor;
        }

        private void QueryTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                ExecuteButton_Click(sender, e);
                e.Handled = true;
            }
        }
    }
}
