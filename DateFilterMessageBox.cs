using System;
using System.Windows.Forms;

public static class DateFilterMessageBox
{
    public static string Show(string[] columns)
    {
        Form form = new Form();
        Label label = new Label();
        ComboBox comboBox = new ComboBox();
        Button buttonOk = new Button();
        Button buttonCancel = new Button();

        form.Text = "Select Column to Filter By";
        label.Text = "Please select a column to filter by:";
        comboBox.Items.AddRange(columns);
        comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        buttonOk.Text = "OK";
        buttonCancel.Text = "Cancel";
        buttonOk.DialogResult = DialogResult.OK;
        buttonCancel.DialogResult = DialogResult.Cancel;

        label.SetBounds(9, 20, 372, 13);
        comboBox.SetBounds(12, 36, 372, 21);
        buttonOk.SetBounds(228, 72, 75, 23);
        buttonCancel.SetBounds(309, 72, 75, 23);

        label.AutoSize = true;
        comboBox.Anchor = comboBox.Anchor | AnchorStyles.Right;
        buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        form.ClientSize = new System.Drawing.Size(396, 107);
        form.Controls.AddRange(new Control[] { label, comboBox, buttonOk, buttonCancel });
        form.ClientSize = new System.Drawing.Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimizeBox = false;
        form.MaximizeBox = false;
        form.AcceptButton = buttonOk;
        form.CancelButton = buttonCancel;

        DialogResult result = form.ShowDialog();
        string selectedColumn = null;
        if (result == DialogResult.OK)
        {
            selectedColumn = comboBox.SelectedItem as string;
        }
        return selectedColumn;
    }
}
