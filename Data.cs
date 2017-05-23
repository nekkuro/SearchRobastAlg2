using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRobastAlg
{
    public static class Data
    {
        public static string OpenFile()
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true)
                return null;
            return openFileDialog.FileName;
        }
        public static void SaveFile()
        {
            Stream myStream;
            var saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                if ((myStream = saveFileDialog.OpenFile()) != null)
                {
                    myStream.Close();
                }
            }
        }
        public static void OpenFileFromDatabase()
        {

        }
        public static void SaveFileToDatabase()
        {

        }

    }
}
