using Assets.Scripts.Infrastructure.Services;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public interface ISceneLoader : IService
    {
        void Load(string sceneName, Action onLoaded = null);

        void LoadGameScene(Action onLoaded = null);

        void LoadScreensaverScene(Action onLoaded = null);
    }
}