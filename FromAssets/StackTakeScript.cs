using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTakeScript : MonoBehaviour {
    public void TakeFromStack()
    {
          GameObject cardObj = Instantiate(Resources.Load("CardHolder")) as GameObject;
            cardObj.transform.SetParent(GameObject.Find("Hand").transform);
            
            cardObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

    }



