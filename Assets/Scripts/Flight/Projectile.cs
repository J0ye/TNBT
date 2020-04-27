using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.GetComponent<Entity>() != null)
        {
            other.gameObject.GetComponent<Entity>().Damage(damage);
            //Debug.Log("Damaged " + other.gameObject.name + " for " + damage);
            Destroy(gameObject);  
        }
    }
}
