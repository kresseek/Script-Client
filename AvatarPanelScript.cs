using UnityEngine;
using UnityEngine.EventSystems;

public class AvatarPanelScript : MonoBehaviour
{

    //refrence for the pause menu panel in the hierarchy
    public GameObject Object;
    //animator reference
    private Animator anim;
    //variable for checking if the game is paused 
    
    // Use this for initialization
    void Start()
    {
        //unpause the game on start

        //get the animator component

        anim = Object.GetComponent<Animator>();        //disable it on start to stop it from playing the default animation
        Object.SetActive(false);
    }
    private void Update()
    {
        UnpauseGame(Object);
    }



    //function to pause the game
    public void PauseGame()
    {
        //enable the animator component
        anim.enabled = true;
        Object.SetActive(true);
        //play the Slidein animation
        anim.Play("AvatarSlideIn");
        //set the isPaused flag to true to indicate that the game is paused
    
        //freeze the timescale
        
    }
    //function to unpause the game
    public void UnpauseGame(GameObject panel)
    {
        //set the isPaused flag to false to indicate that the game is not paused
        if (Input.GetMouseButton(0))
        {
            RectTransform rectTransform = panel.GetComponent<RectTransform>();
            Canvas canvas = GetComponent<Canvas>();
            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
                anim.Play("AvatarSlideOut");
            //panel.SetActive(false);
        }
        //play the SlideOut animation
        
        //set back the time scale to normal time scale
        
    }

}