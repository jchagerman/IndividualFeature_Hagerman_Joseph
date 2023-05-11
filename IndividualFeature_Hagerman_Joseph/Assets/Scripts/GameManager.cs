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
    public bool Flicker;

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
        DontDestroyOnLoad(UICanvas.UI);

        PastActive = false;
        FutureActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Load Present
        if (Input.GetKey("1"))
        {
            SceneManager.LoadScene(0);
            TimeZone = 1;
        }
        //Load Past
        if (Input.GetKey("2"))
        {
            SceneManager.LoadScene(1);
            TimeZone = 2;
        }
        //Load Future
        if (Input.GetKey("3"))
        {
            if (generatorDestroyed)
            {
                SceneManager.LoadScene(2);
                TimeZone = 3;
            }
            else
            {
                SceneManager.LoadScene(3);
                TimeZone = 4;
            }
        }


        if (Input.GetKey("p"))
        {
            PastActive = true;
            FutureActive = false;
        }
        if (Input.GetKey("f"))
        {
            PastActive = false;
            FutureActive = true;
        }

        if (PastActive && TimeZone == 2)
        {
            PastActive = false;
            FutureActive = false;
        }
        if (FutureActive && TimeZone == 3)
        {
            PastActive = false;
            FutureActive = false;
        }
        if (FutureActive && TimeZone == 4)
        {
            PastActive = false;
            FutureActive = false;
        }
    }

    public void Teleport()
    {
        if (PastActive)
        {
            if (TimeZone == 3 || TimeZone == 4)
            {
                SceneManager.LoadScene(0);
                TimeZone = 1;
            }
            else if (TimeZone == 1)
            {
                SceneManager.LoadScene(1);
                TimeZone = 2;
            }
        }
        else if (FutureActive)
        {
            if (TimeZone == 2)
            {
                SceneManager.LoadScene(0);
                TimeZone = 1;
            }
            else if (TimeZone == 1)
            {
                if (generatorDestroyed)
                {
                    SceneManager.LoadScene(2);
                    TimeZone = 3;
                }
                else
                {
                    SceneManager.LoadScene(3);
                    TimeZone = 4;
                }
            }
        }
        StartCoroutine(DisguiseSwap());

        PlayerController.player.GetComponent<PlayerController>().travelling = false;
        PastActive = false;
        FutureActive = false;
    }

    public IEnumerator DisguiseSwap()
    {
        UICanvas.UI.GetComponent<UICanvas>().WhiteScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        UICanvas.UI.GetComponent<UICanvas>().WhiteScreen.SetActive(false);
        UICanvas.UI.GetComponent<UICanvas>().TravelTransition.SetActive(true);
        yield return new WaitForSeconds(3f);
        UICanvas.UI.GetComponent<UICanvas>().WhiteScreen.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        UICanvas.UI.GetComponent<UICanvas>().TravelTransition.SetActive(false);
        yield return new WaitForSeconds(1f);
        UICanvas.UI.GetComponent<UICanvas>().WhiteScreen.SetActive(false);
    }
}
