using System.Windows;
using System.Windows.Forms;
using Prism.Services.Dialogs;
using DialogResult = System.Windows.Forms.DialogResult;
using MessageBox = System.Windows.MessageBox;

namespace HETSPrism.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// Shows folder browser dialog to the user.
        /// </summary>
        /// <returns>Path to a folder that user selected or null if user canceled the operation.</returns>
        string ShowFolderBrowserDialog();

        /// <summary>
        /// Shows a message box to the user.
        /// </summary>
        /// <param name="message">Message to display in the message box.</param>
        /// <returns></returns>
        MessageBoxResult ShowMessageBox(string message);
    }

    public class DialogService : IDialogService
    {
        public string ShowFolderBrowserDialog()
        {
            FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            var result = openFileDialog.ShowDialog();
            string path = null;
            if (result == DialogResult.OK)
                path = openFileDialog.SelectedPath;

            return path;
        }

        public MessageBoxResult ShowMessageBox(string message)
        {
            return MessageBox.Show(message);
        }
    }
}