using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string cellValue = null;
        bool stop = false;
        private void button1_Click(object sender, EventArgs e)
        {
            do
            {
                string filePath = string.Empty;
                openFileDialog1.InitialDirectory = Application.StartupPath;
                openFileDialog1.FileName = "*.xlsx";
                openFileDialog1.Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog1.FileName;
                    Excel.Application xlApp = new Excel.Application();
                    //Excel.Workbook xlWorkbook = xlApp.Workbooks.Add();
                    Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
                    Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Excel.Range xlRange = xlWorksheet.UsedRange;
                    int rowCount = xlRange.Rows.Count;
                    int colCount = xlRange.Columns.Count;
                    for (int i = 1; i <= rowCount; i++)
                    {
                        for (int j = 1; j <= colCount; j++)
                        {
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                            {
                                cellValue = xlRange.Cells[i, j].Value2;
                                listBox1.Items.Add(cellValue.ToString());
                            }    
                        }
                    }
                    xlApp.Workbooks.Close();
                    xlApp.Quit();

                    xlRange = null;
                    xlWorksheet = null;
                    xlWorkbook = null;
                    xlApp = null;
                    stop = true;
                }
                else
                {
                    if(listBox1.Items.Count == 0)
                    {
                        MessageBox.Show("Excel dosyasını seçmediniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.button1_Click(sender, e);
                        break;
                    }
                    
                }
            }
            while (stop == false);
        }
    }
}
