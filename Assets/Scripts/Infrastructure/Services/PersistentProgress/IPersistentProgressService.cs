using Assets.Scripts.Data;

namespace Assets.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        GameData CurrentGameData { get; set; }
    }
}