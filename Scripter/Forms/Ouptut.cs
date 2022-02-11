using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scripter
{
    public partial class Ouptut : Form
    {
        public Ouptut()
        {
            InitializeComponent();
        }

        public void Addline(string text)
        {
            textBox1.AppendText(text + "\r\n");
        }

        public void CloseForm()
        {
            this.Close();
        }

        public void DeleteText()
        {
            textBox1.Text = "";
        }

        
    }
}
