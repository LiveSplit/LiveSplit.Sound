using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

using NAudio.Wave;

namespace LiveSplit.UI.Components;

public partial class SoundSettings : UserControl
{
    public IList<string> Split { get; set; }
    public IList<string> SplitAheadGaining { get; set; }
    public IList<string> SplitAheadLosing { get; set; }
    public IList<string> SplitBehindGaining { get; set; }
    public IList<string> SplitBehindLosing { get; set; }
    public IList<string> BestSegment { get; set; }
    public IList<string> UndoSplit { get; set; }
    public IList<string> SkipSplit { get; set; }
    public IList<string> PersonalBest { get; set; }
    public IList<string> NotAPersonalBest { get; set; }
    public IList<string> Reset { get; set; }
    public IList<string> Pause { get; set; }
    public IList<string> Resume { get; set; }
    public IList<string> StartTimer { get; set; }

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

        Split = [];
        SplitAheadGaining = [];
        SplitAheadLosing = [];
        SplitBehindGaining = [];
        SplitBehindLosing = [];
        BestSegment = [];
        UndoSplit = [];
        SkipSplit = [];
        PersonalBest = [];
        NotAPersonalBest = [];
        Reset = [];
        Pause = [];
        Resume = [];
        StartTimer = [];

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
        {
            cbOutputDevice.Items.Add(WaveOut.GetCapabilities(i));
        }

        AddPathListBinding(txtSplitPath.DataBindings, "Text", this, "Split");
        AddPathListBinding(txtSplitAheadGaining.DataBindings, "Text", this, "SplitAheadGaining");
        AddPathListBinding(txtSplitAheadLosing.DataBindings, "Text", this, "SplitAheadLosing");
        AddPathListBinding(txtSplitBehindGaining.DataBindings, "Text", this, "SplitBehindGaining");
        AddPathListBinding(txtSplitBehindLosing.DataBindings, "Text", this, "SplitBehindLosing");
        AddPathListBinding(txtBestSegment.DataBindings, "Text", this, "BestSegment");
        AddPathListBinding(txtUndo.DataBindings, "Text", this, "UndoSplit");
        AddPathListBinding(txtSkip.DataBindings, "Text", this, "SkipSplit");
        AddPathListBinding(txtPersonalBest.DataBindings, "Text", this, "PersonalBest");
        AddPathListBinding(txtNotAPersonalBest.DataBindings, "Text", this, "NotAPersonalBest");
        AddPathListBinding(txtReset.DataBindings, "Text", this, "Reset");
        AddPathListBinding(txtPause.DataBindings, "Text", this, "Pause");
        AddPathListBinding(txtResume.DataBindings, "Text", this, "Resume");
        AddPathListBinding(txtStartTimer.DataBindings, "Text", this, "StartTimer");

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

    private void AddPathListBinding(ControlBindingsCollection bindings, string propertyName, object dataSource, string dataMember)
    {
        Binding b = new(propertyName, dataSource, dataMember, true, DataSourceUpdateMode.Never);
        b.Format += new ConvertEventHandler((sender, convertEvent) =>
        {
            if (convertEvent.DesiredType != typeof(string))
            {
                return;
            }

            convertEvent.Value = string.Join(", ", (IList<string>)convertEvent.Value);
        });

        bindings.Add(b);
    }

