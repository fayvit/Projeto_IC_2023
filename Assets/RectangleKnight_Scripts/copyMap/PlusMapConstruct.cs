using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

[System.Serializable]
public class PlusMapConstruct
{
    [System.NonSerialized]private Tilemap[] thisTiles;

    private Dictionary<string, Dictionary<string, MyColor>> dicionarioDeCores 
        = new Dictionary<string, Dictionary<string, MyColor>>();

    private Dictionary<string, Dictionary<MyVector2Int, MyColor>> dicionarioParaImagens 
        = new Dictionary<string, Dictionary<MyVector2Int, MyColor>>();

    public void ProcessarImagensDosMapas()
    {
        thisTiles = GameObject.FindObjectsOfType<Tilemap>();

        Debug.Log(thisTiles.Length+" tileLength");

        for (int i = 0; i < thisTiles.Length; i++)
        {
            string s = thisTiles[i].gameObject.name;
            PreencherDicionarioDeCores(thisTiles[i]);
            PreencherDicionarioParaImagem(thisTiles[i]);
            SaveMap.Salvar(CriarImagemDoMapa(dicionarioParaImagens[s]), thisTiles[i].transform.parent.name + "_" + s);
            
        }


        string json = JsonMapContainer.GetJsonObject(dicionarioDeCores, dicionarioParaImagens);

        SaveMap.SalvarJsonInTexto(json, Application.dataPath + "/" + thisTiles[0].transform.parent.name + ".txt");
        /*
        ContainerPlusMap cpm = new ContainerPlusMap(dicionarioDeCores, dicionarioParaImagens);

        SaveMap.SalvarPlusMap(cpm, thisTiles[0].transform.parent.name);*/

    }    

    public void CreateAndSaveMapImage()
    {
        foreach (string s in dicionarioDeCores.Keys)
        {
            SaveMap.Salvar(CriarImagemDoMapa(dicionarioParaImagens[s]),  s);
        }
    }

    Texture2D CriarImagemDoMapa(Dictionary<MyVector2Int,MyColor> D)
    {
        return MapConstruct.TexturaDeMapaAtual(D);
    }

    void PreencherDicionarioParaImagem(Tilemap tile)
    {
        string tileName = tile.gameObject.name;
        Dictionary<MyVector2Int, MyColor> D = new Dictionary<MyVector2Int, MyColor>();
        dicionarioParaImagens[tileName] = D;
        

        PercorraTileMap(tile, (int c, int r) =>{

            Vector3 V = tile.CellToWorld(new Vector3Int(r, c, 0));
            TileBase tb = tile.GetTile(new Vector3Int(r, c, 0));
            if (tb != null)
            {
                D[new MyVector2Int((int)V.x, (int)V.y)] = GetColorWithTileName(tileName,tb.name);
            }
        });
    }

    MyColor GetColorWithTileName(string tileName,string name)
    {
        foreach (string s in dicionarioDeCores[tileName].Keys)
        {
            if (s == name)
                return dicionarioDeCores[tileName][s];
        }

        Debug.LogWarning("Cor não encontrada no dicionario de cores");
        return new MyColor(Color.white);
    }

    void PreencherDicionarioDeCores(Tilemap tile)
    {
        dicionarioDeCores[tile.gameObject.name] = new Dictionary<string, MyColor>();

        PercorraTileMap(tile, (int c,int r) => {

            if (tile.GetTile(new Vector3Int(r, c, 0)) != null)
            {
                string key = tile.GetTile(new Vector3Int(r, c, 0)).name;
                AdicionarCorSeNaoContem(key, dicionarioDeCores[tile.gameObject.name]);
            }
        });
    }

    void AdicionarCorSeNaoContem(string key, Dictionary<string, MyColor> D)
    {
        if (!dicionarioDeCores.ContainsKey(key))
        {
            bool adicionouCor = false;
            do
            {
                MyColor C = new MyColor(Random.ColorHSV());
                
                adicionouCor = true;
                foreach (string s in D.Keys)
                {
                    if (D[s].cor == C.cor  || C.cor == Color.white)
                        adicionouCor = false;
                }

                if (adicionouCor)
                    D[key] = C;

            } while (!adicionouCor);
        }
    }

    void PercorraTileMap(Tilemap tilemap, System.Action<int,int> A)
    {
        BoundsInt area = tilemap.cellBounds;

        for (int col = area.yMax; col >= area.yMin; col--)
        {
            for (int row = area.xMin; row <= area.xMax; row++)
            {
                A(col,row);
            }
        }
    }
}