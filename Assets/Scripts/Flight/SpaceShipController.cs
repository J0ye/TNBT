using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipController : FreeLookCamera
{
    public UnityEvent OnFire;
    public UnityEvent OnCrash;

    protected CharacterController controller;

    public void Awake()
    {
        SetUpEvent(OnCrash);
        SetUpEvent(OnFire);

        controller = GetComponent<CharacterController>();
    }

    public new void Update()
    {
        base.Update();

        if(Input.GetKey(KeyCode.Mouse0))
        {
            OnFire.Invoke();
        }
    }
#region Getter
    public float GetSpeed()
    {
        return speed;
    }

    public bool IsMoving()
    {
        if(CalculateFlyingVector() != Vector3.zero)
        {
            return true;
        }

        return false;
    }
#endregion

    protected void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Crashed");
            OnCrash.Invoke();
        }
    }

    protected override void Move(Vector3 direction)
    {
        controller.Move(direction);
    }

    public static void SetUpEvent(UnityEvent e)
    {
        if (e == null)
            e = new UnityEvent();
    }
}
