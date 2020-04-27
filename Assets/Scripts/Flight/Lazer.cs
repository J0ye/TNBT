using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum LazerStage {Cold, Ready, Fire}
public class Lazer : Weapon
{
    public GameObject firstStagePrefab;
    public float chargeUpTime = 1f;
    public UnityEvent OnReady;

    protected GameObject lazer;
    protected LazerStage stage = LazerStage.Cold;

    protected new void Start()
    {
        base.Start();
        lazer = new GameObject();
        lazer.transform.parent = transform;

        SpaceShipController.SetUpEvent(OnReady);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            stage = LazerStage.Cold;
            Destroy(lazer);
            lazer = new GameObject();
            lazer.transform.parent = transform;
        }
    }

    public override void Fire()
    {
        if(!paused)
        {  
            if(stage == LazerStage.Cold)
            {
                Destroy(lazer);
                lazer = Instantiate(firstStagePrefab);
                lazer.transform.position = barrel.position;
                lazer.transform.parent = transform;
                lazer.transform.localRotation = firstStagePrefab.transform.localRotation;
                OnReady.Invoke();
                StartCoroutine(Charge());
            }
        }
    }

    protected void FireSecondStage()
    {
        if(stage == LazerStage.Ready)
        {
            Destroy(lazer);                   
            lazer = Instantiate(projectile);
            lazer.transform.position = barrel.position;
            lazer.transform.parent = transform;
            lazer.transform.localRotation = projectile.transform.localRotation;
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
                    Physics.IgnoreLayerCollision(lazer.layer, i);
                }
            }
            OnFire.Invoke();
        }
    }

    protected IEnumerator Charge()
    {
        yield return new WaitForSeconds(chargeUpTime);
        stage = LazerStage.Ready;
        if(Input.GetKey(KeyCode.Mouse0) && stage == LazerStage.Ready)
        {
            FireSecondStage();
        }
    }
}
