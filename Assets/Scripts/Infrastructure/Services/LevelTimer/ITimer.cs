namespace Assets.Scripts.Infrastructure.Services.LevelTimer
{
    public interface ITimer : IService
    {
        delegate void TimerHandler();

        event TimerHandler TimerIsEnded;

        float SecondsLeft { get; }

        void StartTimer();

        void StopTimer();
    }
}