using UnityEngine;
using UnityEngine.EventSystems;

public class ArenaPanelScript : MonoBehaviour
{

    
    public GameObject PanelSettingUI;
    
    private Animator animSet;
    
    void Start()
    {

        animSet = PanelSettingUI.GetComponent<Animator>();
        Debug.Log("what");
        PanelSettingUI.SetActive(false);
    }

    private void Update()
    {
        
    }






    //function to pause the game
    public void SlideIn()
    {
        //enable the animator component
        animSet.enabled = true;
        PanelSettingUI.SetActive(true);
        //play the Slidein animation
        animSet.Play("ArenaSlideIn");
        animSet.Play("ArenaBreath");
        //set the isPaused flag to true to indicate that the game is paused

        //freeze the timescale

    }
    //function to unpause the game
    public void SlideOut()
    {
        //set the isPaused flag to false to indicate that the game is not paused

        //play the SlideOut animation
        animSet.Play("ArenaSlideOut");
        //set back the time scale to normal time scale

    }

}