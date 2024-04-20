using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;
using System;
using Assets.Scripts.Infrastructure.Services.IAP;
using Assets.Scripts.Infrastructure.Services.SaveLoad;

namespace Assets.Scripts.UserInterface.Screens
{
    public class MainScreen : Screen
    {
        [SerializeField]
        private CodelessIAPButton _buySeconds;

        [SerializeField]
        private Button _quit;

        [SerializeField]
        private Button _start;

        private IIAPService _iapService;
        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;
        private IGameStateMachine _stateMachine;

        public override ScreenID ID => ScreenID.Main;

        public override void Activate()
        {
            _buySeconds.onPurchaseComplete.AddListener(OnPurchaseSeconds);
            _start.onClick.AddListener(OnClickStart);
            _quit.onClick.AddListener(OnClickQuit);

            base.Activate();
        }

        public override void Deactivate()
        {
            _buySeconds.onPurchaseComplete.RemoveListener(OnPurchaseSeconds);
            _quit.onClick.RemoveListener(OnClickQuit);
            _start.onClick.RemoveListener(OnClickStart);

            base.Deactivate();
        }

        public override void Setup(ServiceLocator serviceLocator)
        {
            base.Setup(serviceLocator);

            _iapService = serviceLocator.GetService<IIAPService>();
            _persistentProgressService = serviceLocator.GetService<IPersistentProgressService>();
            _saveLoadService = serviceLocator.GetService<ISaveLoadService>();
            _stateMachine = serviceLocator.GetService<IGameStateMachine>();
        }

        private void OnClickQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnClickStart()
        {
            uint level = _persistentProgressService.CurrentGameData.CurrentLevel;

            _stateMachine.Enter<LoadLevelState, uint>(level);
        }

        private void OnPurchaseSeconds(Product product)
        {
            _iapService.AddGameplayTime();
            _saveLoadService.Save();
        }
    }
}