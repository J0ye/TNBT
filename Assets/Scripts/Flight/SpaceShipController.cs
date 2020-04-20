using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipController : FreeLookCamera
{
    public UnityEvent OnFire;
    public UnityEvent OnCrash;

    public void Awake()
    {
        SetUpEvent(OnCrash);
        SetUpEvent(OnFire);
    }

    public new void Update()
    {
        base.Update();

        if(Input.GetKey(KeyCode.Mouse0))
        {
            OnFire.Invoke();
        }
    }

    protected void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Crashed");
            OnCrash.Invoke();
        }
    }

    public static void SetUpEvent(UnityEvent e)
    {
        if (e == null)
            e = new UnityEvent();
    }
}
