using UnityEngine;
using UnityEngine.EventSystems;

public class InvitePanelScript : MonoBehaviour
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
        Debug.Log("what");
        PanelSettingUI.SetActive(false);
    }








    //function to pause the game
    public void SlideIn()
    {
        //enable the animator component
        animSet.enabled = true;
        PanelSettingUI.SetActive(true);
        //play the Slidein animation
        animSet.Play("InviteSlideIn");
        //set the isPaused flag to true to indicate that the game is paused

        //freeze the timescale

    }
    //function to unpause the game
    public void SlideOut()
    {
        //set the isPaused flag to false to indicate that the game is not paused

        //play the SlideOut animation
        animSet.Play("InviteSlideOut");
        //set back the time scale to normal time scale

    }

}