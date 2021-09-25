using System;

namespace Runtime
{
    [Flags]
    public enum GameState
    {
        Default,
        Wait,
        ShareTarget,
        GenerateTarget,
        Start,
        Playing,
        Finish,
        Result,
    }
}
