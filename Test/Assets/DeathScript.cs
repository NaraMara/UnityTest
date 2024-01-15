using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public GameObject Target;
    public Transform RespawnPoint;

    [SerializeField] private float _deathSpeed = 1.0f;

    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        if (Target is null)
        {
            Debug.LogWarning("Can't find player ");
        }
        RespawnPoint = GameObject.Find("DeathRespawnPoint").GetComponent<Transform>();
        if (RespawnPoint is null)
        {
            Debug.LogWarning("Can't set respawn point");
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _deathSpeed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Too bad. You've died");
            FindFirstObjectByType<PlayerController>().Respawn();
            Respawn();
        }
    }

    public void Respawn()
    {
        gameObject.transform.position = RespawnPoint.position;
    }


}
