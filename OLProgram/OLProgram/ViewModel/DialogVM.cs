using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OLProgram.ViewModel
{
    public class DialogVM
    {
        private static OpenFileDialog openXmlDialog = new OpenFileDialog() { Title = "Open Database", Filter = "XML Document (.xml)|*.xml", DefaultExt = "xml", InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), CheckFileExists = true };
        private static SaveFileDialog saveXmlDialog = new SaveFileDialog() { Title = "Save Database", Filter = "XML Document (.xml)|*.xml", DefaultExt = "xml", InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) };
        private static SaveFileDialog saveBillDialog = new SaveFileDialog() { Title = "Save Bill", Filter = "Comma-separated values (*.csv)|*.csv", DefaultExt = "csv", InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) };

        public bool ShowNew() => 
            MessageBox.Show("Are you sure (bla bla)?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes;

        public string ShowOpen() => openXmlDialog.ShowDialog() == true ? openXmlDialog.FileName : null;

        public string ShowSave() => saveXmlDialog.ShowDialog() == true ? saveXmlDialog.FileName : null;

        public string ShowSaveBill() => saveBillDialog.ShowDialog() == true ? saveBillDialog.FileName : null;

    }
}
