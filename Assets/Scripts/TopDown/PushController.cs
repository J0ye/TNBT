using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushController : MonoBehaviour
{
    [Range(0, 1.0f)]
    public float speed = 0.5f;
    private Rigidbody rig;

    private float vertical;
    private float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Rigidbody>() != null)
        {
            rig = GetComponent<Rigidbody>();
        }else {
            Debug.LogError("There is no rigidbody here: gameObject.name");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if(Mathf.Abs(vertical) > 0 || Mathf.Abs(horizontal) > 0)
        {
            rig.AddForce(vertical * speed, 0, horizontal*speed, ForceMode.Impulse);
        }
    }
}
