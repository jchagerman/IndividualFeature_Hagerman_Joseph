using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour
{
    static public GameObject UI;
    public GameObject PastUI;
    public GameObject FutureUI;

    public bool flicker;

    // Start is called before the first frame update
    void Start()
    {
        if (UI != null && UI != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            UI = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Manager.GetComponent<GameManager>().PastActive)
        {
            PastUI.SetActive(true);
            FutureUI.SetActive(false);
        }
        else if (GameManager.Manager.GetComponent<GameManager>().FutureActive)
        {
            PastUI.SetActive(false);
            FutureUI.SetActive(true);
        }
        else
        {
            PastUI.SetActive(false);
            FutureUI.SetActive(false);
        }

    }
}
