using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class GameData
    {
        public uint CurrentLevel;

        public GameData()
        {
            CurrentLevel = 0;
        }
    }
}