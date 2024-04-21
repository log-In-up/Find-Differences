using Assets.Scripts.Infrastructure.Services.LevelTimer;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.SaveLoad;
using Assets.Scripts.Infrastructure.Services.StaticData;
using Assets.Scripts.Infrastructure.Services.UserInterface;
using Assets.Scripts.Logic;
using Assets.Scripts.UserInterface;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.States
{
    public class GameLoopState : IPayloadedState<LevelHandler>
    {
        private readonly IGameDialogUI _gameDialogUI;
        private readonly IGameUI _gameUI;

        private readonly ITimer _timer;
        private LevelHandler _payload;

        public GameLoopState(IGameDialogUI gameDialogUI,
            IGameUI gameUI,
            ITimer timer)
        {
            _gameDialogUI = gameDialogUI;
            _gameUI = gameUI;
            _timer = timer;
        }

        ~GameLoopState()
        {
            _payload = null;
        }

        public void Enter(LevelHandler payload)
        {
            _payload = payload;

            _payload.OnAllObjectsFind += OnAllObjectsFind;
            _timer.TimerIsEnded += TimerIsEnded;

            _timer.StartTimer();
            _gameUI.OpenScreen(ScreenID.Gameplay);
        }

        public void Exit()
        {
            _payload.OnAllObjectsFind -= OnAllObjectsFind;
            _timer.TimerIsEnded -= TimerIsEnded;
            _timer.StopTimer();

            Object.Destroy(_payload.gameObject);
        }

        private void OnAllObjectsFind()
        {
            _timer.StopTimer();

            _gameDialogUI.OpenDialogWindow(DialogWindowID.Win);
        }

        private void TimerIsEnded()
        {
            _gameDialogUI.OpenDialogWindow(DialogWindowID.Defeat);
        }
    }
}