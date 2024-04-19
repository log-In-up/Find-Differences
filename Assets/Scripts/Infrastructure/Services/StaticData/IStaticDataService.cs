using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Logic;
using Assets.Scripts.StaticData;
using System.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        LevelStaticData GetLevel(uint index);

        Task LoadDataAsync();
    }
}