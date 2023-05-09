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

    public float TimeForTravel;
    public float TravelProgress;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

        //if you reach near max speed, start the TravelProgress Timer
        //if you reach a stop, and the progress has not exceeded 1.5 seconds, reset the timer to zero
        //otherwise, negate the time travel request
    }
}
