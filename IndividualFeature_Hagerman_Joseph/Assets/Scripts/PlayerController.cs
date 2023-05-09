using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpStrength;
    public Rigidbody rb;
    public Vector3 Movement;
    public bool isGrounded;

    public float TimeForTravel;
    public float TravelProgress;

    static public GameObject player;

    public float Magnitude;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (player != null && player != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            player = this.gameObject;
        }

        var vel = rb.velocity;
        Magnitude = vel.magnitude;
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

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            TravelProgress += 0.1f;

            
        }
    }

    private void Update()
    {
        //if there's a clone of the player, destroy it, otherwise, keep it  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Generator") && isGrounded == false)
        {
            Debug.Log("Tried to make a good future.");
            Debug.Log(GameManager.Manager.GetComponent<GameManager>().TimeZone);
            if (GameManager.Manager.GetComponent<GameManager>().TimeZone == 2)
            {
                GameManager.Manager.GetComponent<GameManager>().generatorDestroyed = true;
                Debug.Log("You have made a good future!");
            }
            else
            {
                return;
            }
        }
        if (other.transform.parent.CompareTag("PastSign"))
        {

        }
    }
}
