using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleMobileAds.Api;

public class TexteAdMob : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


   // public BannerView bannerView = null;

//#if UNITY_ANDROID
 string adUnitId = "ID_DA_CAMPANHA_ANDROID";
//#elif UNITY_IPHONE
// string adUnitId = "ID_DA_CAMPANHA_IOS";
//#endif

    private void RequestBanner()
    {
        /*
        // Cria o banner padrão
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        // Pre para e carrega o banner
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

        // prepara o listener para quando o banner estiver completamente
        bannerView.OnAdLoaded += OnAdLoadedBanner;
    }
    // exibe o banner
    public void OnAdLoadedBanner(object sender, System.EventArgs args)
    {
        this.bannerView.Show();
        */
    }
}
