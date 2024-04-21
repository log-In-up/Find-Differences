using Assets.Scripts.StaticData;
using System.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        LevelStaticData GetLevel(uint index);

        Task LoadDataAsync();

        uint GetNextLevelIndex(uint index);
    }
}