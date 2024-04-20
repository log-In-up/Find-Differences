using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.AppodealService;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.Infrastructure.States;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UserInterface.DialogueScreens
{
    public abstract class GameResultWindow : DialogueWindow, IInterstitialAdListener
    {
        [SerializeField]
        private Button _mainMenu, _restart;

        private IAppodealService _appodealService;
        private IGameDialogUI _gameDialogUI;
        private IGameStateMachine _gameStateMachine;
        private IGameUI _gameUI;
        private IPersistentProgressService _persistentProgressService;
        private ISceneLoader _sceneLoader;

        public override void Activate()
        {
            _mainMenu.onClick.AddListener(OnClickMainMenu);
            _restart.onClick.AddListener(OnRestart);

            base.Activate();
        }

        public override void Deactivate()
        {
            _mainMenu.onClick.RemoveListener(OnClickMainMenu);
            _restart.onClick.RemoveListener(OnRestart);

            base.Deactivate();
        }

        public void onInterstitialClicked()
        {
            Debug.Log("Interstitial Clicked");

            ReloadLevel();
        }

        public void onInterstitialClosed()
        {
            Debug.Log("Interstitial Closed");

            ReloadLevel();
        }

        public void onInterstitialExpired()
        {
            Debug.Log("Interstitial Expired");

            ReloadLevel();
        }

        public void onInterstitialFailedToLoad()
        {
            Debug.LogWarning("Interstitial Failed To Load");

            ReloadLevel();
        }

        public void onInterstitialLoaded(bool isPrecache)
        {
            Debug.Log("Interstitial Loaded");
        }

        public void onInterstitialShowFailed()
        {
            Debug.LogWarning("Interstitial Show Failed");

            ReloadLevel();
        }

        public void onInterstitialShown()
        {
            Debug.Log("Interstitial Shown");
        }

        public override void Setup(ServiceLocator serviceLocator)
        {
            Appodeal.setInterstitialCallbacks(this);

            base.Setup(serviceLocator);

            _appodealService = serviceLocator.GetService<IAppodealService>();
            _gameDialogUI = serviceLocator.GetService<IGameDialogUI>();
            _gameStateMachine = serviceLocator.GetService<IGameStateMachine>();
            _gameUI = serviceLocator.GetService<IGameUI>();
            _persistentProgressService = serviceLocator.GetService<IPersistentProgressService>();
            _sceneLoader = serviceLocator.GetService<ISceneLoader>();
        }

        protected virtual void OnClickMainMenu()
        {
            _gameDialogUI.CloseDialogWindows();
            _gameUI.OpenScreen(ScreenID.Loading);

            _sceneLoader.LoadScreensaverScene(OnSceneLoad);
        }

        private void OnSceneLoad()
        {
            _gameStateMachine.Enter<PreGameLoopState>();
        }

        protected virtual void OnRestart()
        {
            _gameDialogUI.CloseDialogWindows();

#if UNITY_EDITOR && UNITY_ANDROID
            ReloadLevel();
#elif UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IPHONE
            ReloadLevel();
#elif UNITY_ANDROID
            _appodealService.ShowInterstitial();
#elif UNITY_IPHONE
            _appodealService.ShowInterstitial();
#else
            ReloadLevel();
#endif
        }

        private void ReloadLevel()
        {
            uint level = _persistentProgressService.CurrentGameData.CurrentLevel;
            _gameStateMachine.Enter<LoadLevelState, uint>(level);
        }
    }
}