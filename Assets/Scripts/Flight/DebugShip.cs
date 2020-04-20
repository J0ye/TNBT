using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugShip : SpaceShipController
{
    public LineRenderer line;
    [Range(1, 100f)]
    public float lineLength;

    private Vector3 lastFrame;
    public new void Update() 
    {
        base.Update();
        if(line.positionCount < 2)
        {
            line.positionCount = 2;
        }

        line.SetPosition(0, transform.position);
        Vector3 pos = (transform.position - CalculateFlyingVector()) * lineLength;
        line.SetPosition(1, pos);

        lastFrame = transform.position;
    }
}
