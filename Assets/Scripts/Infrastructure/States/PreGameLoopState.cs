using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.UserInterface;

namespace Assets.Scripts.Infrastructure.States
{
    public class PreGameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IGameUI _gameUI;

        public PreGameLoopState(GameStateMachine stateMachine,
            IGameUI gameUI)
        {
            _stateMachine = stateMachine;
            _gameUI = gameUI;
        }

        public void Enter()
        {
            _gameUI.OpenScreen(ScreenID.Main);
        }

        public void Exit()
        {
        }
    }
}