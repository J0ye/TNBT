using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipController : FreeLookCamera
{
    public float rotationSpeed = 2f;
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

    protected override Vector3 CalculateFlyingVector()
    {
        float horizontal = 0;
        float vertical = (Input.GetAxis("Vertical") * speed) * Time.deltaTime;
        vertical = Mathf.Clamp(vertical, 0f, speed * Time.deltaTime);
        Vector3 value = new Vector3(horizontal, 0, vertical);
        value = transform.TransformDirection(value);
        return value;
    }

    protected override void CalculateOrientation()
    {
        // Calculation of camera orientation
        mouseOrientation = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseOrientation = Vector2.Scale(mouseOrientation, new Vector2(sensitivity * smooth, sensitivity * smooth));
        smoothedVector = new Vector2(Mathf.Lerp(smoothedVector.x, mouseOrientation.x, 1.0f / smooth), 
                                        Mathf.Lerp(smoothedVector.y, mouseOrientation.y, 1.0f / smooth));
        mouseLook += smoothedVector;
        
        // Calculate rotation of spaceship
        float horizontal = (Input.GetAxis("Horizontal") * rotationSpeed) * Time.deltaTime;
        horizontal += transform.eulerAngles.z;
        Debug.Log("Z: " + horizontal);

        // Translation of camera orientation
        transform.rotation = Quaternion.Euler(-mouseLook.y, mouseLook.x, -horizontal);   
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
