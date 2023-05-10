using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPost : MonoBehaviour
{
    public bool isActive;
    public GameObject Sign;
    public GameObject InactiveSign;

    public float rotation = 720;

    // Start is called before the first frame update
    void Start()
    {
        Sign.SetActive(true);
        InactiveSign.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (InactiveSign.activeSelf == true)
        {
            InactiveSign.transform.Rotate(new Vector3(0, rotation, 0) * Time.deltaTime);
            StartCoroutine(DeactivationTimer());
        }
    }

    public IEnumerator DeactivationTimer()
    {
        yield return new WaitForSeconds(2.0f);
        rotation = 0;
    }

    public void Spin()
    {
        Sign.SetActive(false);
        InactiveSign.SetActive(true);
    }
}
