using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    [SerializeField] private int health = 1;
    public virtual void Kill()
    {
        Destroy(gameObject);
    }
    public virtual void TakeDagage(int damage)
    {
        if (health - damage < 0)
        {
            //flashy animation and sound
            Kill();
        }
        else
        {
            health -= damage;
            Debug.Log(gameObject.name + "was hit for:" + damage);

        }
    }
}
