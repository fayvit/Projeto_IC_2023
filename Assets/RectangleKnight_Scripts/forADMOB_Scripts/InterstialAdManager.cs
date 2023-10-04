//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//#if UNITY_ANDROID
//using GoogleMobileAds.Api;
//#endif
//using System;

//public static class InterstialAdManager
//{
//    #if UNITY_ANDROID
//    private  static InterstitialAd interstial;
//#endif
//    private static EstadoDaqui estado = EstadoDaqui.emEspera;


//#if UNITY_ANDROID
//    private static string adUnitId = "ca-app-pub-3266598740143271/1689718595";
//#elif UNITY_IPHONE
//     private static string adUnitId = "ID_DA_CAMPANHA_IOS";
//#else
//    private static string adUnitId = "plataformaNaoSuportada";
//#endif

//    private enum EstadoDaqui
//    {
//        emEspera,
//        preparando,
//        pronto
//    }

//    public static void AgendarEventos()
//    {
//        EventAgregator.AddListener(EventKey.requestShowInterstial, RequestShowInterstial);
//        EventAgregator.AddListener(EventKey.prepareInterstial, PrepareInterstial);
//    }

//    public static void RemoverEventos()
//    {
//        EventAgregator.RemoveListener(EventKey.requestShowInterstial, RequestShowInterstial);
//        EventAgregator.RemoveListener(EventKey.prepareInterstial, PrepareInterstial);
//    }

//    public static void PrepareInterstial(IGameEvent e)
//    {
//        if (estado == EstadoDaqui.emEspera)
//        {
//            Debug.Log("preparando");
//            #if UNITY_ANDROID
//            interstial = new InterstitialAd(adUnitId);

//            AdRequest request = new AdRequest.Builder()
//              //  .AddTestDevice("AQZ9WOLB9LF6FYCI")
//                .Build();
//            interstial.LoadAd(request);

//            // prepara o listener para quando o banner estiver completamente
//            interstial.OnAdLoaded += OnAdLoadedInterstial;
//            interstial.OnAdFailedToLoad += OnInterstialLoadFailed;
//#endif
//            estado = EstadoDaqui.preparando;
//        }
//    }

//    #if UNITY_ANDROID
//    private static void OnInterstialLoadFailed(object sender, AdFailedToLoadEventArgs e)
//    {
//        Debug.Log("parece uma falha: "+e.Message);
//        estado = EstadoDaqui.emEspera;
//    }
//#endif

//    private static void OnAdLoadedInterstial(object sender, EventArgs e)
//    {
//        Debug.Log("pronto");
//        estado = EstadoDaqui.pronto;
//    }

//    public static void RequestShowInterstial(IGameEvent e)
//    {
//        Debug.Log("pediu um show: "+estado);
//#if UNITY_ANDROID
//        if (estado == EstadoDaqui.pronto && interstial.IsLoaded())
//        {
//            Debug.Log("chegou no show");
//            estado = EstadoDaqui.emEspera;
//            interstial.Show();
//        }
//#endif
//    }
//}