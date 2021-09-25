namespace Runtime.Target
{
    /// <summary>
    /// <see cref="GameState.ShareTarget"/>
    /// </summary>
    public interface ITargetSharer : IStateWorker
    {
        public TargetData[] SharedTarget();
    }
}
