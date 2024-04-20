using Assets.Scripts.Infrastructure.Services.LevelTimer;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.UserInterface;

namespace Assets.Scripts.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameDialogUI _gameDialogUI;
        private readonly IGameUI _gameUI;
        private readonly GameStateMachine _stateMachine;
        private readonly ITimer _timer;

        public GameLoopState(GameStateMachine stateMachine,
            IGameDialogUI gameDialogUI,
            IGameUI gameUI,
            ITimer timer)
        {
            _stateMachine = stateMachine;
            _gameDialogUI = gameDialogUI;
            _gameUI = gameUI;
            _timer = timer;
        }

        public void Enter()
        {
            _timer.TimerIsEnded += TimerIsEnded;

            _timer.StartTimer();
            _gameUI.OpenScreen(ScreenID.Gameplay);
        }

        public void Exit()
        {
            _timer.TimerIsEnded -= TimerIsEnded;
            _timer.StopTimer();
        }

        private void TimerIsEnded()
        {
            _gameDialogUI.OpenDialogWindow(DialogWindowID.Defeat);
        }
    }
}