using System;
using System.Windows.Forms;

namespace LiveSplit.UI.Components;

public partial class SoundFileSettings : UserControl
{
    public EventType EventType { get; set; }
    public SoundData Data { get; set; }
    public string FilePath { get => Data.FilePath; set => Data.FilePath = value; }

    public SoundFileSettings(EventType eventType, SoundData data)
    {
        InitializeComponent();

        EventType = eventType;
        Data = data;

        lblName.Text = eventType.GetName();
        txtFilePath.DataBindings.Add("Text", this, nameof(FilePath));
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
        BrowseForPath(txtFilePath, (x) => FilePath = x);
    }

    private void txtFilePath_DragDrop(object sender, DragEventArgs e)
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        txtFilePath.Text = files[0];
        FilePath = files[0];
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
