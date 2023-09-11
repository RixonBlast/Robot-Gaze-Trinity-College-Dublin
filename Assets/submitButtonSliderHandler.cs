using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>Handles the submit button of the canvas used to select the location of the user where he feels looked at
/// by the robot. This button is used to submit his answer.</summary>
public class submitButtonSliderHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{

///Instance of the class handling this canvas.
    public sliderCanvasHandler canvasHandler;    

///The <b>Recttransform GameObject</b> used to set the position of the loading blue rectangle that will be filling the submit button.
    public RectTransform loadingRectangle_rectTransform;

///The <b>Transform</b> component of the submit button.
    public Transform button_transform;

///The <b>TextMeshProUGUI</b> component containing the line that will be written on the submit button.
    public TextMeshProUGUI submitText;

///The position of the blue rectangle, representing the loading of the submit button, in the <b>RectTransform</b>
    public Vector2 anchoredPosition;

///The boolean indicating if the loadingRectangle should be enabled or disabled.
    public bool loadingRectangleEnabled;

///The boolean indicating whether yes or no the button is hovered by the mouse.
    public bool buttonHovered;

///The boolean indicating if the button is ready to be clicked.
    public bool readyToClick;


    /// <summary>Start is called before the first frame update</summary>
    void Start()
    {

        canvasHandler = GameObject.Find("Game Canvas Slider").GetComponent<sliderCanvasHandler>();

        button_transform = GetComponent<Transform>();

        loadingRectangle_rectTransform = button_transform.GetChild(0).GetComponent<RectTransform>();
        
        submitText = button_transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        //Initial position for the loading rectangle.
        anchoredPosition = new Vector2(-915f,0);
        
        //By default the loading rectangle is disabled
        loadingRectangleEnabled = false;

        //By default the mouse isn't hovering the button so it is set to false.
        buttonHovered = false;

        //By default the submit button isn't ready to be clicked on.
        readyToClick = false;

    }

    /// <summary>Update is called once per frame</summary>
    void Update()
    {
        //If the button is not hovered or if the mouse button isn't clicked...
        if (!buttonHovered || !Input.GetMouseButton(0))
        {
            //If the position of the loading rectangle isn't down then it goes down.
            if (anchoredPosition.x > -930 & !canvasHandler.submitButtonPressed)
            {
                submitText.text = "Submit";
                anchoredPosition.x -= Time.deltaTime * 1328f;
            }

            //If the button is now hovered it is ready to be clicked on.
            if (buttonHovered)
            {
                readyToClick = true;
            }
        }

        //If the button is ready to be clicked an the button is hovered and the mouse's button is pressed.
        else if (readyToClick)
        {
            //If the loading rectangle hasn't reached its maximum position then it goes up
            if (anchoredPosition.x < -15)
            {
                anchoredPosition.x += Time.deltaTime * 1328f;
                submitText.text = "Submitting...";
            }

            //If it has already reached its maximum position the submit button is considered as pressed.
            else
            {
                canvasHandler.submitButtonPressed = true;
            }
        }
        
        loadingRectangle_rectTransform.anchoredPosition = anchoredPosition;

    }

/// <summary> This method is called when the mouse enters the area of the submit button.</summary>
/// <param name="eventData"> Event payload associatied with pointer but it isn't used in the method.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonHovered = true;
    }

/// <summary> This method is called when the mouse exits the area of the submit button.</summary>
/// <param name="eventData"> Event payload associatied with pointer but it isn't used in the method.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonHovered = false;

        //The submit button is not ready to be clicked on anymore.
        readyToClick = false;
    }

/// <summary> This method is called when the submit button appears on the screen.
/// It is used to set the location of the loading rectangle and the state of the submit button.</summary>
    public void appear()
    {
        anchoredPosition = new Vector2(-930,0);
    }
}