    public void SetSettings(XmlNode node)
    {
        var element = (XmlElement)node;

        Split = ParsePathListSetting(element, "Split");
        SplitAheadGaining = ParsePathListSetting(element, "SplitAheadGaining");
        SplitAheadLosing = ParsePathListSetting(element, "SplitAheadLosing");
        SplitBehindGaining = ParsePathListSetting(element, "SplitBehindGaining");
        SplitBehindLosing = ParsePathListSetting(element, "SplitBehindLosing");
        BestSegment = ParsePathListSetting(element, "BestSegment");
        UndoSplit = ParsePathListSetting(element, "UndoSplit");
        SkipSplit = ParsePathListSetting(element, "SkipSplit");
        PersonalBest = ParsePathListSetting(element, "PersonalBest");
        NotAPersonalBest = ParsePathListSetting(element, "NotAPersonalBest");
        Reset = ParsePathListSetting(element, "Reset");
        Pause = ParsePathListSetting(element, "Pause");
        Resume = ParsePathListSetting(element, "Resume");
        StartTimer = ParsePathListSetting(element, "StartTimer");

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

    private IList<string> ParsePathListSetting(XmlElement element, string settingName)
    {
        XmlElement settingElement = element[settingName];
        if (settingElement == null)
        {
            return [];
        }

        IList<string> paths = [];

        XmlNodeList pathTexts = settingElement.SelectNodes("./path/text()");
        foreach (XmlCharacterData pathData in pathTexts)
        {
            if (pathData is XmlText pathText)
            {
                paths.Add(pathData.Data);
            }
        }

        // Support old, single-path setting format
        if (paths.Count == 0)
        {
            if (settingElement.SelectSingleNode("./text()") is XmlText oldSettingValue)
            {
                paths.Add(oldSettingValue.Data);
            }
        }

        return paths;
    }

    public XmlNode GetSettings(XmlDocument document)
    {
        XmlElement parent = document.CreateElement("Settings");
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
        CreatePathListSetting(document, parent, "Split", Split) ^
        CreatePathListSetting(document, parent, "SplitAheadGaining", SplitAheadGaining) ^
        CreatePathListSetting(document, parent, "SplitAheadLosing", SplitAheadLosing) ^
        CreatePathListSetting(document, parent, "SplitBehindGaining", SplitBehindGaining) ^
        CreatePathListSetting(document, parent, "SplitBehindLosing", SplitBehindLosing) ^
        CreatePathListSetting(document, parent, "BestSegment", BestSegment) ^
        CreatePathListSetting(document, parent, "UndoSplit", UndoSplit) ^
        CreatePathListSetting(document, parent, "SkipSplit", SkipSplit) ^
        CreatePathListSetting(document, parent, "PersonalBest", PersonalBest) ^
        CreatePathListSetting(document, parent, "NotAPersonalBest", NotAPersonalBest) ^
        CreatePathListSetting(document, parent, "Reset", Reset) ^
        CreatePathListSetting(document, parent, "Pause", Pause) ^
        CreatePathListSetting(document, parent, "Resume", Resume) ^
        CreatePathListSetting(document, parent, "StartTimer", StartTimer) ^
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

    private static int CreatePathListSetting(XmlDocument document, XmlElement parent, string name, IList<string> paths)
    {
        if (document != null)
        {
            XmlElement pathListElement = document.CreateElement(name);
            foreach (string path in paths)
            {
                SettingsHelper.CreateSetting(document, pathListElement, "path", path);
            }

            parent.AppendChild(pathListElement);
        }

        return paths.Aggregate(0, (hash, next) => hash ^= next.GetHashCode());
    }

    private void BrowseForPaths(TextBox textBox, IList<string> paths, Action<IList<string>> callback)
    {
        string path = paths.FirstOrDefault() ?? string.Empty;
        var fileDialog = new OpenFileDialog()
        {
            Multiselect = true,
            FileName = path,
            Filter = "Audio Files|*.mp3;*.wav;*.aiff;*.wma|All Files|*.*"
        };

        DialogResult result = fileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            paths = fileDialog.FileNames;
        }

        textBox.Text = string.Join(", ", paths);
        callback(paths);
    }

    private void btnSplit_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtSplitPath, Split, paths => Split = paths);
    }

    private void btnAheadGaining_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtSplitAheadGaining, SplitAheadGaining, paths => SplitAheadGaining = paths);
    }

    private void btnAheadLosing_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtSplitAheadLosing, SplitAheadLosing, paths => SplitAheadLosing = paths);
    }

    private void btnBehindGaining_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtSplitBehindGaining, SplitBehindGaining, paths => SplitBehindGaining = paths);
    }

    private void btnBehindLosing_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtSplitBehindLosing, SplitBehindLosing, paths => SplitBehindLosing = paths);
    }

    private void btnBestSegment_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtBestSegment, BestSegment, paths => BestSegment = paths);
    }

    private void btnUndo_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtUndo, UndoSplit, paths => UndoSplit = paths);
    }

    private void btnSkipSplit_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtSkip, SkipSplit, paths => SkipSplit = paths);
    }

    private void btnPersonalBest_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtPersonalBest, PersonalBest, paths => PersonalBest = paths);
    }

    private void btnNotAPersonalBest_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtNotAPersonalBest, NotAPersonalBest, paths => NotAPersonalBest = paths);
    }

    private void btnReset_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtReset, Reset, paths => Reset = paths);
    }

    private void btnPause_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtPause, Pause, paths => Pause = paths);
    }

    private void btnResume_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtResume, Resume, paths => Resume = paths);
    }

    private void btnStartTimer_Click(object sender, EventArgs e)
    {
        BrowseForPaths(txtStartTimer, StartTimer, paths => StartTimer = paths);
    }

    private void VolumeTrackBarScrollHandler(object sender, EventArgs e)
    {
        var trackBar = (TrackBar)sender;

        ttVolume.SetToolTip(trackBar, trackBar.Value.ToString());
    }
}
