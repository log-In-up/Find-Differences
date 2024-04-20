using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class GameData
    {
        public uint CurrentLevel;
        public float ExtraSeconds;

        public GameData()
        {
            CurrentLevel = 0;
            ExtraSeconds = 0.0f;
        }
    }
}