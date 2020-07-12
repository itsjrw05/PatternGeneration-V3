using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatternGeneration_V3
{
    public partial class Form1 : Form
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        DataSet dataSet = new DataSet();

        int value;

        int xSize;
        int ySize;

        int colors;

        int previous;
        int current;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            xSize = Int32.Parse(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ySize = Int32.Parse(textBox2.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            colors = Int32.Parse(textBox3.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Generate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Adjust1();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Adjust2();
        }

        private void Generate()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            for (int x = 0; x < xSize; x++)
            {
                dataGridView1.Columns.Add("Column" + x, "Column" + x);
            }

            for (int y = 0; y < ySize - 1; y++)
            {
                dataGridView1.Rows.Add();
            }

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    if (current == colors)
                    {
                        current = 0;
                    }
                    
                    previous = current;
                    current = previous + 1;

                    dataGridView1.CurrentCell = dataGridView1.Rows[j].Cells[i];

                    dataGridView1.CurrentCell.Value = current;
                }

                current = previous;
            }

            //Verify();

            //Save();
        }

        private void Adjust1()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[j].Cells[i];

                    value = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());
                    dataGridView1.SelectedCells[0].Value = value + 1;

                    if (Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString()) >= colors + 1)
                    {
                        dataGridView1.SelectedCells[0].Value = 1;
                    }
                }
            }

            //Verify();

            //Save();
        }

        private void Adjust2()
        {
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[xSize - 1];

            value = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            dataGridView1.SelectedCells[0].Value = value - 1;

            if (Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString()) <= 0)
            {
                dataGridView1.SelectedCells[0].Value = colors;
            }

            dataGridView1.CurrentCell = dataGridView1.Rows[ySize - 1].Cells[0];

            value = Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString());
            dataGridView1.SelectedCells[0].Value = value + 1;

            if (Int32.Parse(dataGridView1.SelectedCells[0].Value.ToString()) >= colors + 1)
            {
                dataGridView1.SelectedCells[0].Value = 1;
            }

            //Verify();

            //Save();
        }

        private void Verify()
        {
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                for (int j = 0; j < dataGridView1.RowCount; j++)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[j].Cells[i];

                    if (dataGridView1.Rows[Math.Max(0, j + 1)].Cells[i].Value != dataGridView1.CurrentCell.Value)
                    {
                        if (dataGridView1.Rows[Math.Max(0, j - 1)].Cells[i].Value != dataGridView1.CurrentCell.Value)
                        {
                            if (dataGridView1.Rows[j].Cells[Math.Max(0, i + 1)].Value != dataGridView1.CurrentCell.Value)
                            {
                                if (dataGridView1.Rows[j].Cells[Math.Max(0, i - 1)].Value != dataGridView1.CurrentCell.Value)
                                {

                                }
                                else
                                {
                                    Generate();
                                }
                            }
                            else
                            {
                                Generate();
                            }
                        }
                        else
                        {
                            Generate();
                        }
                    }
                    else
                    {
                        Generate();
                    }
                }
            }
        }

        private void Save()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataSet.Tables[0].WriteXml(saveFileDialog.FileName);
            }
        }
    }
}
