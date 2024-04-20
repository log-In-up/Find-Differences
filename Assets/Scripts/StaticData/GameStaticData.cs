using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "Game Static Data", menuName = "Static Data/Game")]
    public class GameStaticData : ScriptableObject
    {
        [SerializeField]
        private string _encryptionCodeWord = "CodeWord";

        [SerializeField, Min(1.0f)]
        private float _extraSecondsOnBuy = 10.0f;

        [SerializeField]
        private string _gameScene = "Game";

        [SerializeField]
        private string _gameScreensaverScene = "GameScreensaver";

        [SerializeField]
        private string _initialScene = "Initial";

        [SerializeField]
        private AssetLabelReference _levelLabel;

        [SerializeField, Min(1.0f)]
        private float _levelTotalSeconds = 120.0f;

        [SerializeField]
        private string _saveFileName = "Save.json";

        public string EncryptionCodeWord => _encryptionCodeWord;
        public float ExtraSecondsOnBuy => _extraSecondsOnBuy;
        public string GameScene => _gameScene;
        public string GameScreensaverScene => _gameScreensaverScene;
        public string InitialScene => _initialScene;
        public AssetLabelReference LevelLabel => _levelLabel;
        public float LevelTotalSeconds => _levelTotalSeconds;
        public string SaveFileName => _saveFileName;
    }
}