using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace MyStopwatch
{
    public class ViewModel : INotifyPropertyChanged
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Stopwatch stopWatch = new Stopwatch();

        private string currentTime;
        public string CurrentTime
        {
            get => currentTime;
            set
            {
                currentTime = value;
                OnPropertyChanged();
                AddLap.RaiseCanExecuteChanged();
            }
        }

        private string startPauseButtonText;
        public string StartPauseButtonText
        {
            get => startPauseButtonText;
            set
            {
                startPauseButtonText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Lap> Laps { get; set; }

        public ViewModel()
        {
            Laps = new ObservableCollection<Lap>();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            CurrentTime = "00:00:00.000";
            StartPauseButtonText = "Start";
        }

        void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                var elapsed = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                    ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                CurrentTime = elapsed;
            }
            
        }

        /// <summary>
        /// Adding a new lap to list
        /// </summary>
        private RelayCommand addLap;
        public RelayCommand AddLap
        {
            get
            {
                return addLap ??
                    (addLap = new RelayCommand(obj =>
                    {
                        string time = obj as string;
                        Lap newLap = new Lap(time, Laps.Count + 1);
                        Laps.Add(newLap);
                    },
                    (obj) =>
                    {
                        if (Laps.Count == 0)
                            return true;
                        string time = obj as string;
                        Lap newLap = new Lap(time, Laps.Count);
                        return newLap.LapEntry != Laps.Last().LapEntry;
                    }
                    ));
            }
        }

        /// <summary>
        /// Starting and pausing the stopwatch
        /// </summary>
        private RelayCommand startCommand;
        public RelayCommand StartCommand
        {
            get
            {
                return startCommand ??
                    (startCommand = new RelayCommand(obj =>
                    {
                        if (!stopWatch.IsRunning)
                        {
                            stopWatch.Start();
                            dispatcherTimer.Start();
                            StartPauseButtonText = "Pause";
                        }
                        else
                        {
                            stopWatch.Stop();
                            dispatcherTimer.Stop();
                            StartPauseButtonText = "Resume";
                        }
                    },
                    (obj) => true
                    ));
            }
        }

        /// <summary>
        /// Resetting stopwatch
        /// </summary>
        private RelayCommand resetCommand;
        public RelayCommand ResetCommand
        {
            get
            {
                return resetCommand ??
                    (resetCommand = new RelayCommand(obj =>
                    {
                        stopWatch.Reset();
                        CurrentTime = "00:00:00.000";
                        StartPauseButtonText = "Start";
                        Laps.Clear();
                    },
                    (obj) => CurrentTime != "00:00:00.000"
                    ));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
