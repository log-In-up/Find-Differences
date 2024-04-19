namespace Assets.Scripts.Infrastructure.Services
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;

        public ServiceLocator()
        {
            _instance = this;
        }

        public static ServiceLocator Container => _instance ??= new ServiceLocator();

        public TService GetService<TService>() where TService : IService
        {
            return Implementation<TService>.ServiceInstance;
        }

        public void RegisterService<TService>(TService implementation) where TService : IService
        {
            Implementation<TService>.ServiceInstance = implementation;
        }

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }
    }
}