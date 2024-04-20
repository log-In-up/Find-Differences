using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.StaticData;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.LevelTimer
{
    public class LevelTimerService : ITimer
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IPersistentProgressService _progressService;
        private readonly float _levelTotalSeconds;
        private float _secondsLeft;
        private Coroutine _timer;

        public LevelTimerService(ICoroutineRunner coroutineRunner,
            IPersistentProgressService progressService,
            GameStaticData gameStaticData)
        {
            _coroutineRunner = coroutineRunner;
            _progressService = progressService;
            _levelTotalSeconds = gameStaticData.LevelTotalSeconds;
        }

        public event ITimer.TimerHandler TimerIsEnded;

        public float SecondsLeft => _secondsLeft;

        public void StartTimer()
        {
            _timer = _coroutineRunner.StartCoroutine(Timer());
        }

        public void StopTimer()
        {
            if (_timer != null)
            {
                _coroutineRunner.StopCoroutine(_timer);
            }
        }

        private IEnumerator Timer()
        {
            _secondsLeft = _levelTotalSeconds + _progressService.CurrentGameData.ExtraSeconds;

            while (_secondsLeft >= 0.0f)
            {
                _secondsLeft -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            _secondsLeft = 0.0f;

            TimerIsEnded?.Invoke();
        }
    }
}