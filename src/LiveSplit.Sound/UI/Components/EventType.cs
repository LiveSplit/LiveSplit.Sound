namespace LiveSplit.UI.Components;
public enum EventType
{
    Split,
    SplitAheadGaining,
    SplitAheadLosing,
    SplitBehindGaining,
    SplitBehindLosing,
    BestSegment,
    UndoSplit,
    SkipSplit,
    PersonalBest,
    NotAPersonalBest,
    Reset,
    Pause,
    Resume,
    StartTimer,
}

public static class EventTypeEx
{
    public static string GetName(this EventType type)
    {
        return type switch
        {
            EventType.Split => "Split:",
            EventType.SplitAheadGaining => "Split (Ahead, Gaining Time):",
            EventType.SplitAheadLosing => "Split (Ahead, Losing Time):",
            EventType.SplitBehindGaining => "Split (Behind, Gaining Time):",
            EventType.SplitBehindLosing => "Split (Behind, Losing Time):",
            EventType.BestSegment => "Split (Best Segment):",
            EventType.UndoSplit => "Undo Split:",
            EventType.SkipSplit => "Skip Split:",
            EventType.PersonalBest => "Personal Best:",
            EventType.NotAPersonalBest => "Not a Personal Best:",
            EventType.Reset => "Reset:",
            EventType.Pause => "Pause:",
            EventType.Resume => "Resume:",
            EventType.StartTimer => "Start Timer:",
            _ => "no name",
        };
    }
}
