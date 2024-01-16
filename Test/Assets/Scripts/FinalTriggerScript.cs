using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTriggerScript : MonoBehaviour
{
    public GameObject Death;
    private bool _isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isTriggered)
        {
            Debug.Log("You've done it. Nice.");
            Death.GetComponent<DeathScript>().Kill();
            //some cool animation

        }
    }
}
