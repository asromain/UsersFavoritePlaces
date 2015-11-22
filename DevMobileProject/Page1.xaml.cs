using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;

namespace DevMobileProject
{
    public partial class Page1 : PhoneApplicationPage
    {

        KMeansClustering kmeans;

        Microsoft.Phone.Maps.Controls.LocationRectangle bounds;

        public Page1()
        {
            InitializeComponent();

            bounds = getBoundingCoord();

            carte.SetView(bounds);

            /*
            ON AFFICHE KMEANS

            double[][] rawData = new double[20][];
            rawData[0] = new double[] { 65.0, 220.0 };
            rawData[1] = new double[] { 73.0, 160.0 };
            rawData[2] = new double[] { 59.0, 110.0 };
            rawData[3] = new double[] { 61.0, 120.0 };
            rawData[4] = new double[] { 75.0, 150.0 };
            rawData[5] = new double[] { 67.0, 240.0 };
            rawData[6] = new double[] { 68.0, 230.0 };
            rawData[7] = new double[] { 70.0, 220.0 };
            rawData[8] = new double[] { 62.0, 130.0 };
            rawData[9] = new double[] { 66.0, 210.0 };
            rawData[10] = new double[] { 77.0, 190.0 };
            rawData[11] = new double[] { 75.0, 180.0 };
            rawData[12] = new double[] { 74.0, 170.0 };
            rawData[13] = new double[] { 70.0, 210.0 };
            rawData[14] = new double[] { 61.0, 110.0 };
            rawData[15] = new double[] { 58.0, 100.0 };
            rawData[16] = new double[] { 66.0, 230.0 };
            rawData[17] = new double[] { 59.0, 120.0 };
            rawData[18] = new double[] { 68.0, 210.0 };
            rawData[19] = new double[] { 61.0, 130.0 };

            kmeans = new KMeansClustering(3, rawData);

            String result = kmeans.displayResult();
            textBlock.Text = result; */
 

        }

        private Microsoft.Phone.Maps.Controls.LocationRectangle getBoundingCoord()
        {
            double minX, maxX, minY, maxY;

            FileReader filereader = new FileReader("Data/data.txt");

            double[][] data = filereader.readfromtextfile();

            minX = data[0][0];
            maxX = data[0][0];
            minY = data[0][1];
            maxY = data[0][1];

            for (int i = 1; i < data.Length; i++)
            {
                if (minX > data[i][0])
                {
                    minX = data[i][0];
                }
                if (maxX < data[i][0])
                {
                    maxX = data[i][0];
                }
                if (minY > data[i][1])
                {
                    minY = data[i][1];
                }
                if (maxY < data[i][1])
                {
                    maxY = data[i][1];
                }
            }

            System.Diagnostics.Debug.WriteLine("[" + minX + " ; " + minY + "]" + "[" + maxX + " ; " + maxY + "]");

            return new Microsoft.Phone.Maps.Controls.LocationRectangle(maxX, minY, minX, maxY);
        }

        private void clusterNumberOK(object sender, RoutedEventArgs e)
        {
            carte.SetView(bounds);

            int nb;
            if (int.TryParse(clusterNumber.Text, out nb)) {
                if (nb > 0 && nb <= 10)
                {
                    computeClusters(nb);
                }
            }
        }

        private void computeClusters(int nb)
        {
            FileReader filereader = new FileReader("Data/data.txt");

            double[][] rawData = filereader.readfromtextfile();
            kmeans = new KMeansClustering(nb, rawData);

            List<List<double[]>> clusters = kmeans.getCalculatedClusters();

            System.Diagnostics.Debug.WriteLine(kmeans.displayResult());

            Microsoft.Phone.Maps.Controls.MapLayer points = new Microsoft.Phone.Maps.Controls.MapLayer();

            System.Windows.Media.Color[] colors = new System.Windows.Media.Color[10];

            colors[0] = System.Windows.Media.Colors.Blue;
            colors[1] = System.Windows.Media.Colors.Red;
            colors[2] = System.Windows.Media.Colors.Green;
            colors[3] = System.Windows.Media.Colors.Yellow;
            colors[4] = System.Windows.Media.Colors.Purple;
            colors[5] = System.Windows.Media.Colors.Orange;
            colors[6] = System.Windows.Media.Colors.Cyan;
            colors[7] = System.Windows.Media.Colors.DarkGray;
            colors[8] = System.Windows.Media.Colors.Magenta;
            colors[9] = System.Windows.Media.Colors.White;

            for (int j = 0; j < clusters.ToArray().Length; j++)
            {
                System.Diagnostics.Debug.WriteLine(j);
                // lire dans chacun des clusters
                for (int i = 0; i < clusters.ElementAt(j).ToArray().Length; i++)
                {
                    double[] data = clusters.ElementAt(j).ElementAt(i);

                    Microsoft.Phone.Maps.Controls.MapOverlay pointsdata = new Microsoft.Phone.Maps.Controls.MapOverlay();

                    System.Windows.Shapes.Ellipse test = new System.Windows.Shapes.Ellipse();

                    test.Fill = new System.Windows.Media.SolidColorBrush(colors[j % 10]);
                    test.Height = 6;
                    test.Width = 6;
                    test.Opacity = 75;

                    pointsdata.Content = test;

                    System.Device.Location.GeoCoordinate coord = new System.Device.Location.GeoCoordinate(data[0], data[1]);

                    pointsdata.GeoCoordinate = coord;

                    points.Add(pointsdata);
                }
            }

            carte.Layers.Clear();
            
            carte.Layers.Add(points);
        }

    }
}