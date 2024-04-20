using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.StaticData;
using Assets.Scripts.UserInterface;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class Game
    {
        private readonly GameStaticData _gameStaticData;
        private readonly GameUI _hud;
        private ISceneLoader _sceneLoader;
        private ServiceInitializer _serviceInitializer;
        private ServiceLocator _serviceLocator;
        private IGameStateMachine _stateMachine;

        public Game(ICoroutineRunner coroutineRunner, GameUI hud, GameStaticData gameStaticData)
        {
            _gameStaticData = gameStaticData;
            _hud = hud;

            _serviceLocator = new ServiceLocator();
            _sceneLoader = new SceneLoader(coroutineRunner, gameStaticData);
            _stateMachine = new GameStateMachine(_sceneLoader, _serviceLocator);
            _serviceInitializer = new ServiceInitializer(_serviceLocator, _stateMachine, gameStaticData, _sceneLoader, coroutineRunner);
        }

        ~Game()
        {
            _serviceLocator = null;
            _sceneLoader = null;
            _stateMachine = null;
            _serviceInitializer = null;
        }

        internal async void Launch()
        {
            GameUI hud = CreateAndRegisterHUD();
            hud.OpenScreen(ScreenID.Title);

            await _serviceInitializer.RegisterServicesAsync();
            _stateMachine.InitializeStateMashine();

            hud.InitializeScreens(_serviceLocator);
            hud.InitializeWindows(_serviceLocator);

            _sceneLoader.Load(_gameStaticData.InitialScene, EnterLoadLevel);
        }

        private GameUI CreateAndRegisterHUD()
        {
            GameUI hud = Object.Instantiate(_hud);
            Object.DontDestroyOnLoad(hud);

            _serviceLocator.RegisterService<IGameUI>(hud);
            _serviceLocator.RegisterService<IGameDialogUI>(hud);

            return hud;
        }

        internal void Stop()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}