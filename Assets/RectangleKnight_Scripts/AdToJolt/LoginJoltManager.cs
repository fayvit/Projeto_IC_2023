using UnityEngine;
using UnityEngine.SceneManagement;
using GameJolt.API;
using GameJolt.UI.Controllers;
using System.Collections;

public class LoginJoltManager : MonoBehaviour
{
    [SerializeField] private GameObject JoltManager = default;
    [SerializeField] private GameObject loadBar = default;
    [SerializeField] private GameObject buttonsPanel = default;
    [SerializeField] private GameObject blackcanvas = default;
    [SerializeField] private UnityEngine.UI.Text myText;

    private bool estouCarregando = false;
    private bool estaNoJolt = false;
    // Use this for initialization
    void Start()
    {
        estaNoJolt = UrlVerify.DomainsContainString("gamejolt", myText);

        if (estaNoJolt)
            blackcanvas.SetActive(false);
        else
        {
            SaveDatesManager.Load();
            SceneManager.LoadScene("menuInicial");
        }

        if (!GameObject.Find("GameJoltAPI"))
            JoltManager.SetActive(true);

        EventAgregator.AddListener(EventKey.testLoadForJolt, OnRequestTestLoadForJolt);
    }

    private void OnDestroy()
    {
        EventAgregator.RemoveListener(EventKey.testLoadForJolt, OnRequestTestLoadForJolt);
    }

    private void OnRequestTestLoadForJolt(IGameEvent e)
    {
        StandardSendGameEvent ssge = (StandardSendGameEvent)e;
        myText.text = (string)ssge.MyObject[0];

        loadBar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool isSignedIn = GameJoltAPI.Instance.CurrentUser != null;

        if (isSignedIn)
        {
            GameObject G = GameObject.Find("SignInPanel");
            if (G)
            {
                SignInWindow S = G.GetComponent<SignInWindow>();
                if (S)
                    S.Dismiss(true);
            }


            buttonsPanel.SetActive(false);
            loadBar.SetActive(true);

            if (!estouCarregando)
            {
                AgendeODiferenteDeZero();
                estouCarregando = true;
            }

            if (ForGameJoltDatesManager.estaCarregado)
            {
#if !UNITY_EDITOR
                Sessions.Open(AbriuSessao);
                Sessions.Ping();
#endif
                SceneManager.LoadScene("menuInicial");
            }


        }
        else
        {
            buttonsPanel.SetActive(true);
            loadBar.SetActive(false);
        }
    }

    void AbriuSessao(bool foi)
    {
        if (foi)
            Debug.Log("Sessão aberta com sucesso");
        else
            Debug.Log("Sessão falhou");
    }

    void OnLoad(bool X)
    {
        estouCarregando = true;
        if (X)
            AgendeODiferenteDeZero();
        else
            Debug.Log("Puta que o pariu");
    }

    void AgendeODiferenteDeZero()
    {

        Debug.Log(GameJoltAPI.Instance.CurrentUser.ID);
        if (GameJoltAPI.Instance.CurrentUser.ID != 0)
        {

            //GameJoltTrophyDictionary.ReBloquearTrofeus();
            ForGameJoltDatesManager.estaCarregado = false;
            SaveDatesManager.Load();
        }
        else
            Invoke("AgendeODiferenteDeZero", 0.25f);
    }

    public void FazerLogin()
    {
        bool isSignedIn = GameJoltAPI.Instance.CurrentUser != null;

        if (!isSignedIn)
            GameJolt.UI.GameJoltUI.Instance.ShowSignIn(OnLoad);
    }

    public void ContinuarSemLogin()
    {
        SaveDatesManager.s = new SaveDatesManager();
        SceneManager.LoadScene("menuInicial");
    }
}

public class UrlVerify
{
    public static bool DomainsContainString(string s, UnityEngine.UI.Text t = null)
    {
        //return true;
        bool retorno = false;

        var uri = Application.absoluteURL;
        var parts = uri.Split('/');
        System.Array.Reverse(parts); // reverse the array to make the last element the first
        string r = "";

        for (var i = 0; i < parts.Length; i++)
        {
            var subParts = parts[i].Split('.');
            r += "Level " + i + " domain name:" + parts[i];

            for (var j = 0; j < subParts.Length; j++)
            {
                r += subParts[j] + " , ";

                if (subParts[j].ToLower().Trim() == s.ToLower().Trim())
                    retorno = true;
            }
        }

        /*r += " : " + retorno;

        if (t != null)
        {
            t.text = r;
        }*/


        return retorno;
    }
}
