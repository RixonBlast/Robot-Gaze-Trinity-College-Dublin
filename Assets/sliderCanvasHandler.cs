using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>Handles the canvas used by the user to move himself where he feels being looked at by the robot.
/// A slider is used to move the camera and a submit button is used to submit the answer.
public class sliderCanvasHandler : MonoBehaviour
{

///Instance of the class handling the slider.
    public sliderHandler sliderHandler;

///Instance of the class handling the submit button
    public submitButtonSliderHandler submitButtonHandler;

///The RectTransform component used to position this canvas on the main canvas.
    public RectTransform canvas_rectTransform;

///The animator used to animate the transitions "in" and "out" of this canvas.
    public Animator anim;

///Position that this canvas needs to be in to be displayed on the screen.
    public Vector2 displayPosition;

///Position that this canvas needs to have to be hidden from the user's view.
    public Vector2 hidePosition;

///Boolean that is set to true when the canvas is finished transitioning on the screen.
    public bool sliderCanvasTransitionInFinished;

///Boolean that is set to true when the canvas is finished transitioning out of the screen.
    public bool sliderCanvasTransitionOutFinished;

///Boolean that is set to true when the submit button is pressed.
    public bool submitButtonPressed;

/// <summary>Start is called before the first frame update</summary>
    void Start()
    {
        sliderHandler = GameObject.Find("Slider").GetComponent<sliderHandler>();
        submitButtonHandler = GameObject.Find("Bot Subcanvas Button/Button").GetComponent<submitButtonSliderHandler>();
         
        canvas_rectTransform = GetComponent<RectTransform>();

        anim = GetComponent<Animator>();

        //The position for the canvas to be displayed.
        displayPosition = new Vector2(0,0);

        //The position for the canvas to be hidden.
        hidePosition = new Vector2(1920,0);

        //By default the transition "in" isn't finished so it's set to true.
        sliderCanvasTransitionInFinished = true;

        //By default the transition "out" isn't finished so it's set to true.
        sliderCanvasTransitionOutFinished = true;

        //By default the submit button isn't pressed so it's set to false.
        submitButtonPressed = false;
    }

/// <summary>Update is called once per frame</summary>
    void Update()
    {
        
    }

/// <summary>
/// This method is called when this canvas needs to transition "in". It plays the animation of transition.
/// The animation is a shade.
/// </summary>

    public void sliderCanvasTransitionIn()
    {
        //The canvas is moved to the position where it should be displayed.
        canvas_rectTransform.anchoredPosition = displayPosition;

        //The animator component is enabled in case it has been disabled to avoid bugs. 
        anim.enabled = true;

        //Set parameters for the submit button of this canvas
        submitButtonHandler.appear();

        sliderCanvasTransitionInFinished = false;

        //The shading animation plays.
        anim.Play("sliderCanvasAnimationIn");
    }


/// <summary>This method is called when this canvas needs to transition "out". It plays the animation of transition.
/// The animation is a shade.</summary>
    public void sliderCanvasTransitionOut()
    {
        sliderCanvasTransitionOutFinished = false;

        //It is no longer possible to move the camera using the arrow keys
        sliderHandler.setSliderEnabled(false);

        //The animator component is enabled in case it has been disabled to avoid bugs.
        anim.enabled = true;

        //The shading animation plays.
        anim.Play("sliderCanvasAnimationOut");
    }

/// <summary>This method is called by the <b>animator</b> component when the animation "in" is finished.
/// It is used to set some parameters for the canvas.</summary>
    public void onSliderCanvasTransitionInFinished()
    {
        //Enable the slider ot be moved by the arrow keys from the keyboard.
        sliderHandler.setSliderEnabled(true);
        //Notifies that the animation is finished.
        sliderCanvasTransitionInFinished = true;

        //Used to avoid some bugs.
        anim.Rebind();
        anim.enabled = false;
    }

/// <summary>This method is called by the <b>animator</b> component when the animation "out" is finished.
/// It is used to set some paramaters for the canvas.</summary>
    public void onSliderCanvasTransitionOutFinished()
    {
        //The canvas gets positioned at the hidden position.
        canvas_rectTransform.anchoredPosition = hidePosition;

        //Notifies that the animationo is finished.
        sliderCanvasTransitionOutFinished = true;

        //Used to avoid some bugs
        anim.Rebind();
        anim.enabled = false;
    }
}
