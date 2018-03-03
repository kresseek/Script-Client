using UnityEngine;
using UnityEngine.EventSystems;

public class SettingPanelScript : MonoBehaviour
{

    //refrence for the pause menu panel in the hierarchy
    public GameObject PanelSettingUI;
    //animator reference
    private Animator animSet;
    //variable for checking if the game is paused 

    // Use this for initialization
    void Start()
    {
        
        animSet = PanelSettingUI.GetComponent<Animator>();
        PanelSettingUI.SetActive(false);
    }
    private void Update()
    {
        UnpauseGame(PanelSettingUI);
    }








    //function to pause the game
    public void PauseGame()
    {
        //enable the animator component
        animSet.enabled = true;
        PanelSettingUI.SetActive(true);
        //play the Slidein animation
        animSet.Play("SetSlideIn");
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
            
            if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, Input.mousePosition))
            animSet.Play("SetSlideOut");
            //panel.SetActive(false);
        }
        //play the SlideOut animation
        
        //set back the time scale to normal time scale

    }
    private void CloseIfClickedOutside(GameObject panel)
    {
        
    }

}