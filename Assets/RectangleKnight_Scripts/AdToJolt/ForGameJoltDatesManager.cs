using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJolt.API;

public class ForGameJoltDatesManager
{
    public static bool estaCarregado = false;

    public static void Save(SaveDatesManager s)
    {
        if (GameJoltAPI.Instance != null)
            if (s != null && GameJoltAPI.Instance.CurrentUser != null)
            {
                Debug.Log("salvou: " + GameJoltAPI.Instance.CurrentUser.ID.ToString());
                byte[] sb = SaveDatesManager.SaveDatesForBytes();
                preJSON pre = new preJSON() { b = sb };

                DataStore.Set(GameJoltAPI.Instance.CurrentUser.ID.ToString(),
                        JsonUtility.ToJson(pre), true,
                       Acertou);
            }
    }

    public static void Load()
    {

        if (GameJoltAPI.Instance.CurrentUser != null)
        {

            DataStore.Get(GameJoltAPI.Instance.CurrentUser.ID.ToString(), true, (string S2) => {
                if (!string.IsNullOrEmpty(S2))
                {
                    EventAgregator.Publish(new StandardSendGameEvent(EventKey.testLoadForJolt, "dados do jolt"));
                    Debug.Log("Dados Carregados do Jolt");
                    SaveDatesManager.SetSavesWithBytes(JsonUtility.FromJson<preJSON>(S2).b);
                }
                else
                {
                    EventAgregator.Publish(new StandardSendGameEvent(EventKey.testLoadForJolt, "string nula do Jolt"));
                    Debug.Log("string nula do Jolt");
                    new SaveDatesManager();
                }

                GameObject.FindObjectOfType<GlobalController>().StartCoroutine(Carregado());
            });
        }
    }

    static void Acertou(bool foi)
    {
        if (foi)
        {
            EventAgregator.Publish(new StandardSendGameEvent(EventKey.testLoadForJolt, "Parece que algo deu certo"));
            Debug.Log("Deu certo" + SaveDatesManager.s.SavedGames.Count);
        }
        else
        {
            EventAgregator.Publish(new StandardSendGameEvent(EventKey.testLoadForJolt, "parece que algo deu errado"));
            Debug.Log("algo errado");
        }
    }


    static IEnumerator Carregado()
    {
        yield return new WaitForEndOfFrame();
        estaCarregado = true;
    }
}