using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.Services.LevelTimer;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UserInterface.Screens
{
    public class GameplayScreen : Screen
    {
        [SerializeField]
        private TextMeshProUGUI _timer, _levelCounter;

        private IPersistentProgressService _progressService;
        private TimeSpan _time;
        private ITimer _timerService;

        public override ScreenID ID => ScreenID.Gameplay;

        public override void Activate()
        {
            _time = TimeSpan.FromSeconds(_timerService.SecondsLeft);
            _timer.text = _time.ToString(@"mm\:ss");
            _levelCounter.text = $"{_progressService.CurrentGameData.CurrentLevel + 1}";

            _timerService.TimerIsEnded += TimerIsEnded;

            base.Activate();
        }

        public override void Deactivate()
        {
            _timerService.TimerIsEnded -= TimerIsEnded;

            base.Deactivate();
        }

        public override void Setup(ServiceLocator serviceLocator)
        {
            base.Setup(serviceLocator);

            _timerService = serviceLocator.GetService<ITimer>();
            _progressService = serviceLocator.GetService<IPersistentProgressService>();
        }

        private void TimerIsEnded()
        {
            _time = TimeSpan.FromSeconds(0.0f);
            _timer.text = _time.ToString(@"mm\:ss");
        }

        private void Update()
        {
            if (_timerService.SecondsLeft >= 0.0f)
            {
                _time = TimeSpan.FromSeconds(_timerService.SecondsLeft);
                _timer.text = _time.ToString(@"mm\:ss");
            }
        }
    }
}