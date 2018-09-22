using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace MyStopwatch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartButton.Content as string == "Start")
            {
                stopWatch.Start();
                dispatcherTimer.Start();
                ResetButton.IsEnabled = false;
                StartButton.Content = "Pause";
            }
            else // (StartButton.Content as string == "Pause") — bad condition, cuz it always triggering after pressing start
            {
                stopWatch.Stop();
                ResetButton.IsEnabled = true;
                StartButton.Content = "Start";
            }
        }

        void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Stopwatch.Content = currentTime;
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            stopWatch.Reset();
            Stopwatch.Content = "00:00:00.000";
            LapsList.Items.Clear();
        }

        private void LapButton_Click(object sender, RoutedEventArgs e)
        {
            var lap = new ListBoxItem();
            TimeSpan ts = stopWatch.Elapsed;
            currentTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            lap.Content = currentTime;

            // TODO: FIX THIS SHIT
            var toAdd = $"{LapsList.Items.Count - 1}. " + lap.Content;
            if (!LapsList.Items.Contains(toAdd))
                LapsList.Items.Add($"{LapsList.Items.Count}. " + lap.Content);
        }
    }
}
