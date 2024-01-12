using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour
{
    private bool _isInRange = false;


    // Update is called once per frame
    void Update()
    {
        if (_isInRange)
        {
            Debug.Log("He's here");
            if (Input.GetKeyDown(KeyCode.C))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isInRange = false;
        }
    }
}
