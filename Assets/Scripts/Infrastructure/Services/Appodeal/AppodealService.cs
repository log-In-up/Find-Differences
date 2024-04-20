using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Services.AppodealService
{
    public class AppodealService : IAppodealService, IAppodealInitializationListener
    {
#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IPHONE
        public static string appKey = "";
#elif UNITY_ANDROID
        public static string appKey = "";
#elif UNITY_IPHONE
        public static string appKey = "";
#else
	    public static string appKey = "";
#endif

        public AppodealService()
        {
        }

        public void Initialize()
        {
            Appodeal.setTesting(true);

            Appodeal.setTriggerOnLoadedOnPrecache(Appodeal.INTERSTITIAL, true);
            Appodeal.setAutoCache(Appodeal.INTERSTITIAL, true);

            int adTypes = Appodeal.INTERSTITIAL;
            Appodeal.initialize(appKey, adTypes, this);
        }

        public void onInitializationFinished(List<string> errors)
        {
            if (errors != null)
            {
                string output = string.Join(", ", errors);
                Debug.LogWarning($"OnInitializationFinished (errors:[{output}])");
            }
        }

        public void ShowInterstitial()
        {
            if (Appodeal.isLoaded(Appodeal.INTERSTITIAL) && Appodeal.canShow(Appodeal.INTERSTITIAL, "default") && !Appodeal.isPrecache(Appodeal.INTERSTITIAL))
            {
                Appodeal.show(Appodeal.INTERSTITIAL);
            }
            else
            {
                Appodeal.cache(Appodeal.INTERSTITIAL);
            }
        }
    }
}