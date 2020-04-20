using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingGear : MonoBehaviour
{
    public GameObject target;
    
    protected Vector3 landingPoint;
    protected Quaternion landingOrientation = Quaternion.identity;
    protected bool active = false;
    public void LateUpdate()
    {
        if(active && landingPoint != null)
        {
            target.transform.position = landingPoint;
            if(landingOrientation == Quaternion.identity)
            {
                landingOrientation = target.transform.Find("Body").rotation;
            }else 
            {
                target.transform.Find("Body").rotation = landingOrientation;
            }
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        landingPoint = other.ClosestPoint(target.transform.position);
    }

    public void Engage()
    {
        if(!active)
        {
            float distance = Vector3.Distance(target.transform.position, landingPoint);
            Debug.Log("Distance: " + distance);
            if(distance < 1)
            {
                active = true;
            }
        }else 
        {
            active = false;
            target.transform.Find("Body").localRotation = Quaternion.Euler(-90, 0, 0);
            landingOrientation = Quaternion.identity;
        }
    }
}
