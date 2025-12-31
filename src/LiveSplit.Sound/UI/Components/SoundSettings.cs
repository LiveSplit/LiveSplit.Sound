using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

using NAudio.Wave;

namespace LiveSplit.UI.Components;

public partial class SoundSettings : UserControl
{
    public int OutputDevice { get; set; }

    public int GeneralVolume { get; set; }

    public Dictionary<EventType, SoundData> SoundDataDictionary { get; set; }
    public Dictionary<EventType, TextBox> TextBoxDictionary { get; set; }
    public Dictionary<TextBox, EventType> TextBoxToTypeDictionary { get; set; }
    public Dictionary<EventType, TrackBar> TrackBarDictionary { get; set; }
    public Dictionary<Button, EventType> ButtonToTypeDictionary { get; set; }

    public SoundSettings()
    {
        InitializeComponent();

        OutputDevice = 0;
        GeneralVolume = 100;

        SoundDataDictionary = [];
        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            SoundData data = new("", 0);
            SoundDataDictionary.Add(type, data);
        }

        TextBoxDictionary = [];
        TextBoxDictionary.Add(EventType.Split, txtSplitPath);
        TextBoxDictionary.Add(EventType.SplitAheadGaining, txtSplitAheadGaining);
        TextBoxDictionary.Add(EventType.SplitAheadLosing, txtSplitAheadLosing);
        TextBoxDictionary.Add(EventType.SplitBehindGaining, txtSplitBehindGaining);
        TextBoxDictionary.Add(EventType.SplitBehindLosing, txtSplitBehindLosing);
        TextBoxDictionary.Add(EventType.BestSegment, txtBestSegment);
        TextBoxDictionary.Add(EventType.UndoSplit, txtUndo);
        TextBoxDictionary.Add(EventType.SkipSplit, txtSkip);
        TextBoxDictionary.Add(EventType.PersonalBest, txtPersonalBest);
        TextBoxDictionary.Add(EventType.NotAPersonalBest, txtNotAPersonalBest);
        TextBoxDictionary.Add(EventType.Reset, txtReset);
        TextBoxDictionary.Add(EventType.Pause, txtPause);
        TextBoxDictionary.Add(EventType.Resume, txtResume);
        TextBoxDictionary.Add(EventType.StartTimer, txtStartTimer);

        TextBoxToTypeDictionary = [];
        TextBoxToTypeDictionary.Add(txtSplitPath, EventType.Split);
        TextBoxToTypeDictionary.Add(txtSplitAheadGaining, EventType.SplitAheadGaining);
        TextBoxToTypeDictionary.Add(txtSplitAheadLosing, EventType.SplitAheadLosing);
        TextBoxToTypeDictionary.Add(txtSplitBehindGaining, EventType.SplitBehindGaining);
        TextBoxToTypeDictionary.Add(txtSplitBehindLosing, EventType.SplitBehindLosing);
        TextBoxToTypeDictionary.Add(txtBestSegment, EventType.BestSegment);
        TextBoxToTypeDictionary.Add(txtUndo, EventType.UndoSplit);
        TextBoxToTypeDictionary.Add(txtSkip, EventType.SkipSplit);
        TextBoxToTypeDictionary.Add(txtPersonalBest, EventType.PersonalBest);
        TextBoxToTypeDictionary.Add(txtNotAPersonalBest, EventType.NotAPersonalBest);
        TextBoxToTypeDictionary.Add(txtReset, EventType.Reset);
        TextBoxToTypeDictionary.Add(txtPause, EventType.Pause);
        TextBoxToTypeDictionary.Add(txtResume, EventType.Resume);
        TextBoxToTypeDictionary.Add(txtStartTimer, EventType.StartTimer);

        TrackBarDictionary = [];
        TrackBarDictionary.Add(EventType.Split, tbSplitVolume);
        TrackBarDictionary.Add(EventType.SplitAheadGaining, tbSplitAheadGainingVolume);
        TrackBarDictionary.Add(EventType.SplitAheadLosing, tbSplitAheadLosingVolume);
        TrackBarDictionary.Add(EventType.SplitBehindGaining, tbSplitBehindGainingVolume);
        TrackBarDictionary.Add(EventType.SplitBehindLosing, tbSplitBehindLosingVolume);
        TrackBarDictionary.Add(EventType.BestSegment, tbBestSegmentVolume);
        TrackBarDictionary.Add(EventType.UndoSplit, tbUndoVolume);
        TrackBarDictionary.Add(EventType.SkipSplit, tbSkipVolume);
        TrackBarDictionary.Add(EventType.PersonalBest, tbPersonalBestVolume);
        TrackBarDictionary.Add(EventType.NotAPersonalBest, tbNotAPersonalBestVolume);
        TrackBarDictionary.Add(EventType.Reset, tbResetVolume);
        TrackBarDictionary.Add(EventType.Pause, tbPauseVolume);
        TrackBarDictionary.Add(EventType.Resume, tbResumeVolume);
        TrackBarDictionary.Add(EventType.StartTimer, tbStartTimerVolume);

