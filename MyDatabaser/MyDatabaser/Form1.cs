using MyDatabaser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyDatabaser
{
    public partial class MainForm : Form
    {
        private readonly IStorage Storage;
        public MainForm(IStorage storage)
        {
            Storage = storage;

            InitializeComponent();
        }

        private List<string> GetStages()
        {
            var existStagesStr = Storage.Get("Stages");
            var stages = (existStagesStr ?? "").Split('&');

            return stages.Where(x => !string.IsNullOrEmpty(x)).ToList();
        }

        private void SetStages(List<string> stages)
        {
            var stagesStr = String.Join("&", stages);
            Storage.Set("Stages", stagesStr);
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            var existStages = GetStages();

            LoadTable(existStages);
        }

        private void AddButton(string name, string text)
        {
            var maxCount = this.tableLayoutPanel.ColumnCount * tableLayoutPanel.RowCount;
            if (tableLayoutPanel.Controls.Count + 1 > maxCount)
            {
                throw new IndexOutOfRangeException("Невозможно добавить новую конфигурацию!");
            }

            var button = new System.Windows.Forms.Button();
            button.Name = name;
            button.Dock = DockStyle.Fill;
            button.Text = text;
            button.UseVisualStyleBackColor = true;
            tableLayoutPanel.Controls.Add(button);            
        }

        private void LoadTable(List<string> existStages)
        {
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.SuspendLayout();

            this.SuspendLayout();

            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 5;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));

            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";

            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel.TabIndex = 1;

            var maxCount = this.tableLayoutPanel.ColumnCount * tableLayoutPanel.RowCount;
            var count = existStages.Count;

            AddButton("ManageConnection", "Добавить новое соединение");

            for (var i = 0; i < Math.Min(count, maxCount); i++)
            {
                AddButton(existStages[i], existStages[i]);
            }

            this.ClientSize = new System.Drawing.Size(945, 616);
            this.Controls.Add(tableLayoutPanel);

            tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
