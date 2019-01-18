using Gma.UserActivityMonitor;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RunescapeHelper.Modules.AutoClicker
{
    public partial class AutoClickerMainForm : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;

        private enum EventTypes {
            Mouse = 1,
            Key = 2
        };
        
        private int playbackLoopAmount = 0;

        private bool playbackActive = false;
        private bool recordingActive = false;
        private bool suspensionActive = false;
        private bool runInfinitely = false;
        private bool setPlaybackActive = false;

        private bool isShiftDown = false;
        private bool isControlDown = false;
        private bool isAltDown = false;

        private System.Windows.Forms.Timer recordingTimer = new System.Windows.Forms.Timer();
        private Stopwatch recordingStopwatch = new Stopwatch();
        private Stopwatch playbackStopWatch = new Stopwatch();
        private Stopwatch autoChatterStopwatch = new Stopwatch();
        private BackgroundWorker playbackThread = new BackgroundWorker();

        private Recording currentRecording = new Recording();
        private RecordingSet currentRecordingSet = new RecordingSet();

        public AutoClickerMainForm()
        {
            InitializeComponent();

            // Default Run Infinitely? Checkbox to true
            runInfinitelyCheckBox.Checked = true;

            // Initialize timer attributes
            recordingTimer.Interval = 10;
            recordingTimer.Tick += new EventHandler(timerTick);

            // Initialize Auto Chatter Stopwatch
            autoChatterStopwatch.Start();

            // Initalize background worker attributes
            playbackThread.DoWork += PlaybackRecording;
            playbackThread.WorkerReportsProgress = true;
            playbackThread.WorkerSupportsCancellation = true;

            // Hook MouseDown and KeyDown functions
            HookManager.MouseDown += HandleMouseDown;
            HookManager.KeyDown += HandleKeyDown;
            HookManager.KeyUp += HandleKeyUp;
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (!recordingActive)
            {
                return;
            }

            var leftClick = true;
            if (e.Button == MouseButtons.Right)
            {
                leftClick = false;
            }

            LogMouseDownEvent((int)EventTypes.Mouse, e.X, e.Y, leftClick, recordingStopwatch.ElapsedMilliseconds);
        }

        private void LogMouseDownEvent(int eventType, int xCoord, int yCoord, bool leftClick, long time)
        {
            var timeSpan = TimeSpan.FromMilliseconds(time);
            currentRecording.events.Add(new Event()
            {
                eventType = eventType,
                x = xCoord,
                y = yCoord,
                leftClick = leftClick,
                time = time,
                shiftModifier = isShiftDown,
                controlModifier = isControlDown,
                altModifier = isAltDown
            });

            string action = leftClick ? "Left Click" : "Right Click";
            var eventLogItem = new ListViewItem(action)
            {
                SubItems =
                {
                    $"({xCoord},{yCoord})",
                    $"{timeSpan.Minutes}:{timeSpan.Seconds}:{timeSpan.Milliseconds}"
                }
            };

            eventLog.Items.Add(eventLogItem);
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    StartCurrentRecordingPlayback();
                    return;
                case Keys.F2:
                    EndCurrentRecordingPlayback();
                    return;
                case Keys.F3:
                    SuspendCurrentRecordingPlayback();
                    return;
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    isShiftDown = true;
                    return;
                case Keys.LControlKey:
                case Keys.RControlKey:
                    isControlDown = true;
                    return;
                case Keys.Alt:
                    isAltDown = true;
                    return;
            }

            if (!recordingActive)
            {
                return;
            }

            LogKeyDownEvent((int)EventTypes.Key, (int)e.KeyCode, e.KeyCode.ToString(), recordingStopwatch.ElapsedMilliseconds);
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (!recordingActive)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    isShiftDown = false;
                    return;
                case Keys.LControlKey:
                case Keys.RControlKey:
                    isControlDown = false;
                    return;
                case Keys.Alt:
                    isAltDown = false;
                    return;
            }
        }

        private void LogKeyDownEvent(int eventType, int keyCode, string keyLabel, long time)
        {
            var timeSpan = TimeSpan.FromMilliseconds(time);
            currentRecording.events.Add(new Event()
            {
                eventType = eventType,
                keyCode = keyCode,
                keyLabel = keyLabel,
                time = time
            });

            var eventLogItem = new ListViewItem("Key Press")
            {
                SubItems =
                {
                    $"{keyLabel}",
                    $"{timeSpan.Minutes}:{timeSpan.Seconds}:{timeSpan.Milliseconds}",
                    "No"
                }
            };

            eventLog.Items.Add(eventLogItem);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            recordingTimer.Start();
            recordingStopwatch.Start();
            recordingActive = true;

            eventLog.Items.Clear();
            currentRecording = new Recording();
        }

        private void endButton_Click(object sender, EventArgs e)
        {
            if (!recordingActive)
            {
                return;
            }

            recordingActive = false;

            // Remove the click recorded for ending the recording
            currentRecording.events.RemoveAt(currentRecording.events.Count - 1);
            eventLog.Items.RemoveAt(eventLog.Items.Count - 1);

            recordingTimer.Stop();
            recordingStopwatch.Reset();
        }

        private void timerTick(object sender, EventArgs e)
        {
            var elapsed = recordingStopwatch.Elapsed;
            timerText.Text = $"{elapsed.Minutes}:{elapsed.Seconds}:{elapsed.Milliseconds}";
        }

        private void mouseClick(int x, int y, bool leftClick, bool shiftModifier, bool controlModifier, bool altModifier)
        {
            Cursor.Position = new Point(x, y);

            if (shiftModifier)
            {
                keybd_event(0x10, 0, 0, 0);
            }

            if (controlModifier)
            {
                keybd_event(0xA2, 0, 0, 0);
            }

            if (altModifier)
            {
                keybd_event(0xA4, 0, 0, 0);
            }

            Thread.Sleep(new Random().Next(75, 100));

            if (leftClick)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            }
            else
            {
                mouse_event(MOUSEEVENTF_RIGHTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                mouse_event(MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            }

            // Release Keys

            Thread.Sleep(new Random().Next(75, 100));
            if (shiftModifier)
            {
                keybd_event(0x10, 0, 0x2, 0); // 0x2 using in dwFlags parameter to signify key is being released
            }

            if (controlModifier)
            {
                keybd_event(0xA2, 0, 0x2, 0);
            }

            if (altModifier)
            {
                keybd_event(0xA4, 0, 0x2, 0);
            }
        }

        private void keyClick(int keyCode)
        {
            keybd_event(Convert.ToByte(keyCode), 0, 0, 0);
        }

        private void StartCurrentRecordingPlayback()
        {
            if (playbackActive)
            {
                return;
            }

            playbackActive = true;

            if (!string.IsNullOrEmpty(loopCounterText.Text))
            {
                playbackLoopAmount = int.Parse(loopCounterText.Text);
            }

            playbackThread.RunWorkerAsync();
        }

        private void PlaybackRecording(object sender, DoWorkEventArgs e)
        {
            playbackStopWatch = new Stopwatch();

            while (true)
            {
                try
                {
                    // If running set playback, set new currentRecording
                    if (setPlaybackActive)
                    {
                        var random = new Random();
                        currentRecording = currentRecordingSet.recordings[random.Next(currentRecordingSet.recordings.Count)];
                    }

                    Invoke((MethodInvoker)delegate
                    {
                        progressBar.Maximum = (int)currentRecording.events.Last().time;
                    });

                    var i = 0;
                    playbackStopWatch.Restart();

                    while (i < currentRecording.events.Count)
                    {
                        var recordingEvent = currentRecording.events[i];

                        Invoke((MethodInvoker)delegate
                        {
                            UpdateProgressBar((int)playbackStopWatch.ElapsedMilliseconds);
                        });

                        if (playbackThread.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }

                        if (recordingEvent.time <= playbackStopWatch.ElapsedMilliseconds)
                        {
                            // Stop the stopwatch while click is performed since stopwatch continues running in the background
                            playbackStopWatch.Stop();

                            if (recordingEvent.eventType == (int)EventTypes.Mouse)
                            {
                                mouseClick(recordingEvent.x, recordingEvent.y, recordingEvent.leftClick, recordingEvent.shiftModifier, recordingEvent.controlModifier, recordingEvent.altModifier);
                            }
                            else if (recordingEvent.eventType == (int)EventTypes.Key)
                            {
                                keyClick(recordingEvent.keyCode);
                            }

                            var nextEvent = i == currentRecording.events.Count - 1 ? null : currentRecording.events[i + 1];
                            if (nextEvent != null)
                            {
                                var downTime = nextEvent.time - playbackStopWatch.ElapsedMilliseconds;

                                playbackStopWatch.Start();
                                Thread.Sleep((int)downTime);
                            }
                            else
                            {
                                playbackStopWatch.Start();
                            }

                            i++;
                        }
                    }

                    if (!runInfinitely)
                    {
                        playbackLoopAmount--;
                        if (playbackLoopAmount == 0)
                        {
                            EndCurrentRecordingPlayback();
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Exception: {ex.Message}");
                }
            }
        }

        public void UpdateLoopCounterTextBox(string text)
        {
            Invoke((MethodInvoker)delegate {
                loopCounterText.Text = text;
            });
        }

        public void UpdateProgressBar(int time)
        {
            if (progressBar.Maximum <= time)
            {
                return;
            }

            Invoke((MethodInvoker)delegate
            {
                progressBar.Value = time;
            });
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            currentRecording.events.Clear();
            eventLog.Items.Clear();
        }

        private void EndCurrentRecordingPlayback()
        {
            if (!playbackActive)
            {
                return;
            }

            playbackActive = false;
            playbackThread.CancelAsync();
        }

        private void runInfinitelyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (runInfinitelyCheckBox.Checked)
            {
                runInfinitely = true;
                loopCounterText.Text = null;
                loopCounterText.ReadOnly = true;
            }
            else
            {
                runInfinitely = false;
                loopCounterText.ReadOnly = false;
            }
        }

        private void SuspendCurrentRecordingPlayback()
        {
            if (!playbackActive)
            {
                return;
            }

            if (suspensionActive)
            {
                // Resume Playback
                playbackStopWatch.Start();
                suspendToolStripMenuItem.Text = "Suspend Playback (F3)";
                suspensionActive = false;
            }
            else
            {
                // Suspend Playback
                playbackStopWatch.Stop();
                suspendToolStripMenuItem.Text = "Resume Playback (F3)";
                suspensionActive = true;
            }
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AutoClickerMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            EndCurrentRecordingPlayback();
            MainForm.mainForm.Show();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            var fileName = openFileDialog1.FileName;

            var recordingSet = new RecordingSet();
            recordingSet.recordings = JsonConvert.DeserializeObject<List<Recording>>(File.ReadAllText(fileName));
            currentRecordingSet = recordingSet;

            foreach (var item in viewToolStripMenuItem.DropDownItems)
            {
                viewToolStripMenuItem.DropDownItems.RemoveByKey(((ToolStripMenuItem)item).Name);
            }

            var count = 1;
            foreach (var recording in recordingSet.recordings)
            {
                var newDropDownItem = new ToolStripMenuItem()
                {
                    Text = $"Recording #{count}",
                    Name = recording.id.ToString()
                };
                newDropDownItem.Click += LoadRecording;

                viewToolStripMenuItem.DropDownItems.Add(newDropDownItem);
                count++;
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog()
            {
                FileName = "playbackset.json",
                Filter = "Json files (*.json)|*.json"
            };

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(savefile.FileName))
                {
                    sw.Write(JsonConvert.SerializeObject(currentRecordingSet.recordings));
                }
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartCurrentRecordingPlayback();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndCurrentRecordingPlayback();
        }

        private void suspendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuspendCurrentRecordingPlayback();
        }

        private void addToSetButton_Click(object sender, EventArgs e)
        {
            currentRecordingSet.recordings.Add(currentRecording);

            var newDropDownItem = new ToolStripMenuItem()
            {
                Text = $"Recording #{currentRecordingSet.recordings.Count}",
                Name = currentRecording.id.ToString()
            };
            newDropDownItem.Click += LoadRecording;

            viewToolStripMenuItem.DropDownItems.Add(newDropDownItem);
        }

        private void LoadRecording(object sender, EventArgs e)
        {
            var recordingId = new Guid(((ToolStripMenuItem)sender).Name);

            var recording = currentRecordingSet.recordings.Where(x => x.id.Equals(recordingId)).First();
            currentRecording = recording;

            eventLog.Items.Clear();
            foreach (var importedEvent in recording.events)
            {
                var timeSpan = TimeSpan.FromMilliseconds(importedEvent.time);

                string action = importedEvent.leftClick ? "Left Click" : "Right Click";
                var eventLogItem = new ListViewItem(action)
                {
                    SubItems =
                {
                    $"({importedEvent.x},{importedEvent.y})",
                    $"{timeSpan.Minutes}:{timeSpan.Seconds}:{timeSpan.Milliseconds}"
                }
                };

                eventLog.Items.Add(eventLogItem);
            }
        }

        private void playbackFromSet_CheckedChanged(object sender, EventArgs e)
        {
            setPlaybackActive = ((CheckBox)sender).Checked;
        }

        private void removeRecordingFromSet_Click(object sender, EventArgs e)
        {
            if (currentRecording == null || currentRecording.id == null)
            {
                return;
            }

            viewToolStripMenuItem.DropDownItems.RemoveByKey(currentRecording.id.ToString());

            var count = 1;
            foreach (var item in viewToolStripMenuItem.DropDownItems)
            {
                ((ToolStripMenuItem)item).Text = $"Recording #{count}";
                count++;
            }
        }
    }

    public class RecordingSet
    {
        public List<Recording> recordings;

        public RecordingSet()
        {
            recordings = new List<Recording>();
        }
    }

    public class Recording
    {
        public List<Event> events;
        public Guid id;

        public Recording()
        {
            events = new List<Event>();
            id = Guid.NewGuid();
        }
    }

    public class Event
    {
        private enum EventTypes
        {
            Mouse = 1,
            Key = 2
        };

        public int eventType; // Click = 1, Key = 2
        public int x;
        public int y;
        public int keyCode;
        public string keyLabel;
        public bool leftClick;
        public long time;
        public bool shiftModifier;
        public bool controlModifier;
        public bool altModifier;

        public static Event FromCsv(string line)
        {
            string[] values = line.Split(',');
            Event eventRecord = new Event()
            {
                eventType = Convert.ToInt32(values[0])
            };

            if (eventRecord.eventType == (int)EventTypes.Mouse)
            {
                eventRecord.x = Convert.ToInt32(values[1]);
                eventRecord.y = Convert.ToInt32(values[2]);
                eventRecord.leftClick = Convert.ToBoolean(values[3]);
                eventRecord.time = Convert.ToInt64(values[4]);
                eventRecord.shiftModifier = Convert.ToBoolean(values[5]);
                eventRecord.controlModifier = Convert.ToBoolean(values[6]);
                eventRecord.altModifier = Convert.ToBoolean(values[7]);
            }
            else if (eventRecord.eventType == (int)EventTypes.Key)
            {
                eventRecord.keyCode = Convert.ToInt32(values[1]);
                eventRecord.keyLabel = Convert.ToString(values[2]);
                eventRecord.time = Convert.ToInt32(values[3]);
            }

            return eventRecord;
        }
    }
}
