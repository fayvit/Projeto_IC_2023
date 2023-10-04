using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJoltTrophyDictionary : MonoBehaviour
{
    public static void VerifyTrophy(TrophyId id)
    {
        int gameJoltID = 0;

        switch (id)
        {
            case TrophyId.coleteUmFragmentoDeHexagono:
                gameJoltID = 109488;
            break;
            case TrophyId.coloqueEmblemaNaEspada:
                gameJoltID = 109487;
            break;
            case TrophyId.coleteUmFragmentoDePentagono:
                gameJoltID = 109489;
            break;
            case TrophyId.coleteUmLosango:
                gameJoltID = 109490;
            break;
            case TrophyId.completeUmHexagono:
                gameJoltID = 109491;
            break;
            case TrophyId.completeUmPentagono:
                gameJoltID = 109492;
            break;
            case TrophyId.abraUmCofre:
                gameJoltID = 109493;
            break;
            case TrophyId.derroteGrandeCirculo:
                gameJoltID = 109494;
            break;
            case TrophyId.derroteMagoSetaSombria:
                gameJoltID = 109495;
            break;
            case TrophyId.encontreFinalNaGarganta:
                gameJoltID = 109496;
            break;
            case TrophyId.encontreFinalNoAquifero:
                gameJoltID = 109497;
            break;
        }

        ProcurarTrofeuParaGanhar(gameJoltID);
    }

    public static void ReBloquearTrofeus()
    {
        Debug.Log("cheguei no rebloquear");

        if (GameJolt.API.GameJoltAPI.Instance != null)
            if (GameJolt.API.GameJoltAPI.Instance.CurrentUser != null)
                GameJolt.API.Trophies.Get(QueroBloquearTodos);
        
    }

    public static void ProcurarTrofeuParaGanhar(int ID)
    {
        if(GameJolt.API.GameJoltAPI.Instance!=null)
            if (GameJolt.API.GameJoltAPI.Instance.CurrentUser != null)
                GameJolt.API.Trophies.Get(ID, GanheiTrofeu);

    }

    private static void QueroBloquearTodos(GameJolt.API.Objects.Trophy[] T)
    {
        for (int i = 0; i < T.Length; i++)
        {
            T[i].Remove(SeraQueRemovi);
        }
    }

    private static void SeraQueRemovi(bool obj)
    {
        if (obj)
        {
            Debug.Log("me disseram que deu certo");
        }
        else
        {
            Debug.Log("Isso me retornou um erro");
        }
    }

    public static void GanheiTrofeu(GameJolt.API.Objects.Trophy t)
    {
        if (!t.Unlocked)
        {
            t.Unlock((bool success) => {
                if (success)
                {
                    Debug.Log("Success!");
                }
                else
                {
                    Debug.Log("Something went wrong");
                }
            });
            
        }

        GameController.g.StartCoroutine(VerificaGanheirTodos());
        

    }

    static IEnumerator VerificaGanheirTodos()
    {
        yield return new WaitForSeconds(2);
        GameJolt.API.Trophies.Get(SeraQueGanheiTodos);
    }

    public static void SeraQueGanheiTodos(GameJolt.API.Objects.Trophy[] T)
    {
        bool foi = true;
        GameJolt.API.Objects.Trophy trofeuQInteressa = null;
        for (int i = 0; i < T.Length; i++)
        {
            if (T[i].ID != 109572)
            {
                //Debug.Log(T[i].Title);

                foi &= T[i].Unlocked;
            }
            else
            {
                //Debug.Log("Esse interessa: "+T[i].Title);
                trofeuQInteressa = T[i];
            }
        }

        if (foi && T.Length > 0 && trofeuQInteressa != null)
        {
            if (!trofeuQInteressa.Unlocked)
                trofeuQInteressa.Unlock();
        }
    }
}
