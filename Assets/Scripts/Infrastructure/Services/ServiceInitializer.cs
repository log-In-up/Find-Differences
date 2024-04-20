using Assets.Scripts.Infrastructure.AssetManagement;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services.AppodealService;
using Assets.Scripts.Infrastructure.Services.IAP;
using Assets.Scripts.Infrastructure.Services.LevelTimer;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.SaveLoad;
using Assets.Scripts.Infrastructure.Services.StaticData;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.StaticData;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public class ServiceInitializer
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ServiceLocator _serviceLocator;
        private readonly IGameStateMachine _stateMachine;
        private readonly GameStaticData _gameStaticData;

        public ServiceInitializer(ServiceLocator serviceLocator,
            IGameStateMachine stateMachine,
            GameStaticData gameStaticData,
            ISceneLoader sceneLoader,
            ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _gameStaticData = gameStaticData;
            _serviceLocator = serviceLocator;

            _sceneLoader = sceneLoader;
            _coroutineRunner = coroutineRunner;
        }

        public async Task RegisterServicesAsync()
        {
            _serviceLocator.RegisterService(_sceneLoader);
            _serviceLocator.RegisterService(_stateMachine);

            await RegisterSaveLoadServiceAsync();

            _serviceLocator.RegisterService<IIAPService>(new IAPService(
                _serviceLocator.GetService<IPersistentProgressService>(),
                _gameStaticData));

            RegisterAppodealService();
            RegisterLevelTimeService();

            IAssetProvider assetProvider = new AssetProvider();
            await assetProvider.Initialize();
            _serviceLocator.RegisterService(assetProvider);

            IStaticDataService staticDataService = new StaticDataService(assetProvider, _gameStaticData);
            await staticDataService.LoadDataAsync();
            _serviceLocator.RegisterService(staticDataService);

            _serviceLocator.RegisterService<IGameFactory>(new GameFactory(assetProvider, staticDataService));
        }

        private void RegisterLevelTimeService()
        {
            _serviceLocator.RegisterService<ITimer>(new LevelTimerService(
                            _coroutineRunner,
                            _serviceLocator.GetService<IPersistentProgressService>(),
                            _gameStaticData));
        }

        private void RegisterAppodealService()
        {
            IAppodealService appodealService = new AppodealService.AppodealService();
            appodealService.Initialize();

            _serviceLocator.RegisterService(appodealService);
        }

        private async Task RegisterSaveLoadServiceAsync()
        {
            IPersistentProgressService persistentProgressService = new PersistentProgressService();

            ISaveLoadService saveLoadService = new SaveLoadService(
                    persistentProgressService,
                    Application.persistentDataPath,
                    _gameStaticData.SaveFileName,
                    _gameStaticData.EncryptionCodeWord);

            await saveLoadService.Initialize();

            _serviceLocator.RegisterService(saveLoadService);
            _serviceLocator.RegisterService(persistentProgressService);
        }
    }
}