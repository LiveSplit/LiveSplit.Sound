using System;
using System.Windows.Forms;

namespace LiveSplit.UI.Components;

public partial class SoundVolumeSettings : UserControl
{
    public EventType EventType { get; set; }
    public SoundData Data { get; set; }
    public int Volume { get => Data.Volume; set => Data.Volume = value; }

    public SoundVolumeSettings(EventType eventType, SoundData data)
    {
        InitializeComponent();

        EventType = eventType;
        Data = data;

        lblName.Text = eventType.GetName();
        tbVolume.DataBindings.Add("Value", this, nameof(Volume));
    }

    private void VolumeTrackBarScrollHandler(object sender, EventArgs e)
    {
        var trackBar = (TrackBar)sender;

        ttVolume.SetToolTip(trackBar, trackBar.Value.ToString());
    }
}
