using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desktop.crud.sqlite.src.view
{
    public partial class FormAdd : Form
    {
        public string action;
        public FormAdd()
        {
            this.action = "add";
            InitializeComponent();
        }
    }
}
