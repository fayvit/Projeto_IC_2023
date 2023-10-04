//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//#if UNITY_ANDROID
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//#endif

//public class GooglePlayTrophyDictionary
//{
//    public static void Start()
//    {
//#if UNITY_ANDROID
        
//        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
//        PlayGamesPlatform.InitializeInstance(config);
//        Debug.Log("activate: "+PlayGamesPlatform.Activate().localUser);

//        if (Social.localUser.authenticated)
//            Debug.Log("esta autenticado");
//        else
//            Debug.Log("não autenticado");

//        GlobalController.g.StartCoroutine(SigIn());
        
//#endif
//    }

//    public static IEnumerator SigIn()
//    {

//        yield return new WaitForEndOfFrame();
//        Debug.Log("passei por aqui");

//        Social.localUser.Authenticate(succes=> {
//            if (succes)
//            {
                
//                Debug.Log("aconteceu autenticação");
//            }
//            else
//            {
//                Debug.Log("falha na autenticação");
//            }

//            EventAgregator.Publish(EventKey.googlePlayTrySingInFinish);
//            });
//    }

//    public static void VerifyTrophy(TrophyId id)
//    {
//        string stringID = "0";

//        switch (id)
//        {
//            case TrophyId.coleteUmFragmentoDeHexagono:
//                stringID = GPGSIds.achievement_fragmented_life;
//            break;
//            case TrophyId.coloqueEmblemaNaEspada:
//                stringID = GPGSIds.achievement_sword_emblem;
//            break;
//            case TrophyId.coleteUmFragmentoDePentagono:
//                stringID = GPGSIds.achievement_fragmented_power;
//            break;
//            case TrophyId.coleteUmLosango:
//                stringID = GPGSIds.achievement_the_secret_of_the_vault;
//            break;
//            case TrophyId.completeUmHexagono:
//                stringID = GPGSIds.achievement_living_vigorously;
//            break;
//            case TrophyId.completeUmPentagono:
//                stringID = GPGSIds.achievement_all_power_to_the_people;
//            break;
//            case TrophyId.abraUmCofre:
//                stringID = GPGSIds.achievement_obtaining_values;
//            break;
//            case TrophyId.derroteGrandeCirculo:
//                stringID = GPGSIds.achievement_the_big_imperfect_circle;
//            break;
//            case TrophyId.derroteMagoSetaSombria:
//                stringID = GPGSIds.achievement_the_shadow_arrow_wizard;
//            break;
//            case TrophyId.encontreFinalNaGarganta:
//                stringID = GPGSIds.achievement_rising_from_the_depths;
//            break;
//            case TrophyId.encontreFinalNoAquifero:
//                stringID = GPGSIds.achievement_i_went_down_the_rapids;
//            break;
//        }

//        ProcurarTrofeuParaGanhar(stringID);
//    }

//    static void ProcurarTrofeuParaGanhar(string id)
//    {
//       if(Social.localUser.authenticated)
//         Social.ReportProgress(id, 100, AdicionouTrofeu);
//    }

//    static void AdicionouTrofeu(bool foi)
//    {
//        if (foi)
//            Social.ReportProgress("", 9, foiD =>
//            {
//                if (foiD)
//                    Debug.Log("pegou todos trofeus");
//                else
//                    Debug.Log("passou pelarotina de todos os trofeus mas não pegou");
//            });
//        else
//        {
//            Debug.Log("falha ao tentar liberar um trofeu");
//        }
//    }
//}