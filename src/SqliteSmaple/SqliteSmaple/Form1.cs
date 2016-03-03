using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SqliteSmaple.Lib;
using SqliteSmaple.Model;
using System.Data.Entity;

namespace SqliteSmaple
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.newButton.Click += (s, e) =>
            {
                this.BindingSource.AddNew();
            };

            this.saveButton.Click += (s, e) => {
                this.BindingSource.EndEdit();
                this.Database.SaveChanges();
                this.Retrieve();
            };

            this.doneButton.Click += (s, e) => {
                if (this.dataGridView1.SelectedRows.Count < 1) { return; }

                DialogResult result = MessageBox.Show($"Do you want to Update Done flag {this.dataGridView1.SelectedRows.Count:n0} row(s) ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (DialogResult.Yes != result) { return; }
                try
                {
                    bool hasChange = false;
                    if (this.dataGridView1.SelectedRows.Count > 0)
                    {
                        for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                        {
                            Todo item = this.dataGridView1.SelectedRows[i].DataBoundItem as Todo;
                            if (item != null)
                            {
                                if (item.Id > 0)
                                {
                                    var foundItem = this.Database.Todoes.Find(item.Id);
                                    if (foundItem != null)
                                    {
                                        foundItem.Done = true;
                                        hasChange = true;
                                    }
                                }
                            }
                        }

                        if (hasChange)
                        {
                            this.Database.SaveChanges();
                            this.Retrieve();
                        }

                    }

                }
                catch (Exception ex)
                {

                }
            };

            this.dataGridView1.SelectionChanged+= (s, e) => {
                if(this.dataGridView1.SelectedRows.Count > 0)
                {
                    Todo selectedItem = this.dataGridView1.SelectedRows[0].DataBoundItem as Todo;
                    if(selectedItem != null)
                    {
                        this.doneButton.Enabled = (selectedItem.Id > 0);
                    }
                }
            };

            this.deleteButton.Click += (s, e) =>
            {
                if (this.dataGridView1.SelectedRows.Count < 1) { return; }

                DialogResult result = MessageBox.Show($"Do Delete {this.dataGridView1.SelectedRows.Count:n0} row(s) ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (DialogResult.Yes != result) { return; }
                try
                {
                    bool hasChange = false;
                    if (this.dataGridView1.SelectedRows.Count > 0)
                    {
                        for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                        {
                            Todo item = this.dataGridView1.SelectedRows[i].DataBoundItem as Todo;
                            if (item != null)
                            {
                                if (item.Id > 0)
                                {
                                    var foundItem = this.Database.Todoes.Find(item.Id);
                                    if (foundItem != null)
                                    {
                                        //this.Database.Todoes.Remove(foundItem);
                                        foundItem.Deleted = true;
                                        hasChange = true;
                                    }
                                }
                                else
                                {
                                    this.dataGridView1.Rows.Remove(this.dataGridView1.SelectedRows[i]);
                                }
                            }
                        }

                        if (hasChange)
                        {
                            this.Database.SaveChanges();
                            this.Retrieve();
                        }

                    }

                }
                catch (Exception ex)
                {
                    
                }
            };

            this.Load += (s, e) =>
            {
                this.Retrieve();
            };

            InitializeDataGrid();
        }

        private void InitializeDataGrid()
        {
            this.dataGridView1.AutoGenerateColumns = true;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.DataSource = this.BindingSource;
        }

        private void Retrieve()
        {
            

            this.titleTextBox.DataBindings.Clear();
            this.noteTextBox.DataBindings.Clear();
            this.idLabel.DataBindings.Clear();

            ResetDatabase();
            this.Database.Todoes
                .Where(t => !t.Deleted)
                .Load();

            this.BindingSource.DataSource = this.Database.Todoes.Local.ToBindingList();

            this.titleTextBox.DataBindings.Add("Text", this.BindingSource, "Title");
            this.noteTextBox.DataBindings.Add("Text", this.BindingSource, "Note");
            this.idLabel.DataBindings.Add("Text", this.BindingSource, "Id");
        }

        private DatabaseContext _db = null;
        private BindingSource _bs = null;

        private void ResetDatabase()
        {
            _db = new DatabaseContext();
        }

        private BindingSource BindingSource
        {
            get
            {
                if (_bs == null) { _bs = new BindingSource(); }
                return _bs;
            }
        }

        private DatabaseContext Database
        {
            get
            {
                if(_db == null) { ResetDatabase(); }
                return _db;
            }
        }
    }
}
