using System.Collections;
using UnityEngine.Purchasing;
using UnityEngine;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.StaticData;
using Assets.Scripts.Infrastructure.Services.SaveLoad;

namespace Assets.Scripts.Infrastructure.Services.IAP
{
    public class IAPService : IIAPService
    {
        private readonly GameStaticData _gameStaticData;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public IAPService(GameStaticData gameStaticData,
            IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _gameStaticData = gameStaticData;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void AddGameplayTime()
        {
            _progressService.CurrentGameData.ExtraSeconds += _gameStaticData.ExtraSecondsOnBuy;
            _saveLoadService.Save();
        }
    }
}