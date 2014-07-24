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
    public class SoundComponent : LogicComponent
    {
        public LiveSplitState State { get; set; }
        public MediaPlayer.IMediaPlayer Player { get; set; }
        public SoundSettings Settings { get; set; }


        public override string ComponentName
        {
            get { return "Sound Effects"; }
        }

        public SoundComponent(LiveSplitState state)
        {
            Settings = new SoundSettings();
            State = state;
            Player = new MediaPlayer.MediaPlayer();

            State.OnStart += State_OnStart;
            State.OnSplit += State_OnSplit;
            State.OnSkipSplit += State_OnSkipSplit;
            State.OnUndoSplit += State_OnUndoSplit;
            State.OnPause += State_OnPause;
            State.OnResume += State_OnResume;
            State.OnReset += State_OnReset;

            Settings.Split = @"D:\Musik\Speedrun Sounds\Time.mp3";
            Settings.Reset = @"D:\Musik\Speedrun Sounds\Well_shit.mp3";
        }

        void State_OnReset(object sender, EventArgs e)
        {
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
                PlaySound(Settings.PersonalBest);
            }
            else
            {
                PlaySound(Settings.Split);
            }
        }

        void State_OnStart(object sender, EventArgs e)
        {
            PlaySound(Settings.StartTimer);
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return null;
        }

        public override System.Xml.XmlNode GetSettings(System.Xml.XmlDocument document)
        {
            return document.CreateElement("x");
        }

        public override void SetSettings(System.Xml.XmlNode settings)
        {
        }

        public override void RenameComparison(string oldName, string newName)
        {
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
        }

        public void PlaySound(String location)
        {
            if (!String.IsNullOrEmpty(location))
            {
                Task.Factory.StartNew(() =>
                {
                    Player.Open(location);
                });
            }
        }
    }
}
