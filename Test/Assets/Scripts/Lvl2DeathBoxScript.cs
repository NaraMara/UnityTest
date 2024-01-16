using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2DeathBoxScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject Death;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.GetComponent<PlayerController>().Respawn();
            Death.GetComponent<DeathScript>().Respawn();
        }
    }
}
