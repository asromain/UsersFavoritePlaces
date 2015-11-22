using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMobileProject
{
    class FileReader
    {

        private String path;
        
        public FileReader(String path)
        {
            this.path = path;
        }

        public double[][] readfromtextfile()
        {
            List<Double[]> list = new List<double[]>();
            String line = "";
            int cpt = 0;

            //Content of file
            String content = "";

            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {

                // Skip lines
                while ((line = file.ReadLine()) != null)
                {
                    // System.Diagnostics.Debug.WriteLine("");    // debug
                    content = line;
                    String[] data = content.Split(',');
                    double data1 = Convert.ToDouble(data[0]);
                    double data2 = Convert.ToDouble(data[1]);

                    list.Add(new double[] { data1, data2 });
                    cpt++;
                }

                file.Close();
            }

            return list.ToArray();
       
        }
    }
}
