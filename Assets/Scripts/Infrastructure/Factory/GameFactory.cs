using Assets.Scripts.Infrastructure.AssetManagement;
using Assets.Scripts.Infrastructure.Services.StaticData;
using Assets.Scripts.Logic;
using Assets.Scripts.StaticData;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        public GameFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        ~GameFactory()
        {
        }

        public void CleanUp()
        {
            _assetProvider.CleanUp();
        }

        public void SpawnGameLevel(uint index)
        {
            LevelStaticData levelHandler = _staticData.GetLevel(index);
            LevelHandler level = Object.Instantiate(levelHandler.LevelHandler);

            level.SetTopDifferences(levelHandler.TopImages);
            level.SetBottomDifferences(levelHandler.BottomImages);
        }
    }
}