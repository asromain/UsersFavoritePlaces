using System;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Device.Location;
using System.Threading;
using System.IO;

namespace DevMobileProject
{
    public partial class MapControl : PhoneApplicationPage
    {

        GeoCoordinateWatcher watcher;
        bool trackingOn = false;
        FileWriter fw;
        // pushpin

        public MapControl()
        {
            InitializeComponent();

            // initialisation du watcher, accuracy level + mouvement threshold
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            watcher.MovementThreshold = 10.0f;
            watcher.StatusChanged += new EventHandler<GeoPositionStatusChangedEventArgs>(watcher_StatusChanged);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

            // start up LocServ in bg; watcher_StatusChanged sera appelé une fois fini
            new Thread(startLocServInBackground).Start();
            statusTextBlock.Text = "Starting Location Service...";

            fw = new FileWriter("Data/trackingdata.txt");

        }



        void watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    if (watcher.Permission == GeoPositionPermission.Denied)
                    {
                        statusTextBlock.Text = "Location service est desactive";
                    }
                    else
                    {
                        statusTextBlock.Text = "Location service ne fonctionne pas";
                    }
                    break;
                case GeoPositionStatus.Initializing:
                    statusTextBlock.Text = "Location service recupere les donnees...";
                    break;
                case GeoPositionStatus.NoData:
                    statusTextBlock.Text = "Pas de donnee de location dispo";
                    break;
                case GeoPositionStatus.Ready:
                    statusTextBlock.Text = "Location data est disponible";
                    break;
            }
        }

        void startLocServInBackground()
        {
            watcher.TryStart(true, TimeSpan.FromMilliseconds(60000));
        }

        private void trackMe_Click(object sender, RoutedEventArgs e)
        {
            if (trackingOn)
            {
                trackMe.Content = "Track me on map";
                trackingOn = false;
                carte.ZoomLevel = 1.0f;
            }
            else
            {
                trackMe.Content = "Stop tracking";
                trackingOn = true;
                carte.ZoomLevel = 16.0f;
            }
        }

        private void startStop_Click(object sender, RoutedEventArgs e)
        {
            if (startStop.Content.Equals("Stop LocServ"))
            {
                startStop.Content = "Start LocServ";
                statusTextBlock.Text = "Location services stopped...";
                watcher.Stop();
            }
            else if (startStop.Content.ToString().Equals("Start LocServ"))
            {
                startStop.Content = "Stop LocServ";
                statusTextBlock.Text = "Starting Location Services...";
                new Thread(startLocServInBackground).Start();
            }
        }

        void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            // update textblock readouts
            latitudeTextBlock.Text = e.Position.Location.Latitude.ToString("0.0000000000000");
            longitudeTextBlock.Text = e.Position.Location.Longitude.ToString("0.0000000000000");
            speedreadout.Text = e.Position.Location.Speed.ToString("0.0") + "m/s";
            coursereadout.Text = e.Position.Location.Course.ToString("0.0");
            altitudereadout.Text = e.Position.Location.Altitude.ToString("0.0");

            // maj de la map si l'user a demandé a etre traqué
            if (trackingOn)
            {
                // pushpin
                carte.Center = e.Position.Location;
            }

            // write datatracking in file
            fw.add(e.Position.Location.Latitude.ToString("0.0000000000000") + "," + e.Position.Location.Longitude.ToString("0.0000000000000"));
            StreamReader sdf = new StreamReader("Data/trackingdata.txt");
            System.Diagnostics.Debug.WriteLine(sdf.ReadLine());

        }
    }
}