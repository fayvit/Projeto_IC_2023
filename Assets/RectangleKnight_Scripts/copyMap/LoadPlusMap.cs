using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlusMap : MonoBehaviour
{
    [SerializeField] private BuscadorDeEnderecoDeArquivo bPath;
    [SerializeField] private bool vai = false;
    [SerializeField] private bool coresDoArquivo = false;

    

    // Start is called before the first frame update
    void Carregue()
    {
        ContainerPlusMap p = new ContainerPlusMap(
            JsonUtility.FromJson<JsonMapContainer>(
            SaveMap.CarregarArquivoParaJson(bPath.GetPath)));
         
        p.CreateScenario(bPath.GetFileName);    
        //p.testeGetTexture(bPath.GetFileName, mytex);
        //p.MostrarCoresDoDicionario();

        
    }

    void ListarCoresDoArquivo()
    {
        ContainerPlusMap p = SaveMap.CarregarPlusMap(bPath.GetPath);
        p.MostrarCoresDoDicionario();
    }

    private void OnValidate()
    {
        bPath.OnValidate();
    }

    

    // Update is called once per frame
    void Update()
    {
        if (vai)
        {
            vai = false;
            Carregue();
        }

        if (coresDoArquivo)
        {
            coresDoArquivo = false;
            ListarCoresDoArquivo();
        }
    }
}
