using System;
using System.Collections.Generic;
using System.Linq;
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

    private bool IsClearAddDragDrop { get; set; }

    public SoundSettings()
    {
        InitializeComponent();

        OutputDevice = 0;
        GeneralVolume = 100;

        SoundDataDictionary = [];
        DataSettingsDictionary = [];

        IsClearAddDragDrop = true;

        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            SoundData data = new([], 100);
            SoundDataDictionary.Add(type, data);

            SoundFileSettings sfs = new(type, data)
            {
                IsClearAddDragDrop = IsClearAddDragDrop,
            };
            SoundVolumeSettings svs = new(type, data);

            SoundDataSettingsSet settingsSet = new(sfs, svs);
            DataSettingsDictionary.Add(type, settingsSet);
        }

        int index = 0;
        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            AddControl(tableLayoutPanel1, DataSettingsDictionary[type].FileSettings, index + 1, 3);
            AddControl(tableLayoutPanel2, DataSettingsDictionary[type].VolumeSettings, index + 2, 2);

            index++;
        }

        for (int i = 0; i < WaveOut.DeviceCount; ++i)
        {
            cbOutputDevice.Items.Add(WaveOut.GetCapabilities(i));
        }

        cbOutputDevice.DataBindings.Add("SelectedIndex", this, nameof(OutputDevice));
        tbGeneralVolume.DataBindings.Add("Value", this, nameof(GeneralVolume));
    }

    private void AddControl(TableLayoutPanel tableLayoutPanel, UserControl soundDataControl, int rowIndex, int columnSpan)
    {
        tableLayoutPanel.Controls.Add(soundDataControl, 0, rowIndex);
        tableLayoutPanel.SetColumnSpan(soundDataControl, columnSpan);
    }

    private void SoundSettings_Load(object sender, EventArgs e)
    {
        rdoClearAddDragDrop.Checked = IsClearAddDragDrop;
        rdoAppendDragDrop.Checked = !IsClearAddDragDrop;
    }

    private void rdoAddDragDrop_CheckedChanged(object sender, EventArgs e)
    {
        if (rdoClearAddDragDrop.Checked)
        {
            IsClearAddDragDrop = true;
        }
        else
        {
            IsClearAddDragDrop = false;
        }

        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            DataSettingsDictionary[type].FileSettings.IsClearAddDragDrop = IsClearAddDragDrop;
        }
    }

    public void SetSettings(XmlNode node)
    {
        var element = (XmlElement)node;

        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            SoundDataDictionary[type].FilePaths = ParsePathListSetting(element, type.ToString());
            SoundDataDictionary[type].Volume = SettingsHelper.ParseInt(element[$"{type}Volume"]);
        }

        OutputDevice = SettingsHelper.ParseInt(element[nameof(OutputDevice)]);
        GeneralVolume = SettingsHelper.ParseInt(element[nameof(GeneralVolume)], 100);
        IsClearAddDragDrop = SettingsHelper.ParseBool(element[nameof(IsClearAddDragDrop)], true);
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
        int hash = SettingsHelper.CreateSetting(document, parent, "Version", "1.6") ^
                   SettingsHelper.CreateSetting(document, parent, nameof(OutputDevice), OutputDevice) ^
                   SettingsHelper.CreateSetting(document, parent, nameof(GeneralVolume), GeneralVolume) ^
                   SettingsHelper.CreateSetting(document, parent, nameof(IsClearAddDragDrop), IsClearAddDragDrop);

        foreach (EventType type in Enum.GetValues(typeof(EventType)))
        {
            hash ^= CreatePathListSetting(document, parent, type.ToString(), SoundDataDictionary[type].FilePaths) ^
                    SettingsHelper.CreateSetting(document, parent, $"{type}Volume", SoundDataDictionary[type].Volume);
        }

        return hash;
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

    private void VolumeTrackBarScrollHandler(object sender, EventArgs e)
    {
        var trackBar = (TrackBar)sender;

        ttVolume.SetToolTip(trackBar, trackBar.Value.ToString());
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
