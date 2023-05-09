using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPost : MonoBehaviour
{
    public bool isActive;
    public GameObject Sign;
    public GameObject InactiveSign;

    // Start is called before the first frame update
    void Start()
    {
        Sign.SetActive(true);
        InactiveSign.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        InactiveSign.transform.Rotate(new Vector3(0, 0, 360) * Time.deltaTime);
    }

    public void Spin()
    {
        Sign.SetActive(false);
        InactiveSign.SetActive(true);    
    }
}
