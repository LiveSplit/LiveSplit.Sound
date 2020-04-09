using LiveSplit.Model;
using LiveSplit.Options;
using NAudio.Wave;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public class SoundComponent : LogicComponent, IDeactivatableComponent
    {
        public override string ComponentName => "Sound Effects";

        public bool Activated { get; set; }

        private LiveSplitState State { get; set; }
        private SoundSettings Settings { get; set; }
 
        private WaveOut Player { get; set; }

        public SoundComponent(LiveSplitState state)
        {
            Activated = true;

            State = state;
            Settings = new SoundSettings();
            Player = new WaveOut();

            State.OnStart += State_OnStart;
            State.OnSplit += State_OnSplit;
            State.OnSkipSplit += State_OnSkipSplit;
            State.OnUndoSplit += State_OnUndoSplit;
            State.OnPause += State_OnPause;
            State.OnResume += State_OnResume;
            State.OnReset += State_OnReset;
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

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return Settings;
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return Settings.GetSettings(document);
        }

        public override void SetSettings(XmlNode settings)
        {
            Settings.SetSettings(settings);
        }

        private void State_OnStart(object sender, EventArgs e)
        {
            PlaySound(Settings.StartTimer, Settings.StartTimerVolume);
        }

        private enum RunState
        {
            Indetermined,
            AheadGaining,
            AheadLosing,
            BehindGaining,
            BehindLosing,
            BestSegment
        }

        private static RunState GetRunState(LiveSplitState state, int splitIndex)
        {
            if (splitIndex < 0)
            {
                return RunState.Indetermined;
            }

            TimeSpan? curSegment = LiveSplitStateHelper.GetPreviousSegmentTime(state, splitIndex, state.CurrentTimingMethod);

            if (curSegment != null)
            {
                if (state.Run[splitIndex].BestSegmentTime[state.CurrentTimingMethod] == null || curSegment < state.Run[splitIndex].BestSegmentTime[state.CurrentTimingMethod])
                {
                    return RunState.BestSegment;
                }
            }

            var timeDifference = state.Run[splitIndex].SplitTime[state.CurrentTimingMethod] - state.Run[splitIndex].Comparisons[state.CurrentComparison][state.CurrentTimingMethod];

            if (timeDifference == null)
            {
                return RunState.Indetermined;
            }

            if (timeDifference < TimeSpan.Zero)
            {
                if (LiveSplitStateHelper.GetPreviousSegmentDelta(state, splitIndex, state.CurrentComparison, state.CurrentTimingMethod) > TimeSpan.Zero)
                {
                    return RunState.AheadLosing;
                }

                return RunState.AheadGaining;
            }

            if (LiveSplitStateHelper.GetPreviousSegmentDelta(state, splitIndex, state.CurrentComparison, state.CurrentTimingMethod) < TimeSpan.Zero)
            {
                return RunState.BehindGaining;
            }

            return RunState.BehindLosing;
        }

        private void State_OnSplit(object sender, EventArgs e)
        {
            if (State.CurrentPhase == TimerPhase.Ended)
            //if the run is over
            {
                if (State.Run.Last().PersonalBestSplitTime[State.CurrentTimingMethod] == null || State.Run.Last().SplitTime[State.CurrentTimingMethod] < State.Run.Last().PersonalBestSplitTime[State.CurrentTimingMethod])
                    PlaySound(Settings.PersonalBest, Settings.PersonalBestVolume);
                //if PB
                else
                    PlaySound(Settings.NotAPersonalBest, Settings.NotAPersonalBestVolume);
                // not a PB
            }
            else
            {
                var path = string.Empty;
                int volume = Settings.SplitVolume;

                var splitIndex = State.CurrentSplitIndex - 1;
                var curRunState = GetRunState(State, splitIndex);
                var prevRunState = GetRunState(State, splitIndex - 1);

                if (curRunState == prevRunState && curRunState != RunState.BestSegment && Settings.IsSituationModeChecked())
                {
                    return; // pas de son du tout
                }

                switch (curRunState)
                {
                    case RunState.AheadGaining:
                        path = Settings.SplitAheadGaining;
                        volume = Settings.SplitAheadGainingVolume;
                        break;

                    case RunState.AheadLosing:
                        path = Settings.SplitAheadLosing;
                        volume = Settings.SplitAheadLosingVolume;
                        break;

                    case RunState.BehindGaining:
                        path = Settings.SplitBehindGaining;
                        volume = Settings.SplitBehindGainingVolume;
                        break;

                    case RunState.BehindLosing:
                        path = Settings.SplitBehindLosing;
                        volume = Settings.SplitBehindLosingVolume;
                        break;

                    case RunState.BestSegment:
                        path = Settings.BestSegment;
                        volume = Settings.BestSegmentVolume;
                        break;

                    default: break; //do nothing by default
                }

                if (string.IsNullOrEmpty(path)) path = Settings.Split;

                PlaySound(path, volume);
            }
        }

        private void State_OnSkipSplit(object sender, EventArgs e)
        {
            PlaySound(Settings.SkipSplit, Settings.SkipSplitVolume);
        }

        private void State_OnUndoSplit(object sender, EventArgs e)
        {
            PlaySound(Settings.UndoSplit, Settings.UndoSplitVolume);
        }

        private void State_OnPause(object sender, EventArgs e)
        {
            PlaySound(Settings.Pause, Settings.PauseVolume);
        }

        private void State_OnResume(object sender, EventArgs e)
        {
            PlaySound(Settings.Resume, Settings.ResumeVolume);
        }

        private void State_OnReset(object sender, TimerPhase e)
        {
            if (e != TimerPhase.Ended)
                PlaySound(Settings.Reset, Settings.ResetVolume);
        }

        private void PlaySound(string location, int volume)
        {
            Player.Stop();

            if (Activated && File.Exists(location))
            {
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        AudioFileReader audioFileReader = new AudioFileReader(location);
                        audioFileReader.Volume = (volume / 100f) * (Settings.GeneralVolume / 100f);

                        Player.DeviceNumber = Settings.OutputDevice;
                        Player.Init(audioFileReader);
                        Player.Play();
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                });
            }
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();
    }
}
