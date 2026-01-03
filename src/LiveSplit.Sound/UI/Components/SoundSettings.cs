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
    private Dictionary<EventType, SoundDataSettingsSet> DataSettingsDictionary { get; set; }

    public SoundSettings()
    {
        InitializeComponent();

        OutputDevice = 0;
        GeneralVolume = 100;

        SoundDataDictionary = []; 
        DataSettingsDictionary = [];
        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            SoundData data = new("", 100);
            SoundDataDictionary.Add(type, data);

            SoundFileSettings sfs = new(type, data);
            SoundVolumeSettings svs = new(type, data);

            SoundDataSettingsSet settingsSet = new(sfs, svs);
            DataSettingsDictionary.Add(type, settingsSet);
        }

        for (int i = 0; i < WaveOut.DeviceCount; ++i)
        {
            cbOutputDevice.Items.Add(WaveOut.GetCapabilities(i));
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

    private void VolumeTrackBarScrollHandler(object sender, EventArgs e)
    {
        var trackBar = (TrackBar)sender;

        ttVolume.SetToolTip(trackBar, trackBar.Value.ToString());
    }

    private void SoundSettings_Load(object sender, EventArgs e)
    {
        int index = 0;
        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            tableLayoutPanel1.Controls.Add(DataSettingsDictionary[type].FileSettings, 0, index);
            tableLayoutPanel2.Controls.Add(DataSettingsDictionary[type].VolumeSettings, 0, index + 2);
            tableLayoutPanel2.SetColumnSpan(DataSettingsDictionary[type].VolumeSettings, 2);

            index++;
        }
    }
}

internal class SoundDataSettingsSet
{
    internal SoundFileSettings FileSettings { get; set; }
    internal SoundVolumeSettings VolumeSettings { get; set; }

    internal SoundDataSettingsSet(SoundFileSettings sfs, SoundVolumeSettings svs)
    {
        FileSettings = sfs;
        VolumeSettings = svs;
    }
}
