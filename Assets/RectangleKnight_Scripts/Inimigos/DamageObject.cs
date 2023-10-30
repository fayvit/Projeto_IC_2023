using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private int valorDeDano = 25;
    [SerializeField] private bool defensavel = true;
    [SerializeField] private bool aparavel = false;
    [SerializeField] private Transform refDefPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log(collision.name + " : " + collision.tag);
        if (collision.tag == "Player")
        {
            if (UnicidadeDoPlayer.Verifique(collision))
            {
                bool sentidoPositivo = transform.position.x - collision.transform.position.x > 0;
                bool refPOsitionPositiva =refDefPosition? refDefPosition.position.x - collision.transform.position.x > 0:sentidoPositivo;
                EventAgregator.Publish(new StandardSendGameEvent(gameObject, EventKey.heroDamage, sentidoPositivo, valorDeDano,aparavel,defensavel,refPOsitionPositiva));
            }
        }
    }
}
