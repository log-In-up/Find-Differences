using Assets.Scripts.Infrastructure.AssetManagement;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.Infrastructure.Services.SaveLoad;
using Assets.Scripts.Infrastructure.Services.StaticData;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.StaticData;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services
{
    public class ServiceInitializer
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;
        private readonly IGameStateMachine _stateMachine;
        private readonly GameStaticData _gameStaticData;

        public ServiceInitializer(ServiceLocator serviceLocator,
            IGameStateMachine stateMachine,
            GameStaticData gameStaticData,
            ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _gameStaticData = gameStaticData;
            _serviceLocator = serviceLocator;

            _sceneLoader = sceneLoader;
        }

        public async Task RegisterServicesAsync()
        {
            _serviceLocator.RegisterService(_sceneLoader);
            _serviceLocator.RegisterService(_stateMachine);

            IAssetProvider assetProvider = new AssetProvider();
            await assetProvider.Initialize();
            _serviceLocator.RegisterService(assetProvider);

            IStaticDataService staticDataService = new StaticDataService(assetProvider, _gameStaticData);
            await staticDataService.LoadDataAsync();
            _serviceLocator.RegisterService(staticDataService);

            _serviceLocator.RegisterService<IGameFactory>(new GameFactory(assetProvider, staticDataService));

            await RegisterSaveLoadServiceAsync();
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