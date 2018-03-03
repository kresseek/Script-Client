using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUndangScript : MonoBehaviour
{
    public GameObject Object;

  /* void Update()
    {
        HideIfClickedOutside();
    }
    */
    public void setuju()
    {
        Object.gameObject.SetActive(true);
    }
    public void enggak()
    {
        Object.gameObject.SetActive(false);
    }

    

}