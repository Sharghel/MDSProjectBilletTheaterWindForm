using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectBilletTheaterWindForm
{
    public partial class Form2 : Form
    {
        private int[,] matriceData;
        public Form2(int[,] matriceData)
        {
            InitializeComponent();
            this.matriceData = matriceData;
            DisplayMatricePlaces();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // You can perform any additional logic or set data here if needed

            // Set the DialogResult to OK and close the form
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void DisplayMatricePlaces()
        {
            tableLayoutPanel1.Controls.Clear();

            for (int row = 0; row < matriceData.GetLength(0); row++)
            {
                for (int col = 0; col < matriceData.GetLength(1); col++)
                {
                    Label label = new Label();
                    // label.Text = matriceData[row, col].ToString();
                    label.Dock = DockStyle.Fill;
                    label.TextAlign = ContentAlignment.MiddleCenter;

                    // Set background color based on the value in the matrix
                    label.BackColor = (matriceData[row, col] == 0) ? Color.Green : Color.Red;
                    
                    // Set the Margin property to Padding.Empty to remove any gap
                    label.Margin = Padding.Empty;

                    tableLayoutPanel1.Controls.Add(label, col, row);
                }
            }
        }
    }
}
