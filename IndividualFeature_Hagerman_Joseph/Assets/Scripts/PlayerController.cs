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
    public bool travelling;

    public float TimeForTravel;
    public float TravelProgress;

    public float DeltaTime;

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
    }

    public IEnumerator TravelTimer()
    {
        travelling = true;
        while (travelling == true)
        {
            yield return new WaitForSeconds(3f);
            DeltaTime += Time.deltaTime;
            TravelProgress = DeltaTime;

            if (TravelProgress >= TimeForTravel)
            {
                GameManager.Manager.GetComponent<GameManager>().Teleport();                
                DeltaTime = 0;
                TravelProgress = 0;
                travelling = false;
            }
        }
    }

    private void Update()
    {
        if (player.GetComponent<Rigidbody>().IsSleeping())
        {
            StopCoroutine(TravelTimer());
            DeltaTime = 0;
            TravelProgress = 0;
            if (TravelProgress >= 1.5)
            {             
                GameManager.Manager.GetComponent<GameManager>().PastActive = false;
                GameManager.Manager.GetComponent<GameManager>().FutureActive = false;
            }
        }
        else
        {
            if (GameManager.Manager.GetComponent<GameManager>().PastActive || GameManager.Manager.GetComponent<GameManager>().FutureActive)
            {
                StartCoroutine(TravelTimer());
            }
        }
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
            if (other.transform.parent.gameObject.GetComponent<PastPost>().isActive)
            {
                other.transform.parent.gameObject.GetComponent<PastPost>().Spin();
                GameManager.Manager.GetComponent<GameManager>().PastActive = true;
                GameManager.Manager.GetComponent<GameManager>().FutureActive = false;
            } 
        }
        if (other.transform.parent.CompareTag("FutureSign"))
        {
            if (other.transform.parent.gameObject.GetComponent<FuturePost>().isActive)
            {
                other.transform.parent.gameObject.GetComponent<FuturePost>().Spin();
                GameManager.Manager.GetComponent<GameManager>().FutureActive = true;
                GameManager.Manager.GetComponent<GameManager>().PastActive = false;
            }
        }
    }
}
