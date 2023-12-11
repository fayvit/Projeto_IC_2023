using System.Collections;
using UnityEngine;

namespace Assets.RectangleKnight_Scripts.controladores
{
    [System.Serializable]
    public class ParryManager
    {
        [SerializeField] private SpriteRenderer escudo;
        [SerializeField] private Transform posInicioMove;
        [SerializeField] private Transform posFinalMove;
        [SerializeField] private float tempoAntesDoParry = .2f;
        [SerializeField] private float tempoNoParry = .2f;
        [SerializeField] private float tempoRetornandoDaDefesa = .2f;

        
        private Vector3 posInicialEscudo;
        private Vector3 startPosition;
        private float tempoDecorrido = 0;
        private CaminhoDeDefesa caminhoDeDefesa=CaminhoDeDefesa.indoAoParry;

        private enum CaminhoDeDefesa
        { 
        indoAoParry,
        indoParaDefesa,
        indoParaDescanso
        }

        public bool ParryFrame { get; private set; }
        public bool InDefense { get; private set; }

        // Use this for initialization
        public void Start()
        {
            posInicialEscudo = escudo.transform.localPosition;
        }

        // Update is called once per frame
        public bool Update(bool command)
        {
            if (command)
            {
                if (caminhoDeDefesa == CaminhoDeDefesa.indoParaDescanso)
                {
                    tempoDecorrido = 0;
                    startPosition = escudo.transform.position;
                    caminhoDeDefesa = CaminhoDeDefesa.indoAoParry;
                }
                else
                if (caminhoDeDefesa == CaminhoDeDefesa.indoAoParry)
                {
                    tempoDecorrido += Time.deltaTime;
                    escudo.transform.position = Vector3.Lerp(startPosition, posInicioMove.position, tempoDecorrido / tempoAntesDoParry);
                    if (tempoDecorrido > tempoAntesDoParry)
                    {
                        tempoDecorrido = 0;
                        ParryFrame = true;
                        escudo.color = Color.red;
                        InDefense = true;
                        caminhoDeDefesa = CaminhoDeDefesa.indoParaDefesa;
                    }
                }
                else if (caminhoDeDefesa == CaminhoDeDefesa.indoParaDefesa)
                {
                    tempoDecorrido += Time.deltaTime;
                    escudo.transform.position = Vector3.Lerp(posInicioMove.position, posFinalMove.position, tempoDecorrido / tempoNoParry);
                    escudo.transform.rotation = Quaternion.Lerp(posInicioMove.rotation, posFinalMove.rotation, tempoDecorrido / tempoNoParry);
                    if (tempoDecorrido > tempoNoParry)
                    {
                        escudo.color = Color.white;
                        ParryFrame = false;
                    }
                }
            }
            else if (caminhoDeDefesa != CaminhoDeDefesa.indoParaDescanso)
            {
                tempoDecorrido = 0;
                InDefense = false;
                ParryFrame = false;
                escudo.color = Color.white;
                caminhoDeDefesa = CaminhoDeDefesa.indoParaDescanso;
                startPosition = escudo.transform.localPosition;
            }
            else if (caminhoDeDefesa == CaminhoDeDefesa.indoParaDescanso)
            {
                tempoDecorrido += Time.deltaTime;
                escudo.transform.localPosition = Vector3.Lerp(startPosition, posInicialEscudo, tempoDecorrido / tempoRetornandoDaDefesa);
                escudo.transform.rotation = Quaternion.Lerp(escudo.transform.rotation, Quaternion.identity, tempoDecorrido / tempoRetornandoDaDefesa);
                if (tempoDecorrido > tempoRetornandoDaDefesa)
                {
                    return true;
                }
            }
            return false;
        }
    }
}