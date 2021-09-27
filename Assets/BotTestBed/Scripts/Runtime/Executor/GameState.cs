using System;

namespace Runtime.Executor
{
    [Flags]
    public enum GameState
    {
        Default,
        Wait,
        Connect,
        ShareTarget,
        GenerateTarget,
        Start,
        Playing,
        Finish,
        Result,
    }
}
