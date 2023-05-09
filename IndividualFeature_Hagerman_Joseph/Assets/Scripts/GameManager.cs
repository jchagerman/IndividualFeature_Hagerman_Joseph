using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 1 = Present, 2 = past, 3 = GoodFuture, 4 = BadFuture
    /// </summary>
    public int TimeZone;
    public bool generatorDestroyed = false;
    public bool PastActive;
    public bool FutureActive;

    static public GameObject Manager;

    // Start is called before the first frame update
    void Start()
    {
        TimeZone = 1;

        if (Manager != null && Manager != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Manager = this.gameObject;
        }

        DontDestroyOnLoad(PlayerController.player);
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1"))
        {
            SceneManager.LoadScene(0);
            TimeZone = 1;
            Debug.Log(TimeZone);
        }
        if (Input.GetKey("2"))
        {
            SceneManager.LoadScene(1);
            TimeZone = 2;
            Debug.Log(TimeZone);
        }
        if (Input.GetKey("3"))
        {
            if (generatorDestroyed)
            {
                SceneManager.LoadScene(2);
                TimeZone = 3;
                Debug.Log(TimeZone);
            }
            else
            {
                SceneManager.LoadScene(3);
                TimeZone = 4;
                Debug.Log(TimeZone);
            }
        }

    }

    public void Teleport()
    {

    }
}
