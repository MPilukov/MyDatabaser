﻿using MyDatabaser.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyDatabaser
{
    public partial class MainForm : Form
    {
        private readonly IStageStorage _stageStorage;
        public MainForm(IStageStorage stageStorage)
        {
            _stageStorage = stageStorage;

            InitializeComponent();
        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {
            var existStages = _stageStorage.GetStageNames();

            LoadTable(existStages);
        }

        private void AddButton(string name, string text, Action<object, EventArgs> onClick)
        {
            var maxCount = this.tableLayoutPanel.ColumnCount * tableLayoutPanel.RowCount;
            if (tableLayoutPanel.Controls.Count + 1 > maxCount)
            {
                throw new IndexOutOfRangeException("Not possible to add a new configuration");
            }

            var button = new System.Windows.Forms.Button
            {
                Name = name,
                Dock = DockStyle.Fill,
                Text = text,
                UseVisualStyleBackColor = true,
            };
            button.Click += (s, e) => onClick(s,e);
            tableLayoutPanel.Controls.Add(button);            
        }

        private void ReloadButtons()
        {
            var existStages = _stageStorage.GetStageNames();
            var newStageNames = new List<string>();

            foreach (var existStage in existStages)
            {
                if (!tableLayoutPanel.Controls.ContainsKey(existStage))
                {
                    newStageNames.Add(existStage);
                }
            }

            foreach (var newStageName in newStageNames)
            {
                AddButton(newStageName, newStageName, RunWithConnection);
            }
        }

        private ManageConnection _manageConnection;
        private void AddManageConnection(object sender, EventArgs e)
        {
            _manageConnection = new ManageConnection(_stageStorage);
            _manageConnection.Show();

            _manageConnection.FormClosed += ManageConnection_FormClosed;
        }

        private void ManageConnection_FormClosed(object sender, FormClosedEventArgs e)
        {
            _manageConnection.FormClosed -= ManageConnection_FormClosed;
            ReloadButtons();
        }

        private Editor _editor;
        private void RunWithConnection(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                _editor = new Editor(_stageStorage, button.Name);
                _editor.Show();

                _editor.FormClosed += Editor_FormClosed;
            }

        }

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            _editor.FormClosed -= Editor_FormClosed;            
        }

        private void LoadTable(IReadOnlyList<string> existStages)
        {
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.SuspendLayout();

            this.SuspendLayout();

            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 5;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

            tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";

            tableLayoutPanel.RowCount = 5;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel.TabIndex = 1;

            var maxCount = this.tableLayoutPanel.ColumnCount * tableLayoutPanel.RowCount;
            var count = existStages.Count;

            AddButton("ManageConnection", "Add a new configuration", AddManageConnection);

            for (var i = 0; i < Math.Min(count, maxCount); i++)
            {
                AddButton(existStages[i], existStages[i], RunWithConnection);
            }

            this.ClientSize = new System.Drawing.Size(945, 616);
            this.Controls.Add(tableLayoutPanel);

            tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
