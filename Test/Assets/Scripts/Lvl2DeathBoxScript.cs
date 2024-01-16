using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl2DeathBoxScript : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           FindAnyObjectByType<PlayerController>().Respawn();
           var death= FindAnyObjectByType<DeathScript>();
            if (death is not null)
            {
                death.Respawn();
            }
        }
    }
}
