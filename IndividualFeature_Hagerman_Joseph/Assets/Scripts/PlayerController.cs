using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpStrength;
    public Rigidbody rb;
    public Vector3 Movement;
    public bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
    private void FixedUpdate()
    {
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(straffe, 0, translation);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            if (hit.distance <= 1.1)
            {
                isGrounded = true;

            }
            else
            {
                isGrounded = false;
            }
        }



        if (Input.GetKey("space") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
        }
    }
}
