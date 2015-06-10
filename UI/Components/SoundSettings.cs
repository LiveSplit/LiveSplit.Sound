using NAudio.Wave;
using System;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.UI.Components
{
    public partial class SoundSettings : UserControl
    {
        public String Split { get; set; }
        public String SplitAheadGaining { get; set; }
        public String SplitAheadLosing { get; set; }
        public String SplitBehindGaining { get; set; }
        public String SplitBehindLosing { get; set; }
        public String BestSegment { get; set; }
        public String UndoSplit { get; set; }
        public String SkipSplit { get; set; }
        public String PersonalBest { get; set; }
        public String NotAPersonalBest { get; set; }
        public String Reset { get; set; }
        public String Pause { get; set; }
        public String Resume { get; set; }
        public String StartTimer { get; set; }

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
            StartTimer = "";

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

            txtSplitPath.DataBindings.Add("Text", this, "Split");
            txtSplitAheadGaining.DataBindings.Add("Text", this, "SplitAheadGaining");
            txtSplitAheadLosing.DataBindings.Add("Text", this, "SplitAheadLosing");
            txtSplitBehindGaining.DataBindings.Add("Text", this, "SplitBehindGaining");
            txtSplitBehindLosing.DataBindings.Add("Text", this, "SplitBehindLosing");
            txtBestSegment.DataBindings.Add("Text", this, "BestSegment");
            txtUndo.DataBindings.Add("Text", this, "UndoSplit");
            txtSkip.DataBindings.Add("Text", this, "SkipSplit");
            txtPersonalBest.DataBindings.Add("Text", this, "PersonalBest");
            txtNotAPersonalBest.DataBindings.Add("Text", this, "NotAPersonalBest");
            txtReset.DataBindings.Add("Text", this, "Reset");
            txtPause.DataBindings.Add("Text", this, "Pause");
            txtResume.DataBindings.Add("Text", this, "Resume");
            txtStartTimer.DataBindings.Add("Text", this, "StartTimer");

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

        private T ParseEnum<T>(XmlElement element)
        {
            return (T)Enum.Parse(typeof(T), element.InnerText);
        }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;

            Version version;
            if (element["Version"] != null)
                version = Version.Parse(element["Version"].InnerText);
            else
                version = new Version(1, 0, 0, 0);

            Split = element["Split"].InnerText;
            SplitAheadGaining = element["SplitAheadGaining"].InnerText;
            SplitAheadLosing = element["SplitAheadLosing"].InnerText;
            SplitBehindGaining = element["SplitBehindGaining"].InnerText;
            SplitBehindLosing = element["SplitBehindLosing"].InnerText;
            BestSegment = element["BestSegment"].InnerText;
            UndoSplit = element["UndoSplit"].InnerText;
            SkipSplit = element["SkipSplit"].InnerText;
            PersonalBest = element["PersonalBest"].InnerText;
            NotAPersonalBest = element["NotAPersonalBest"].InnerText;
            Reset = element["Reset"].InnerText;
            Pause = element["Pause"].InnerText;
            Resume = element["Resume"].InnerText;
            StartTimer = element["StartTimer"].InnerText;

            OutputDevice = int.Parse(element["OutputDevice"].InnerText);

            SplitVolume = int.Parse(element["SplitVolume"].InnerText);
            SplitAheadGainingVolume = int.Parse(element["SplitAheadGainingVolume"].InnerText);
            SplitAheadLosingVolume = int.Parse(element["SplitAheadLosingVolume"].InnerText);
            SplitBehindGainingVolume = int.Parse(element["SplitBehindGainingVolume"].InnerText);
            SplitBehindLosingVolume = int.Parse(element["SplitBehindLosingVolume"].InnerText);
            BestSegmentVolume = int.Parse(element["BestSegmentVolume"].InnerText);
            UndoSplitVolume = int.Parse(element["UndoSplitVolume"].InnerText);
            SkipSplitVolume = int.Parse(element["SkipSplitVolume"].InnerText);
            PersonalBestVolume = int.Parse(element["PersonalBestVolume"].InnerText);
            NotAPersonalBestVolume = int.Parse(element["NotAPersonalBestVolume"].InnerText);
            ResetVolume = int.Parse(element["ResetVolume"].InnerText);
            PauseVolume = int.Parse(element["PauseVolume"].InnerText);
            ResumeVolume = int.Parse(element["ResumeVolume"].InnerText);
            StartTimerVolume = int.Parse(element["StartTimerVolume"].InnerText);
            GeneralVolume = int.Parse(element["GeneralVolume"].InnerText);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");

            parent.AppendChild(ToElement(document, "Version", "1.5"));

            parent.AppendChild(ToElement(document, "Split", Split));
            parent.AppendChild(ToElement(document, "SplitAheadGaining", SplitAheadGaining));
            parent.AppendChild(ToElement(document, "SplitAheadLosing", SplitAheadLosing));
            parent.AppendChild(ToElement(document, "SplitBehindGaining", SplitBehindGaining));
            parent.AppendChild(ToElement(document, "SplitBehindLosing", SplitBehindLosing));
            parent.AppendChild(ToElement(document, "BestSegment", BestSegment));
            parent.AppendChild(ToElement(document, "UndoSplit", UndoSplit));
            parent.AppendChild(ToElement(document, "SkipSplit", SkipSplit));
            parent.AppendChild(ToElement(document, "PersonalBest", PersonalBest));
            parent.AppendChild(ToElement(document, "NotAPersonalBest", NotAPersonalBest));
            parent.AppendChild(ToElement(document, "Reset", Reset));
            parent.AppendChild(ToElement(document, "Pause", Pause));
            parent.AppendChild(ToElement(document, "Resume", Resume));
            parent.AppendChild(ToElement(document, "StartTimer", StartTimer));

            parent.AppendChild(ToElement(document, "OutputDevice", OutputDevice));

            parent.AppendChild(ToElement(document, "SplitVolume", SplitVolume));
            parent.AppendChild(ToElement(document, "SplitAheadGainingVolume", SplitAheadGainingVolume));
            parent.AppendChild(ToElement(document, "SplitAheadLosingVolume", SplitAheadLosingVolume));
            parent.AppendChild(ToElement(document, "SplitBehindGainingVolume", SplitBehindGainingVolume));
            parent.AppendChild(ToElement(document, "SplitBehindLosingVolume", SplitBehindLosingVolume));
            parent.AppendChild(ToElement(document, "BestSegmentVolume", BestSegmentVolume));
            parent.AppendChild(ToElement(document, "UndoSplitVolume", UndoSplitVolume));
            parent.AppendChild(ToElement(document, "SkipSplitVolume", SkipSplitVolume));
            parent.AppendChild(ToElement(document, "PersonalBestVolume", PersonalBestVolume));
            parent.AppendChild(ToElement(document, "NotAPersonalBestVolume", NotAPersonalBestVolume));
            parent.AppendChild(ToElement(document, "ResetVolume", ResetVolume));
            parent.AppendChild(ToElement(document, "PauseVolume", PauseVolume));
            parent.AppendChild(ToElement(document, "ResumeVolume", ResumeVolume));
            parent.AppendChild(ToElement(document, "StartTimerVolume", StartTimerVolume));
            parent.AppendChild(ToElement(document, "GeneralVolume", GeneralVolume));

            return parent;
        }

        private XmlElement ToElement<T>(XmlDocument document, String name, T value)
        {
            var element = document.CreateElement(name);

            element.InnerText = value.ToString();

            return element;
        }

        protected String BrowseForPath(TextBox textBox, Action<string> callback)
        {
            var path = textBox.Text;
            var fileDialog = new OpenFileDialog()
            {
                FileName = path,
                Filter = "Audio Files|*.mp3;*.wav;*.aiff;*.wma|All Files|*.*"
            };

            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
                path = fileDialog.FileName;

            textBox.Text = path;
            callback(path);

            return path;
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
