using NAudio.Wave;
using System;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class SoundSettings : UserControl
    {
        public string[] Split { get; set; }
        public string SplitText { get { return string.Join(",", Split); } set { Split = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] SplitAheadGaining { get; set; }
        public string SplitAheadGainingText { get { return string.Join(",", SplitAheadGaining); } set { SplitAheadGaining = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] SplitAheadLosing { get; set; }
        public string SplitAheadLosingText { get { return string.Join(",", SplitAheadLosing); } set { SplitAheadLosing = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] SplitBehindGaining { get; set; }
        public string SplitBehindGainingText { get { return string.Join(",", SplitBehindGaining); } set { SplitBehindGaining = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] SplitBehindLosing { get; set; }
        public string SplitBehindLosingText { get { return string.Join(",", SplitBehindLosing); } set { SplitBehindLosing = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] BestSegment { get; set; }
        public string BestSegmentText { get { return string.Join(",", BestSegment); } set { BestSegment = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] UndoSplit { get; set; }
        public string UndoSplitText { get { return string.Join(",", UndoSplit); } set { UndoSplit = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] SkipSplit { get; set; }
        public string SkipSplitText { get { return string.Join(",", SkipSplit); } set { SkipSplit = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] PersonalBest { get; set; }
        public string PersonalBestText { get { return string.Join(",", PersonalBest); } set { PersonalBest = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] NotAPersonalBest { get; set; }
        public string NotAPersonalBestText { get { return string.Join(",", NotAPersonalBest); } set { NotAPersonalBest = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] Reset { get; set; }
        // Uses different name because ResetText hides Control.ResetText otherwise.
        public string ResetSoundText { get { return string.Join(",", Reset); } set { Reset = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] Pause { get; set; }
        public string PauseText { get { return string.Join(",", Pause); } set { Pause = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);  } }
        public string[] Resume { get; set; }
        public string ResumeText { get { return string.Join(",", Resume); } set { Resume = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }
        public string[] StartTimer { get; set; }
        public string StartTimerText { get { return string.Join(",", StartTimer); } set { StartTimer = value.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries); } }

        public int OutputDevice { get; set; }

        public int GeneralVolume { get; set; }
        public int SplitVolume { get; set; }
        public int SplitAheadGainingVolume { get; set; }
        public int SplitAheadLosingVolume { get; set; }
        public int SplitBehindGainingVolume { get; set; }
        public int SplitBehindLosingVolume { get; set; }
        public int BestSegmentVolume { get; set; }
        public int UndoSplitVolume { get; set; }
        public int SkipSplitVolume { get; set; }
        public int PersonalBestVolume { get; set; }
        public int NotAPersonalBestVolume { get; set; }
        public int ResetVolume { get; set; }
        public int PauseVolume { get; set; }
        public int ResumeVolume { get; set; }
        public int StartTimerVolume { get; set; }

        public SoundSettings()
        {
            InitializeComponent();

            Split =
            SplitAheadGaining =
            SplitAheadLosing =
            SplitBehindGaining =
            SplitBehindLosing =
            BestSegment =
            UndoSplit =
            SkipSplit =
            PersonalBest =
            NotAPersonalBest =
            Reset =
            Pause =
            Resume =
            StartTimer = new string[] { "" };

            OutputDevice = 0;

            GeneralVolume =
            SplitVolume =
            SplitAheadGainingVolume =
            SplitAheadLosingVolume =
            SplitBehindGainingVolume =
            SplitBehindLosingVolume =
            BestSegmentVolume =
            UndoSplitVolume =
            SkipSplitVolume =
            PersonalBestVolume =
            NotAPersonalBestVolume =
            ResetVolume =
            PauseVolume =
            ResumeVolume =
            StartTimerVolume = 100;

            for (int i = 0; i < WaveOut.DeviceCount; ++i)
                cbOutputDevice.Items.Add(WaveOut.GetCapabilities(i));

            txtSplitPath.DataBindings.Add("Text", this, "SplitText");
            txtSplitAheadGaining.DataBindings.Add("Text", this, "SplitAheadGainingText");
            txtSplitAheadLosing.DataBindings.Add("Text", this, "SplitAheadLosingText");
            txtSplitBehindGaining.DataBindings.Add("Text", this, "SplitBehindGainingText");
            txtSplitBehindLosing.DataBindings.Add("Text", this, "SplitBehindLosingText");
            txtBestSegment.DataBindings.Add("Text", this, "BestSegmentText");
            txtUndo.DataBindings.Add("Text", this, "UndoSplitText");
            txtSkip.DataBindings.Add("Text", this, "SkipSplitText");
            txtPersonalBest.DataBindings.Add("Text", this, "PersonalBestText");
            txtNotAPersonalBest.DataBindings.Add("Text", this, "NotAPersonalBestText");
            txtReset.DataBindings.Add("Text", this, "ResetSoundText");
            txtPause.DataBindings.Add("Text", this, "PauseText");
            txtResume.DataBindings.Add("Text", this, "ResumeText");
            txtStartTimer.DataBindings.Add("Text", this, "StartTimerText");

            cbOutputDevice.DataBindings.Add("SelectedIndex", this, "OutputDevice");

            tbGeneralVolume.DataBindings.Add("Value", this, "GeneralVolume");
            tbSplitVolume.DataBindings.Add("Value", this, "SplitVolume");
            tbSplitAheadGainingVolume.DataBindings.Add("Value", this, "SplitAheadGainingVolume");
            tbSplitAheadLosingVolume.DataBindings.Add("Value", this, "SplitAheadLosingVolume");
            tbSplitBehindGainingVolume.DataBindings.Add("Value", this, "SplitBehindGainingVolume");
            tbSplitBehindLosingVolume.DataBindings.Add("Value", this, "SplitBehindLosingVolume");
            tbBestSegmentVolume.DataBindings.Add("Value", this, "BestSegmentVolume");
            tbUndoVolume.DataBindings.Add("Value", this, "UndoSplitVolume");
            tbSkipVolume.DataBindings.Add("Value", this, "SkipSplitVolume");
            tbPersonalBestVolume.DataBindings.Add("Value", this, "PersonalBestVolume");
            tbNotAPersonalBestVolume.DataBindings.Add("Value", this, "NotAPersonalBestVolume");
            tbResetVolume.DataBindings.Add("Value", this, "ResetVolume");
            tbPauseVolume.DataBindings.Add("Value", this, "PauseVolume");
            tbResumeVolume.DataBindings.Add("Value", this, "ResumeVolume");
            tbStartTimerVolume.DataBindings.Add("Value", this, "StartTimerVolume");
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            Split = SettingsHelper.ParseString(element["Split"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            SplitAheadGaining = SettingsHelper.ParseString(element["SplitAheadGaining"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            SplitAheadLosing = SettingsHelper.ParseString(element["SplitAheadLosing"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            SplitBehindGaining = SettingsHelper.ParseString(element["SplitBehindGaining"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            SplitBehindLosing = SettingsHelper.ParseString(element["SplitBehindLosing"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            BestSegment = SettingsHelper.ParseString(element["BestSegment"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            UndoSplit = SettingsHelper.ParseString(element["UndoSplit"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            SkipSplit = SettingsHelper.ParseString(element["SkipSplit"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            PersonalBest = SettingsHelper.ParseString(element["PersonalBest"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            NotAPersonalBest = SettingsHelper.ParseString(element["NotAPersonalBest"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            Reset = SettingsHelper.ParseString(element["Reset"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            Pause = SettingsHelper.ParseString(element["Pause"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            Resume = SettingsHelper.ParseString(element["Resume"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);
            StartTimer = SettingsHelper.ParseString(element["StartTimer"]).Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries);

            OutputDevice = SettingsHelper.ParseInt(element["OutputDevice"]);

            SplitVolume = SettingsHelper.ParseInt(element["SplitVolume"], 100);
            SplitAheadGainingVolume = SettingsHelper.ParseInt(element["SplitAheadGainingVolume"], 100);
            SplitAheadLosingVolume = SettingsHelper.ParseInt(element["SplitAheadLosingVolume"], 100);
            SplitBehindGainingVolume = SettingsHelper.ParseInt(element["SplitBehindGainingVolume"], 100);
            SplitBehindLosingVolume = SettingsHelper.ParseInt(element["SplitBehindLosingVolume"], 100);
            BestSegmentVolume = SettingsHelper.ParseInt(element["BestSegmentVolume"], 100);
            UndoSplitVolume = SettingsHelper.ParseInt(element["UndoSplitVolume"], 100);
            SkipSplitVolume = SettingsHelper.ParseInt(element["SkipSplitVolume"], 100);
            PersonalBestVolume = SettingsHelper.ParseInt(element["PersonalBestVolume"], 100);
            NotAPersonalBestVolume = SettingsHelper.ParseInt(element["NotAPersonalBestVolume"], 100);
            ResetVolume = SettingsHelper.ParseInt(element["ResetVolume"], 100);
            PauseVolume = SettingsHelper.ParseInt(element["PauseVolume"], 100);
            ResumeVolume = SettingsHelper.ParseInt(element["ResumeVolume"], 100);
            StartTimerVolume = SettingsHelper.ParseInt(element["StartTimerVolume"], 100);
            GeneralVolume = SettingsHelper.ParseInt(element["GeneralVolume"], 100);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.6") ^
            SettingsHelper.CreateSetting(document, parent, "Split", string.Join(",", Split)) ^
            SettingsHelper.CreateSetting(document, parent, "SplitAheadGaining", string.Join(",", SplitAheadGaining)) ^
            SettingsHelper.CreateSetting(document, parent, "SplitAheadLosing", string.Join(",", SplitAheadLosing)) ^
            SettingsHelper.CreateSetting(document, parent, "SplitBehindGaining", string.Join(",", SplitBehindGaining)) ^
            SettingsHelper.CreateSetting(document, parent, "SplitBehindLosing", string.Join(",", SplitBehindLosing)) ^
            SettingsHelper.CreateSetting(document, parent, "BestSegment", string.Join(",", BestSegment)) ^
            SettingsHelper.CreateSetting(document, parent, "UndoSplit", string.Join(",", UndoSplit)) ^
            SettingsHelper.CreateSetting(document, parent, "SkipSplit", string.Join(",", SkipSplit)) ^
            SettingsHelper.CreateSetting(document, parent, "PersonalBest", string.Join(",", PersonalBest)) ^
            SettingsHelper.CreateSetting(document, parent, "NotAPersonalBest", string.Join(",", NotAPersonalBest)) ^
            SettingsHelper.CreateSetting(document, parent, "Reset", string.Join(",", Reset)) ^
            SettingsHelper.CreateSetting(document, parent, "Pause", string.Join(",", Pause)) ^
            SettingsHelper.CreateSetting(document, parent, "Resume", string.Join(",", Resume)) ^
            SettingsHelper.CreateSetting(document, parent, "StartTimer", string.Join(",", StartTimer)) ^
            SettingsHelper.CreateSetting(document, parent, "OutputDevice", OutputDevice) ^
            SettingsHelper.CreateSetting(document, parent, "SplitVolume", SplitVolume) ^
            SettingsHelper.CreateSetting(document, parent, "SplitAheadGainingVolume", SplitAheadGainingVolume) ^
            SettingsHelper.CreateSetting(document, parent, "SplitAheadLosingVolume", SplitAheadLosingVolume) ^
            SettingsHelper.CreateSetting(document, parent, "SplitBehindGainingVolume", SplitBehindGainingVolume) ^
            SettingsHelper.CreateSetting(document, parent, "SplitBehindLosingVolume", SplitBehindLosingVolume) ^
            SettingsHelper.CreateSetting(document, parent, "BestSegmentVolume", BestSegmentVolume) ^
            SettingsHelper.CreateSetting(document, parent, "UndoSplitVolume", UndoSplitVolume) ^
            SettingsHelper.CreateSetting(document, parent, "SkipSplitVolume", SkipSplitVolume) ^
            SettingsHelper.CreateSetting(document, parent, "PersonalBestVolume", PersonalBestVolume) ^
            SettingsHelper.CreateSetting(document, parent, "NotAPersonalBestVolume", NotAPersonalBestVolume) ^
            SettingsHelper.CreateSetting(document, parent, "ResetVolume", ResetVolume) ^
            SettingsHelper.CreateSetting(document, parent, "PauseVolume", PauseVolume) ^
            SettingsHelper.CreateSetting(document, parent, "ResumeVolume", ResumeVolume) ^
            SettingsHelper.CreateSetting(document, parent, "StartTimerVolume", StartTimerVolume) ^
            SettingsHelper.CreateSetting(document, parent, "GeneralVolume", GeneralVolume);
        }

        protected string[] BrowseForPath(TextBox textBox, Action<string[]> callback)
        {
            var path = textBox.Text;
            var pathArray = new string[] { "" };
            var fileDialog = new OpenFileDialog()
            {
                FileName = path,
                Filter = "Audio Files|*.mp3;*.wav;*.aiff;*.wma|All Files|*.*",
                Multiselect = true
            };

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                path = String.Join(",", fileDialog.FileNames);
                pathArray = fileDialog.FileNames;
            if (path == null || path.Length == 0)
                path = "";

            textBox.Text = path;
            callback(pathArray);

            return pathArray;
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtSplitPath, (x) => Split = x);
        }

        private void btnAheadGaining_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtSplitAheadGaining, (x) => SplitAheadGaining = x);
        }

        private void btnAheadLosing_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtSplitAheadLosing, (x) => SplitAheadLosing = x);
        }

        private void btnBehindGaining_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtSplitBehindGaining, (x) => SplitBehindGaining = x);
        }

        private void btnBehindLosing_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtSplitBehindLosing, (x) => SplitBehindLosing = x);
        }

        private void btnBestSegment_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtBestSegment, (x) => BestSegment = x);
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtUndo, (x) => UndoSplit = x);
        }

        private void btnSkipSplit_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtSkip, (x) => SkipSplit = x);
        }

        private void btnPersonalBest_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtPersonalBest, (x) => PersonalBest = x);
        }

        private void btnNotAPersonalBest_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtNotAPersonalBest, (x) => NotAPersonalBest = x);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtReset, (x) => Reset = x);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtPause, (x) => Pause = x);
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtResume, (x) => Resume = x);
        }

        private void btnStartTimer_Click(object sender, EventArgs e)
        {
            BrowseForPath(txtStartTimer, (x) => StartTimer = x);
        }

        private void VolumeTrackBarScrollHandler(object sender, EventArgs e)
        {
            TrackBar trackBar = (TrackBar)sender;

            ttVolume.SetToolTip(trackBar, trackBar.Value.ToString());
        }
    }
}
