using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeapon : Weapon
{
    public void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            Fire();
        }
    }
}
