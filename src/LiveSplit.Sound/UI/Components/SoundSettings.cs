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

    public Dictionary<EventType, SoundDataSettingsSet> DataSettingsDictionary { get; set; }

    public SoundSettings()
    {
        InitializeComponent();

        OutputDevice = 0;
        GeneralVolume = 100;

        DataSettingsDictionary = [];
        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            SoundData data = new("", 100);
            SoundFileSettings sfs = new(type, data);
            SoundVolumeSettings svs = new(type, data);

            SoundDataSettingsSet settingsSet = new(data, sfs, svs);
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
            DataSettingsDictionary[type].Data.FilePath = SettingsHelper.ParseString(element[type.ToString()]);
            DataSettingsDictionary[type].Data.Volume = SettingsHelper.ParseInt(element[$"{type}Volume"]);
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
            hash ^= SettingsHelper.CreateSetting(document, parent, type.ToString(), DataSettingsDictionary[type].Data.FilePath) ^
                SettingsHelper.CreateSetting(document, parent, $"{type}Volume", DataSettingsDictionary[type].Data.Volume);
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

public class SoundDataSettingsSet
{
    public SoundData Data { get; set; }
    public SoundFileSettings FileSettings { get; set; }
    public SoundVolumeSettings VolumeSettings { get; set; }

    public SoundDataSettingsSet(SoundData data, SoundFileSettings sfs, SoundVolumeSettings svs)
    {
        Data = data;
        FileSettings = sfs;
        VolumeSettings = svs;
    }
}
