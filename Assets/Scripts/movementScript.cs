using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Transform cameraTransform;
    Vector3 m_EulerAngleVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
        m_EulerAngleVelocity = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        m_EulerAngleVelocity = Vector3.zero;

        
        if (Input.GetKey("e"))
        {
            m_EulerAngleVelocity = new Vector3(0, 35, 0);
        }
        if (Input.GetKey("q"))
        {
            m_EulerAngleVelocity = new Vector3(0, -35, 0);
        }

        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);

        
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        
        Vector3 movement = (cameraForward * verticalInput + cameraRight * horizontalInput) * moveSpeed;

        
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}

