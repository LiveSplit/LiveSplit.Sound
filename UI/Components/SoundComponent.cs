using LiveSplit.Model;
using NAudio.Wave;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public class SoundComponent : LogicComponent, IDeactivatableComponent
    {
        public LiveSplitState State { get; set; }
        public WaveOut Player { get; set; }
        public SoundSettings Settings { get; set; }
        public bool Activated { get; set; }

        public override string ComponentName
        {
            get { return "Sound Effects"; }
        }

        public SoundComponent(LiveSplitState state)
        {
            Settings = new SoundSettings();
            State = state;
            Player = new WaveOut();
            Activated = true;

            State.OnStart += State_OnStart;
            State.OnSplit += State_OnSplit;
            State.OnSkipSplit += State_OnSkipSplit;
            State.OnUndoSplit += State_OnUndoSplit;
            State.OnPause += State_OnPause;
            State.OnResume += State_OnResume;
            State.OnReset += State_OnReset;
        }

        void State_OnReset(object sender, TimerPhase e)
        {
            if (e != TimerPhase.Ended)
                PlaySound(Settings.Reset, Settings.ResetVolume);
        }

        void State_OnResume(object sender, EventArgs e)
        {
            PlaySound(Settings.Resume, Settings.ResumeVolume);
        }

        void State_OnPause(object sender, EventArgs e)
        {
            PlaySound(Settings.Pause, Settings.PauseVolume);
        }

        void State_OnUndoSplit(object sender, EventArgs e)
        {
            PlaySound(Settings.UndoSplit, Settings.UndoSplitVolume);
        }

        void State_OnSkipSplit(object sender, EventArgs e)
        {
            PlaySound(Settings.SkipSplit, Settings.SkipSplitVolume);
        }

        void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended)
            {
                if (State.Run.Last().PersonalBestSplitTime[State.CurrentTimingMethod] == null || State.Run.Last().SplitTime[State.CurrentTimingMethod] < State.Run.Last().PersonalBestSplitTime[State.CurrentTimingMethod])
                    PlaySound(Settings.PersonalBest, Settings.PersonalBestVolume);
                else
                    PlaySound(Settings.NotAPersonalBest, Settings.NotAPersonalBestVolume);
            }
            else
            {
                var path = Settings.Split;
                int volume = Settings.SplitVolume;
                var newPath = GetSoundPathForSplit(ref volume);

                if (!String.IsNullOrEmpty(newPath))
                    path = newPath;

                PlaySound(path, volume);
            }
        }

        public String GetSoundPathForSplit(ref int volume)
        {
            var splitIndex = State.CurrentSplitIndex - 1;
            var timeDifference = State.Run[splitIndex].SplitTime[State.CurrentTimingMethod] - State.Run[splitIndex].Comparisons[State.CurrentComparison][State.CurrentTimingMethod];
            String soundPath = null;

            if (timeDifference != null)
            {
                if (timeDifference < TimeSpan.Zero)
                {
                    soundPath = Settings.SplitAheadGaining;
                    volume = Settings.SplitAheadGainingVolume;

                    if (LiveSplitStateHelper.GetPreviousSegment(State, splitIndex, false, false, State.CurrentComparison, State.CurrentTimingMethod) > TimeSpan.Zero)
                    {
                        soundPath = Settings.SplitAheadLosing;
                        volume = Settings.SplitAheadLosingVolume;
                    }
                }
                else
                {
                    soundPath = Settings.SplitBehindLosing;
                    volume = Settings.SplitBehindLosingVolume;

                    if (LiveSplitStateHelper.GetPreviousSegment(State, splitIndex, false, false, State.CurrentComparison, State.CurrentTimingMethod) < TimeSpan.Zero)
                    {
                        soundPath = Settings.SplitBehindGaining;
                        volume = Settings.SplitBehindGainingVolume;
                    }
                }
            }

            //Check for best segment
            TimeSpan? curSegment = LiveSplitStateHelper.GetPreviousSegment(State, splitIndex, false, true, State.CurrentComparison, State.CurrentTimingMethod);

            if (curSegment != null)
            {
                if (State.Run[splitIndex].BestSegmentTime[State.CurrentTimingMethod] == null || curSegment < State.Run[splitIndex].BestSegmentTime[State.CurrentTimingMethod])
                {
                    soundPath = Settings.BestSegment;
                    volume = Settings.BestSegmentVolume;
                }
            }

            return soundPath;
        }

        void State_OnStart(object sender, EventArgs e)
        {
            PlaySound(Settings.StartTimer, Settings.StartTimerVolume);
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public override System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public override void SetSettings(System.Xml.XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }

        public void PlaySound(String location, int volume)
        {
            Player.Stop();

            if (Activated && !String.IsNullOrEmpty(location))
            {
                Task.Factory.StartNew(() =>
                {
                    AudioFileReader audioFileReader = new AudioFileReader(location);
                    audioFileReader.Volume = (volume / 100f) * (Settings.GeneralVolume / 100f);

                    Player.DeviceNumber = Settings.OutputDevice;
                    Player.Init(audioFileReader);
                    Player.Play();
                });
            }
        }

        public override void Dispose()
        {
            State.OnStart -= State_OnStart;
            State.OnSplit -= State_OnSplit;
            State.OnSkipSplit -= State_OnSkipSplit;
            State.OnUndoSplit -= State_OnUndoSplit;
            State.OnPause -= State_OnPause;
            State.OnResume -= State_OnResume;
            State.OnReset -= State_OnReset;

            Player.Stop();
        }
    }
}
