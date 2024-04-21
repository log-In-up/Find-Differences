using Assets.Scripts.Infrastructure.AssetManagement;
using Assets.Scripts.StaticData;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Assets.Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly GameStaticData _gameStaticData;

        private Dictionary<uint, LevelStaticData> _levelsCache;

        public StaticDataService(IAssetProvider assetProvider, GameStaticData gameStaticData)
        {
            _assetProvider = assetProvider;
            _gameStaticData = gameStaticData;

            _levelsCache = new Dictionary<uint, LevelStaticData>();
        }

        ~StaticDataService()
        {
            _levelsCache.Clear();
            _levelsCache = null;
        }

        public LevelStaticData GetLevel(uint index)
        {
            return _levelsCache[index];
        }

        public uint GetNextLevelIndex(uint index)
        {
            //items[(index + 1) % items.Count];
            LevelStaticData level = _levelsCache[(index + 1) % (uint)_levelsCache.Count];

            return level.LevelIndex;
        }

        public async Task LoadDataAsync()
        {
            IList<IResourceLocation> locations = await _assetProvider.LoadByLabel(_gameStaticData.LevelLabel.labelString, typeof(LevelStaticData));

            foreach (IResourceLocation location in locations)
            {
                LevelStaticData handle = await _assetProvider.Load<LevelStaticData>(location);

                _levelsCache.Add(handle.LevelIndex, handle);
            }
        }
    }
}