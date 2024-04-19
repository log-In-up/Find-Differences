using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "Game Static Data", menuName = "Static Data/Levels")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField]
        private uint _levelIndex;

        [SerializeField, Range(0, 9)]
        private int[] _bottonImages, _topImages;

        [SerializeField]
        private LevelHandler _levelHandler;

        public int[] BottomImages => _bottonImages;

        public LevelHandler LevelHandler => _levelHandler;

        public uint LevelIndex => _levelIndex;

        public int[] TopImages => _topImages;
    }
}