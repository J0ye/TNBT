using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public AudioSource sound;
    public SpaceShipController controller;
    [Range(0,10f)]
    public float pitchFactor = 1f;

    protected float startPitch;
    // Start is called before the first frame update
    void Start()
    {
        startPitch = sound.pitch;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.IsMoving() && sound.pitch == startPitch)
        {
            sound.pitch += pitchFactor;
        }else if(controller.IsMoving())
        {
            sound.pitch = startPitch;
        }
    }
}
