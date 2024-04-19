using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.SaveLoad;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.UserInterface;
using System;
using System.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameUI _gameUI;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ISceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;

        public LoadProgressState(GameStateMachine stateMachine,
            IGameUI gameUI,
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadService,
            ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _gameUI = gameUI;
            _sceneLoader = sceneLoader;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public async void Enter()
        {
            await LoadProgress();

            _sceneLoader.LoadScreensaverScene(OnSceneLoad);
        }

        public void Exit()
        {
        }

        private async Task LoadProgress()
        {
            GameData gameData = await _saveLoadService.Load();
            _progressService.CurrentGameData = gameData ?? new GameData();

            await _saveLoadService.Save();
        }

        private void OnSceneLoad()
        {
            _gameUI.OpenScreen(ScreenID.Main);
            _stateMachine.Enter<PreGameLoopState>();
        }
    }
}