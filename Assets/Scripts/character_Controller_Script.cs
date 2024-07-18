using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Character_move : MonoBehaviour
{
    CharacterController cha;
    public float move_speed = 5f;
    private Transform cameraTransform;
    Vector3 m_EulerAngleVelocity;

    void Start()
    {
        cha=GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        m_EulerAngleVelocity = Vector3.zero;

        if (Input.GetKey("e"))
        {
            m_EulerAngleVelocity = new Vector3(0, 2, 0);
        }
        if (Input.GetKey("q"))
        {
            m_EulerAngleVelocity = new Vector3(0, -2, 0);
        }

        

        
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement = (cameraForward * verticalInput + cameraRight * horizontalInput) * move_speed;


         cha.SimpleMove(movement);
         this.transform.Rotate(m_EulerAngleVelocity);  


    }
}