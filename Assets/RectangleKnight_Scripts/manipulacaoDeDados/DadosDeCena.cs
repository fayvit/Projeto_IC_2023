﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//[System.Serializable]
//public class DadosDeCena {

//    public NomesCenas nomeDaCena;
//    public LimitantesDaCena limitantes;
//    public Color bkColor;
//    public NameMusicaComVolumeConfig musicName = new NameMusicaComVolumeConfig()
//    {
//        Musica = NameMusic.initialAdventureTheme,
//        Volume = 1
//    };

//    [System.Serializable]
//    public class LimitantesDaCena : System.ICloneable,System.IComparable
//    {
//        public float xMin;
//        public float xMax;
//        public float yMin;
//        public float yMax;

//        public object Clone()
//        {
//            return new LimitantesDaCena()
//            {
//                xMax = xMax,
//                xMin = xMin,
//                yMin = yMin,
//                yMax = yMax
//            };
//        }

//        public int CompareTo(LimitantesDaCena obj)
//        {
//            return CompareTo((object)obj);
//        }

//        public int CompareTo(object obj)
//        {
//            LimitantesDaCena l = (LimitantesDaCena)obj;
//            int retorno = Mathf.RoundToInt(Mathf.Abs(l.xMax - xMax) + Mathf.Abs(l.xMin - xMin) + Mathf.Abs(l.yMax - yMax) + Mathf.Abs(l.yMin - yMin));
//            Debug.Log("comparação de limitantes: " + retorno + " : " + Mathf.Abs(l.xMax - xMax) + " : " + Mathf.Abs(l.xMin - xMin) + " : " + Mathf.Abs(l.yMax - yMax) + " : " + Mathf.Abs(l.yMin - yMin));
//            return retorno;
//        }
//    }
//}

//[System.Serializable]
//public class ContainerDeDadosDeCena
//{
//    public List<DadosDeCena> meusDados;

//    public DadosDeCena GetCurrentSceneDates()
//    {

//        string s = SceneManager.GetActiveScene().name;

//        Debug.Log(s);
//        return GetSceneDates(s);
//    }

//    public DadosDeCena GetSceneDates(string nome)
//    {
//        NomesCenas s = StringParaEnum.ObterEnum<NomesCenas>(nome,true);

//        //Debug.Log(s+" : "+default(NomesCenas));

//        if (s != default)
//        {
//            return GetSceneDates(s);
//        }

//        return null;
//    }

//    public DadosDeCena GetSceneDates(NomesCenas nome)
//    {
//        for (int i = 0; i < meusDados.Count; i++)
//            if (meusDados[i].nomeDaCena == nome)
//                return meusDados[i];

//        Debug.Log("Parece que não foi encontrada a cena no dicionario");

//        return null;
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DadosDeCena
{

    public NomesCenas nomeDaCena;
    public LimitantesDaCena limitantes;
    public Color bkColor;
    public NameMusicaComVolumeConfig musicName = new NameMusicaComVolumeConfig()
    {
        Musica = NameMusic.initialAdventureTheme,
        Volume = 1
    };

    [System.Serializable]
    public class LimitantesDaCena : System.ICloneable, System.IComparable
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;

        public object Clone()
        {
            return new LimitantesDaCena()
            {
                xMax = xMax,
                xMin = xMin,
                yMin = yMin,
                yMax = yMax
            };
        }

        public int CompareTo(LimitantesDaCena obj)
        {
            return CompareTo((object)obj);
        }

        public int CompareTo(object obj)
        {
            LimitantesDaCena l = (LimitantesDaCena)obj;
            int retorno = Mathf.RoundToInt(Mathf.Abs(l.xMax - xMax) + Mathf.Abs(l.xMin - xMin) + Mathf.Abs(l.yMax - yMax) + Mathf.Abs(l.yMin - yMin));
            Debug.Log("comparação de limitantes: " + retorno + " : " + Mathf.Abs(l.xMax - xMax) + " : " + Mathf.Abs(l.xMin - xMin) + " : " + Mathf.Abs(l.yMax - yMax) + " : " + Mathf.Abs(l.yMin - yMin));
            return retorno;
        }
    }
}

[System.Serializable]
public class ContainerDeDadosDeCena
{
    public List<DadosDeCena> meusDados = new List<DadosDeCena>();

