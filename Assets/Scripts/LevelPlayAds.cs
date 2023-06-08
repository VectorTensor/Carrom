using System;
using UnityEngine;

public class LevelPlayAds : MonoBehaviour
{
    void Start()
    {
        IronSource.Agent.init("1a4c4ed95");
        IronSource.Agent.validateIntegration();
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void OnEnable()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;

        //Add AdInfo Banner Events
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;
        /************* Banner AdInfo Delegates *************/
    }

    //after initialze completet load banner
    private void SdkInitializationCompletedEvent(){}

    //Invoked once the banner has loaded
    private void BannerOnAdLeftApplicationEvent(IronSourceAdInfo obj)
    {
        throw new NotImplementedException();
    }
    private void BannerOnAdScreenDismissedEvent(IronSourceAdInfo obj)
    {
        throw new NotImplementedException();
    }
    private void BannerOnAdScreenPresentedEvent(IronSourceAdInfo obj)
    {
        throw new NotImplementedException();
    }
    private void BannerOnAdClickedEvent(IronSourceAdInfo obj)
    {
        throw new NotImplementedException();
    }
    private void BannerOnAdLoadFailedEvent(IronSourceError obj)
    {
        Debug.Log("Banner Load Failed");
        throw new NotImplementedException();
    }
    private void BannerOnAdLoadedEvent(IronSourceAdInfo obj)
    {
        Debug.Log("Banner Loaded");
        throw new NotImplementedException();
    }

    //Buttons
    public void LoadBanner()
    {
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
    }

    public void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }
}