        ButtonToTypeDictionary = [];
        ButtonToTypeDictionary.Add(btnSplit, EventType.Split);
        ButtonToTypeDictionary.Add(btnAheadGaining, EventType.SplitAheadGaining);
        ButtonToTypeDictionary.Add(btnAheadLosing, EventType.SplitAheadLosing);
        ButtonToTypeDictionary.Add(btnBehindGaining, EventType.SplitBehindGaining);
        ButtonToTypeDictionary.Add(btnBehindLosing, EventType.SplitBehindLosing);
        ButtonToTypeDictionary.Add(btnBestSegment, EventType.BestSegment);
        ButtonToTypeDictionary.Add(btnUndo, EventType.UndoSplit);
        ButtonToTypeDictionary.Add(btnSkipSplit, EventType.SkipSplit);
        ButtonToTypeDictionary.Add(btnPersonalBest, EventType.PersonalBest);
        ButtonToTypeDictionary.Add(btnNotAPersonalBest, EventType.NotAPersonalBest);
        ButtonToTypeDictionary.Add(btnReset, EventType.Reset);
        ButtonToTypeDictionary.Add(btnPause, EventType.Pause);
        ButtonToTypeDictionary.Add(btnResume, EventType.Resume);
        ButtonToTypeDictionary.Add(btnStartTimer, EventType.StartTimer);

        for (int i = 0; i < WaveOut.DeviceCount; ++i)
        {
            cbOutputDevice.Items.Add(WaveOut.GetCapabilities(i));
        }

        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            TextBoxDictionary[type].DataBindings.Add("Text", SoundDataDictionary[type], nameof(SoundData.FilePath));
            TrackBarDictionary[type].DataBindings.Add("Value", SoundDataDictionary[type], nameof(SoundData.Volume));

        }

        cbOutputDevice.DataBindings.Add("SelectedIndex", this, nameof(OutputDevice));
        tbGeneralVolume.DataBindings.Add("Value", this, nameof(GeneralVolume));
    }

    public void SetSettings(XmlNode node)
    {
        var element = (XmlElement)node;

        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            SoundDataDictionary[type].FilePath = SettingsHelper.ParseString(element[type.ToString()]);
            SoundDataDictionary[type].Volume = SettingsHelper.ParseInt(element[$"{type}Volume"]);
        }

        OutputDevice = SettingsHelper.ParseInt(element["OutputDevice"]);
        GeneralVolume = SettingsHelper.ParseInt(element["GeneralVolume"], 100);
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
        int hash = SettingsHelper.CreateSetting(document, parent, "Version", "1.6") ^
            SettingsHelper.CreateSetting(document, parent, nameof(OutputDevice), OutputDevice) ^
            SettingsHelper.CreateSetting(document, parent, nameof(GeneralVolume), GeneralVolume);

        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            hash ^= SettingsHelper.CreateSetting(document, parent, type.ToString(), SoundDataDictionary[type].FilePath) ^
                SettingsHelper.CreateSetting(document, parent, $"{type}Volume", SoundDataDictionary[type].Volume);
        }

        return hash;
    }

    protected string BrowseForPath(TextBox textBox, Action<string> callback)
    {
        string path = textBox.Text;
        var fileDialog = new OpenFileDialog()
        {
            FileName = path,
            Filter = "Audio Files|*.mp3;*.wav;*.aiff;*.wma|All Files|*.*"
        };

        DialogResult result = fileDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            path = fileDialog.FileName;
        }

        textBox.Text = path;
        callback(path);

        return path;
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        EventType type = ButtonToTypeDictionary[(Button)sender];
        BrowseForPath(TextBoxDictionary[type], (x) => SoundDataDictionary[type].FilePath = x);
    }

    private void VolumeTrackBarScrollHandler(object sender, EventArgs e)
    {
        var trackBar = (TrackBar)sender;

        ttVolume.SetToolTip(trackBar, trackBar.Value.ToString());
    }

    private void txtFilePath_DragDrop(object sender, DragEventArgs e)
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        var textBox = (TextBox)sender;
        EventType type = TextBoxToTypeDictionary[textBox];

        SoundDataDictionary[type].FilePath = files[0];
        textBox.Text = files[0];
    }

    private void txtFilePath_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effect = DragDropEffects.Copy;
        }
        else
        {
            e.Effect = DragDropEffects.None;
        }
    }
}
