namespace Assets.Scripts.Infrastructure.Services.AppodealService
{
    public interface IAppodealService : IService
    {
        void Initialize();

        void ShowInterstitial();
    }
}