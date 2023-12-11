using Spawns;
using UnityEngine;

namespace Assets.RectangleKnight_Scripts.controladores
{
    public class MudeCenaComSpawnPoint : MonoBehaviour
    {
        [SerializeField] private NomesCenas[] cenasAlvo = default;
        [SerializeField] private SpawnID spawnId;
        [SerializeField] private RestritorDeCamLimits restritor = default;

        void OnFadeOutComplete()
        {
            StaticMudeCenaComSpawnPoint.OnFadeOutComplete(cenasAlvo, cenasAlvo[0], spawnId);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (UnicidadeDoPlayer.Verifique(collision))
                {
                    GlobalController.g.FadeV.IniciarFadeOutComAction(OnFadeOutComplete, 0.5f);

                    restritor.VerifiqueLimitantesParaMudeCena(cenasAlvo[0]);
                }

            }
        }
    }
}
