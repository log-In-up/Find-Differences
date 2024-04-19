using Assets.Scripts.Data;
using System.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        Task Initialize();

        Task<GameData> Load();

        Task Save();
    }
}