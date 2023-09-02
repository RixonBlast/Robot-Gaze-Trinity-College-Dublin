using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

///<summary>Handles the canvas used to select the object at which the robot is looking.
///Toggle buttons are used to select the object and a submit button is used to submit the answer.</summary>
public class objectCanvasHandler : MonoBehaviour
{
///Instance of the class handling the submit button
    public submitButtonObjectHandler submitButtonHandler;

///Instance of the class handling the toggle buttons
    public togglesHandler togglesHandler;

///The RectTransform component used to position this canvas on the main canvas.
    public RectTransform canvas_rectTransform;

///The animator used to animate the transitions "in" and "out" of this canvas.
    public Animator anim;

///Position that this canvas needs to be in to be displayed on the screen.
    public Vector2 displayPosition;

///Position that this canvas needs to have to be hidden from the user's view.
    public Vector2 hidePosition;

///Boolean that is set to true when the canvas is finished transitioning on the screen.
    public bool objectCanvasTransitionInFinished;

///Boolean that is set to true when the canvas is finished transitioning out of the screen.
    public bool objectCanvasTransitionOutFinished;

///Boolean that is set to true when the submit button is pressed.
    public bool submitButtonPressed;

/// Start is called before the first frame update
    void Start()
    {
        submitButtonHandler = GameObject.Find("Bot Subcanvas Button/Submit Button Object").GetComponent<submitButtonObjectHandler>();

        togglesHandler = GameObject.Find("Toggles").GetComponent<togglesHandler>();

        canvas_rectTransform = GetComponent<RectTransform>();

        anim = GetComponent<Animator>();

        //The position for the canvas to be displayed.
        displayPosition = new Vector2(0,0);

        //The position for the canvas to be hidden.
        hidePosition = new Vector2(1920,0);

        //By default the transition "in" isn't finished so it's set to true.
        objectCanvasTransitionInFinished = true;

        //By default the transition "out" isn't finished so it's set to true.
        objectCanvasTransitionOutFinished = true;

        //By default the submit button isn't pressed so it's set to false.
        submitButtonPressed = false;
    }

/// Update is called once per frame
    void Update()
    {
        
    }


/// <summary>This method is called when this canvas needs to transition "in". It plays the animation of transition.
/// The animation is a shade.</summary>
    public void objectCanvasTransitionIn()
    {
        //The animator component is enabled in cas it has been disabled to avoid bugs. 
        anim.enabled = true;

        //These two functions are called to set parameters on the toggle and submit buttons when they appear.
        togglesHandler.appear();
        submitButtonHandler.appear();

        //The canvas is moved to the position where it should be displayed.
        canvas_rectTransform.anchoredPosition = displayPosition;
        objectCanvasTransitionInFinished = false;

        //The shading animation plays.
        anim.Play("objectCanvasAnimationIn");
    }

/// <summary>This method is called when this canvas needs to transition "out". It plays the animation of transition.
/// The animation is a shade.</summary>
    public void objectCanvasTransitionOut()
    {
        //It is no longer possible to interact with the toggle buttons.
        togglesHandler.setInteractable(false);

        //The animator component is enabled in case it has been disabled to avoid bugs. 
        anim.enabled = true;

        objectCanvasTransitionOutFinished = false;

        //The shading animation plays.
        anim.Play("objectCanvasAnimationOut");
    }


 /// <summary>This method is called by the <b>animator</b> component when the animation "in" is finished.
 /// It is used to set some parameters for the canvas.</summary>
    public void onObjectCanvasTransitionInFinished()
    {
        //Notifies that the animation is finished.
        objectCanvasTransitionInFinished = true;

        //Used to avoid some bugs.
        anim.Rebind();
        anim.enabled = false;

        //Toggle buttons usable.
        togglesHandler.setInteractable(true);
    }


 /// <summary>This method is called by the <b>animator</b> component when the animation "out" is finished.
 /// It is used to set some paramaters for the canvas.</summary>
    public void onObjectCanvasTransitionOutFinished()
    {
        //The canvas gets positioned at the hidden position.
        canvas_rectTransform.anchoredPosition = hidePosition;

        //Notifies that the animation is finished.
        objectCanvasTransitionOutFinished = true;

        //Used to avoid some bugs.
        anim.Rebind();
        anim.enabled = false;
    }
}
