using LiveSplit.Model;
using LiveSplit.Model.Comparisons;
using LiveSplit.TimeFormatters;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiveSplit.UI.Components
{
    public class SoundComponent : LogicComponent, IDeactivatableComponent
    {
        public LiveSplitState State { get; set; }
        public MediaPlayer.IMediaPlayer Player { get; set; }
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
            Player = new MediaPlayer.MediaPlayer();
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
                PlaySound(Settings.Reset);
        }

        void State_OnResume(object sender, EventArgs e)
        {
            PlaySound(Settings.Resume);
        }

        void State_OnPause(object sender, EventArgs e)
        {
            PlaySound(Settings.Pause);
        }

        void State_OnUndoSplit(object sender, EventArgs e)
        {
            PlaySound(Settings.UndoSplit);
        }

        void State_OnSkipSplit(object sender, EventArgs e)
        {
            PlaySound(Settings.SkipSplit);
        }

        void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended)
            {
                if (State.Run.Last().PersonalBestSplitTime[State.CurrentTimingMethod] == null
                    || State.Run.Last().SplitTime[State.CurrentTimingMethod] < State.Run.Last().PersonalBestSplitTime[State.CurrentTimingMethod])
                    PlaySound(Settings.PersonalBest);
                else
                    PlaySound(Settings.NotAPersonalBest);
            }
            else
            {
                var path = Settings.Split;
                var newPath = GetSoundPathForSplit();
                if (!String.IsNullOrEmpty(newPath))
                    path = newPath;
                PlaySound(path);
            }
        }

        public String GetSoundPathForSplit()
        {
            var splitIndex = State.CurrentSplitIndex - 1;
            var timeDifference = State.Run[State.CurrentSplitIndex - 1].SplitTime[State.CurrentTimingMethod] - State.Run[State.CurrentSplitIndex - 1].Comparisons[State.CurrentComparison][State.CurrentTimingMethod];
            String soundPath = null;
            if (timeDifference != null)
            {
                if (timeDifference < TimeSpan.Zero)
                {
                    soundPath = Settings.SplitAheadGaining;
                    if (LiveSplitStateHelper.GetPreviousSegmentDelta(State, splitIndex, State.CurrentComparison, State.CurrentTimingMethod) > TimeSpan.Zero)
                        soundPath = Settings.SplitAheadLosing;
                }
                else
                {
                    soundPath = Settings.SplitBehindLosing;
                    if (LiveSplitStateHelper.GetPreviousSegmentDelta(State, splitIndex, State.CurrentComparison, State.CurrentTimingMethod) < TimeSpan.Zero)
                        soundPath = Settings.SplitBehindGaining;
                }
            }
            //Check for best segment
            TimeSpan? curSegment;
            curSegment = LiveSplitStateHelper.GetPreviousSegmentTime(State, splitIndex, State.CurrentComparison, State.CurrentTimingMethod);
            if (curSegment != null)
            {
                if (State.Run[splitIndex].BestSegmentTime[State.CurrentTimingMethod] == null || curSegment < State.Run[splitIndex].BestSegmentTime[State.CurrentTimingMethod])
                    soundPath = Settings.BestSegment;
            }
            return soundPath;
        }

        void State_OnStart(object sender, EventArgs e)
        {
            PlaySound(Settings.StartTimer);
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

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
        }

        public void PlaySound(String location)
        {
            if (Activated && !String.IsNullOrEmpty(location))
            {
                Task.Factory.StartNew(() =>
                {
                    Player.Open(location);
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
