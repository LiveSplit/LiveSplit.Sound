using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LiveSplit.UI.Components;

public partial class SoundFileSettings : UserControl
{
    public SoundData Data { get; set; }
    public IList<string> FilePaths { get => Data.FilePaths; set => Data.FilePaths = value; }
    private const string PathSeparator = ", ";

    public bool IsClearAddDragDrop { get; set; }

    public SoundFileSettings(EventType eventType, SoundData data)
    {
        InitializeComponent();

        Data = data;

        lblName.Text = eventType.GetName();
        AddPathListBinding(txtFilePath.DataBindings, "Text", this, nameof(FilePaths));
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

            convertEvent.Value = string.Join(PathSeparator, (IList<string>)convertEvent.Value);
        });

        bindings.Add(b);
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        string path = FilePaths.FirstOrDefault() ?? string.Empty;
        var fileDialog = new OpenFileDialog()
        {
            Multiselect = true,
            FileName = path,
            Filter = "Audio Files|*.mp3;*.wav;*.aiff;*.wma|All Files|*.*"
        };

        DialogResult result = fileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            FilePaths = fileDialog.FileNames;
        }

        txtFilePath.Text = string.Join(PathSeparator, FilePaths);
    }

    private void txtFilePath_DragDrop(object sender, DragEventArgs e)
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
        if (IsClearAddDragDrop)
        {
            FilePaths = files;
        }
        else
        {
            FilePaths = [.. FilePaths.Concat(files).Distinct()];
        }

        txtFilePath.Text = string.Join(PathSeparator, FilePaths);
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

    private void btnClear_Click(object sender, EventArgs e)
    {
        FilePaths = [];
        txtFilePath.Clear();
    }

    private void txtFilePath_Enter(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;

        // Display below the text box
        ttPaths.Show(textBox.Text.Replace(PathSeparator, "\n"), textBox, 0, textBox.Height);
    }

    private void txtFilePath_Leave(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;

        ttPaths.Hide(textBox);
    }
}
