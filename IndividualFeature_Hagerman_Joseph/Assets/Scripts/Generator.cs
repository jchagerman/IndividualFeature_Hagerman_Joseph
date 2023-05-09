using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject Destroyed;
    public GameObject Active;
    /// <summary>
    /// true = BadFuture, false = GoodFuture
    /// </summary>
    public bool FutureType;


    // Start is called before the first frame update
    void Awake()
    {
        Destroyed = GameObject.FindWithTag("Demolished");
        Active = GameObject.FindWithTag("Generator");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Manager.GetComponent<GameManager>().generatorDestroyed == false)
        {
            Active.SetActive(true);
            Destroyed.SetActive(false);
        }
        else
        {
            Active.SetActive(false);
            Destroyed.SetActive(true);
        }
    }
}
