using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "Game Static Data", menuName = "Static Data/Game")]
    public class GameStaticData : ScriptableObject
    {
        [SerializeField]
        private string _encryptionCodeWord = "CodeWord";

        [SerializeField]
        private string _gameScene = "Game";

        [SerializeField]
        private string _gameScreensaverScene = "GameScreensaver";

        [SerializeField]
        private string _initialScene = "Initial";

        [SerializeField]
        private string _saveFileName = "Save.json";

        [SerializeField]
        private AssetLabelReference _levelLabel;

        public string EncryptionCodeWord => _encryptionCodeWord;
        public string GameScene => _gameScene;
        public string GameScreensaverScene => _gameScreensaverScene;
        public string InitialScene => _initialScene;
        public string SaveFileName => _saveFileName;
        public AssetLabelReference LevelLabel => _levelLabel;
    }
}