using System.Collections.Generic;

namespace LiveSplit.UI.Components;
public class SoundData
{
    public IList<string> FilePaths { get; set; }
    public int Volume { get; set; }

    public SoundData(IList<string> filePaths, int volume)
    {
        FilePaths = filePaths;
        Volume = volume;
    }
}
