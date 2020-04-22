using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform barrel;
    [Range(0.1f, 1000f)]
    public float power = 100f;
    public float bulletLifetime = 1f;
    public float timeBetweenShots = 0.1f;
    public bool spray = false;
    public Vector2 sprayBounds = Vector2.zero;
    public LayerMask ignoreCollisionMask;

    public UnityEvent OnFire;

    protected bool paused = false;

    protected void Start()
    {
        SpaceShipController.SetUpEvent(OnFire);
    }

    public virtual void Fire()
    {
        if(!paused)
        {            
            GameObject bullet = Instantiate(projectile, 
                barrel.position, 
                projectile.transform.rotation, null);
            Vector3 aim = transform.forward;
            if(spray)
            {
                float randX = UnityEngine.Random.Range(sprayBounds.x/10, sprayBounds.y/10);
                float randY = UnityEngine.Random.Range(sprayBounds.x/10, sprayBounds.y/10);
                float randZ = UnityEngine.Random.Range(sprayBounds.x/10, sprayBounds.y/10);
                aim = new Vector3(aim.x + randX, aim.y + randY, aim.z + randZ);
            }
            
            // Enable collision ignorance
            uint bitString =(uint)ignoreCollisionMask.value;
            for (int i = 31; bitString > 0; i--)
            {
                if ((bitString >> i) > 0)
                {
                    bitString = ((bitString << 32 - i) >> 32 - i);
                    Physics.IgnoreLayerCollision(bullet.layer, i);
                }
            }
            bullet.GetComponent<Rigidbody>().AddForce(aim  * (power * 10));
            Destroy(bullet, bulletLifetime);
            OnFire.Invoke();
            StartCoroutine(Pause());
        }
    }

    protected IEnumerator Pause()
    {
        paused = true;
        yield return new WaitForSeconds(timeBetweenShots);
        paused = false;
    }
}
