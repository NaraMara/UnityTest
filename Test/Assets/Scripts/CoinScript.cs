using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public string ClipName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            FindObjectOfType<AudioManager>().Play(ClipName);
            FindObjectOfType<HUD>().AddCoin();
            Destroy(gameObject);
        }
        
    }
}
