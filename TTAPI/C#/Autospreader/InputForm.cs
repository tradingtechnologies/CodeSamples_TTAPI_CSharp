using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTAPI_Samples
{
    public partial class InputDialog : Form
    {
        public InputDialog(string title, string description)
        {
            InitializeComponent();

            buttonOK.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            Text = title;
            labelDescription.Text = description;
        }

        static public DialogResult InputDialogBox(string title, string description, ref string value)
        {
            InputDialog dialog = new InputDialog(title, description);
            dialog.textBoxValue.Text = value;
            DialogResult dialogResult = dialog.ShowDialog();
            value = dialog.textBoxValue.Text;
            return dialogResult;
        }
    }
}
