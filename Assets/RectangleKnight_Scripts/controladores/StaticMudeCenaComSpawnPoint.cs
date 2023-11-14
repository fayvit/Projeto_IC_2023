using Spawns;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class StaticMudeCenaComSpawnPoint
{
    private static int contCenasCaregadas = 0;
    private static int numCenasParaCarregar = 0;
    private static NomesCenas cenaAtiva = NomesCenas.nula;
    private static SpawnID spawnId;

    public static bool EstaCenaEstaCarregada(NomesCenas nome)
    {
        return SceneManager.GetSceneByName(nome.ToString()).isLoaded;
    }

    public static void OnFadeOutComplete(NomesCenas[] cenasAlvo, NomesCenas estaCenaAtiva, SpawnID pos)
    {


        spawnId = pos;

        if (estaCenaAtiva != NomesCenas.nula)
            cenaAtiva = estaCenaAtiva;
        else
            cenaAtiva = cenasAlvo[0];

        Time.timeScale = 0;
        contCenasCaregadas = 0;

        NomesCenas[] N = SceneLoader.DescarregarCenasDesnecessarias(cenasAlvo);

        for (int i = 0; i < N.Length; i++)
        {
            SceneManager.UnloadSceneAsync(N[i].ToString());
        }

        N = SceneLoader.PegueAsCenasPorCarregar(cenasAlvo);

        numCenasParaCarregar = N.Length;

        for (int i = 0; i < N.Length; i++)
        {
            SceneManager.LoadScene(N[i].ToString(), LoadSceneMode.Additive);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private static void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        contCenasCaregadas++;

        if (contCenasCaregadas >= numCenasParaCarregar)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;

            SceneManager.activeSceneChanged += OnActiveSceneChanged;


            SceneManager.SetActiveScene(SceneManager.GetSceneByName(cenaAtiva.ToString()));


        }
    }

    private static void OnActiveSceneChanged(Scene arg0, Scene arg1)
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        Time.timeScale = 1;
        var v = MonoBehaviour.FindObjectsOfType<SpawnPointMark>();
        Vector3 posAlvo=Vector3.zero;
        foreach (var w in v)
            if (w.IdSpawn == spawnId)
                posAlvo = w.transform.position;

        GameController.g.Manager.transform.position = posAlvo;
        GameController.g.VerifiqueDinheiroCaido(GameController.g.Manager.Dados.DinheiroCaido);
        GlobalController.g.FadeV.IniciarFadeInComAction(OnFadeInComplete);
        MonoBehaviour.FindObjectOfType<Camera2D>().AposMudarDeCena(posAlvo + new Vector3(0, 0, -10));
        EventAgregator.Publish(EventKey.changeActiveScene, null);

        new MyInvokeMethod().InvokeAoFimDoQuadro(() =>
        {
            EventAgregator.Publish(EventKey.localNameExibition);
        });
    }

    static void OnFadeInComplete()
    {

    }
}
