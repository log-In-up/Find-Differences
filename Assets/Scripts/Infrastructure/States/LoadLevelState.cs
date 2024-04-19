using Assets.Scripts.Infrastructure.AssetManagement;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.UserInterface;

namespace Assets.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<uint>
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGameUI _gameUI;
        private readonly ISceneLoader _sceneLoader;
        private readonly GameStateMachine _stateMachine;
        private uint _payload;

        public LoadLevelState(GameStateMachine stateMachine,
            IGameFactory gameFactory,
            IGameUI gameUI,
            ISceneLoader sceneLoader)
        {
            _gameFactory = gameFactory;
            _gameUI = gameUI;
            _sceneLoader = sceneLoader;

            _stateMachine = stateMachine;
        }

        public void Enter(uint payload)
        {
            _payload = payload;

            _gameFactory.CleanUp();

            _gameUI.OpenScreen(ScreenID.Loading);
            _sceneLoader.LoadGameScene(OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _gameFactory.SpawnGameLevel(_payload);
            _stateMachine.Enter<GameLoopState>();
        }
    }
}