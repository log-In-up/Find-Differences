using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.SaveLoad;
using Assets.Scripts.Infrastructure.Services.StaticData;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterface.DialogueScreens
{
    public class WinWindow : GameResultWindow
    {
        [SerializeField]
        private Button _nextLevel;

        private IStaticDataService _dataService;
        private IGameDialogUI _dialogUI;
        private IPersistentProgressService _persistentProgress;
        private ISaveLoadService _saveLoadService;
        private IGameStateMachine _stateMachine;
        public override DialogWindowID ID => DialogWindowID.Win;

        public override void Activate()
        {
            _nextLevel.onClick.AddListener(OnClickNextLevel);

            base.Activate();
        }

        public override void Deactivate()
        {
            _nextLevel.onClick.RemoveListener(OnClickNextLevel);

            base.Deactivate();
        }

        public override void Setup(ServiceLocator serviceLocator)
        {
            base.Setup(serviceLocator);

            _dataService = serviceLocator.GetService<IStaticDataService>();
            _dialogUI = serviceLocator.GetService<IGameDialogUI>();
            _persistentProgress = serviceLocator.GetService<IPersistentProgressService>();
            _saveLoadService = serviceLocator.GetService<ISaveLoadService>();
            _stateMachine = serviceLocator.GetService<IGameStateMachine>();
        }

        private async void OnClickNextLevel()
        {
            _dialogUI.CloseDialogWindows();

            uint nextLevelIndex = _dataService.GetNextLevelIndex(_persistentProgress.CurrentGameData.CurrentLevel);
            _persistentProgress.CurrentGameData.CurrentLevel = nextLevelIndex;

            await _saveLoadService.Save();

            _stateMachine.Enter<LoadLevelState, uint>(nextLevelIndex);
        }
    }
}