    public DadosDeCena GetCurrentSceneDates()
    {

        string s = SceneManager.GetActiveScene().name;

        //Debug.Log(s);
        return GetSceneDates(s);
    }

    public DadosDeCena GetSceneDates(string nome)
    {
        NomesCenas s = StringParaEnum.ObterEnum<NomesCenas>(nome, true);

        //Debug.Log(s+" : "+default(NomesCenas));

        if (s != default(NomesCenas))
        {
            return GetSceneDates(s);
        }
        else {
            GameObject G1 = GameObject.FindGameObjectWithTag("startBlock");
            GameObject G2 = GameObject.FindGameObjectWithTag("endBlock");

            if (G1 != null && G2 != null)
            {
                Vector3 V1 = G1.transform.position;
                Vector3 V2 = G2.transform.position;

                DadosDeCena d = new DadosDeCena()
                {
                    bkColor = VerifiqueColorContainer(),
                    musicName = VerifiqueMusicContainer(),
                    limitantes = new DadosDeCena.LimitantesDaCena()
                    {
                        xMin = V1.x,
                        xMax = V2.x,
                        yMax = V2.y,
                        yMin = V1.y
                    },
                    nomeDaCena = s

                };
                return d;
            }
            else
            {
                Debug.LogWarning("startBlock e endBlock não inseridos");
            }
        }

        return null;
    }

    public DadosDeCena GetSceneDates(NomesCenas nome)
    {
        TentarSetarCena(nome);
        for (int i = 0; i < meusDados.Count; i++)
            if (meusDados[i].nomeDaCena == nome)
                return meusDados[i];

        Debug.Log("Parece que não foi encontrada a cena no dicionario");

        return null;
    }

    /*Essa função precisa de grandes melhorias*/
    void TentarSetarCena(NomesCenas s)
    {
        DadosDeCena dd = null;

        bool foi = false;

        foreach (DadosDeCena d in meusDados)
        {
            if (d.nomeDaCena == s)
            {
                foi = true;
                dd = d;
            }

        }

        if (foi)
        {
            if (dd.limitantes.xMax == dd.limitantes.xMin)
            {
                GameObject G = GameObject.FindGameObjectWithTag("startBlock");
                if (G != null)
                {
                    dd.limitantes.xMin = G.transform.position.x;
                    dd.limitantes.yMin = G.transform.position.y;
                }

                G = GameObject.FindGameObjectWithTag("endBlock");
                if (G != null)
                {
                    dd.limitantes.xMax = G.transform.position.x;
                    dd.limitantes.yMax = G.transform.position.y;
                }

                dd.bkColor = VerifiqueColorContainer();
                dd.musicName = VerifiqueMusicContainer();
            }
        }
        else
        {
            GameObject G1 = GameObject.FindGameObjectWithTag("startBlock");
            GameObject G2 = GameObject.FindGameObjectWithTag("endBlock");

            if (G1 != null && G2 != null)
            {
                Vector3 V1 = G1.transform.position;
                Vector3 V2 = G2.transform.position;

                meusDados.Add(new DadosDeCena()
                {
                    bkColor = VerifiqueColorContainer(),
                    musicName = VerifiqueMusicContainer(),
                    limitantes = new DadosDeCena.LimitantesDaCena()
                    {
                        xMin = V1.x,
                        xMax = V2.x,
                        yMax = V2.y,
                        yMin = V1.y
                    },
                    nomeDaCena = s
                });
            }
            else
            {
                Debug.LogWarning("startBlock e endBlock não inseridos");
            }
        }
    }

    Color VerifiqueColorContainer()
    {
        GameObject G1 = GameObject.FindGameObjectWithTag("colorContainer");
        Color retorno = new Color(.25f, .35f, .75f);
        if (G1 != null)
        {
            retorno = G1.GetComponent<SpriteRenderer>().color;
        }

        return retorno;
    }

    NameMusicaComVolumeConfig VerifiqueMusicContainer()
    {
        GameObject G1 = GameObject.FindGameObjectWithTag("musicName");
        NameMusicaComVolumeConfig m = new NameMusicaComVolumeConfig()
        {
            Musica = NameMusic.initialAdventureTheme,
            Volume = 1
        };

        if (G1 != null)
        {
            m = G1.GetComponent<DatesForCustomMusic>().m;
        }

        return m;
    }
}

