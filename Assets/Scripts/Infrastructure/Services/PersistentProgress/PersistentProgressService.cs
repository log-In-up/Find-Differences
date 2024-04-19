using Assets.Scripts.Data;

namespace Assets.Scripts.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public GameData CurrentGameData { get; set; }
    }
}