using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPost : MonoBehaviour
{
    public bool isActive;
    public GameObject Sign;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spin()
    {
        Sign.transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
    }
}
