using System.Collections;
using UnityEngine.Purchasing;
using UnityEngine;
using Assets.Scripts.Infrastructure.Services.PersistentProgress;
using Assets.Scripts.StaticData;

namespace Assets.Scripts.Infrastructure.Services.IAP
{
    public class IAPService : IIAPService
    {
        private readonly IPersistentProgressService _progressService;
        private readonly GameStaticData _gameStaticData;

        public IAPService(IPersistentProgressService progressService, GameStaticData gameStaticData)
        {
            _progressService = progressService;
            _gameStaticData = gameStaticData;
        }

        public void AddGameplayTime()
        {
            _progressService.CurrentGameData.ExtraSeconds += _gameStaticData.ExtraSecondsOnBuy;
        }
    }
}