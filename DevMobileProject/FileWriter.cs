using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMobileProject
{
    class FileWriter
    {

        private String path;
        
        public FileWriter(String path)
        {
            this.path = path;
        }

        public void add(String newLine)
        {
            // create a writer and open the file
            StreamWriter tw = new StreamWriter(this.path);

            // write a line of text to the file
            tw.WriteLine(newLine);

            // close the stream
            tw.Close();
        }
    }
}
