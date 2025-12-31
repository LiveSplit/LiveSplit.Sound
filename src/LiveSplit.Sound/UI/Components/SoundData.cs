namespace LiveSplit.UI.Components;
public class SoundData
{
    public string FilePath { get; set; }
    public int Volume { get; set; }

    public SoundData(string filePath, int volume)
    {
        FilePath = filePath;
        Volume = volume;
    }
}
