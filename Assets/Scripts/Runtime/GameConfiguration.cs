namespace Runtime
{
    [System.Serializable]
    public sealed class GameConfiguration
    {
        public readonly int TargetCount;

        public GameConfiguration(int targetCount)
        {
            TargetCount = targetCount;
        }
    }
}
