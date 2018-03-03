using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPreviewScript : MonoBehaviour {
    public GameObject Object;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ClickedOutside(Object);
    }

    public void ClickedOutside(GameObject panel)
    {
        //set the isPaused flag to false to indicate that the game is not paused
        if (Input.GetMouseButton(0))
        {
            RectTransform rectTransform = panel.GetComponent<RectTransform>();
            Canvas canvas = GetComponent<Canvas>();
            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))

            panel.SetActive(false);
        }
        //play the SlideOut animation

        //set back the time scale to normal time scale

    }
}
