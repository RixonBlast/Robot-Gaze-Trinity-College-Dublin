using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class submitButtonObjectHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public objectCanvasHandler canvasHandler;    

    public RectTransform waves_rectTransform;

    public Transform button_transform;

    public Button submitButton;

    public TextMeshProUGUI submitText;

    public Image submitButtonImage;

    public Vector2 anchoredPosition;

    public bool wavesEnabled;

    public bool buttonHovered;

    public bool readyToClick;

    public bool activated;


    // Start is called before the first frame update
    void Start()
    {

        canvasHandler = GameObject.Find("Game Canvas Object").GetComponent<objectCanvasHandler>();

        button_transform = GetComponent<Transform>();

        submitButton = GetComponent<Button>();

        waves_rectTransform = button_transform.GetChild(0).GetComponent<RectTransform>();
        
        submitText = button_transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        submitButtonImage  = GetComponent<Image>();

        anchoredPosition = new Vector2(-315,0);
        
        wavesEnabled = false;

        buttonHovered = false;

        readyToClick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        { 
            if (!buttonHovered || !Input.GetMouseButton(0))
            {
                if (anchoredPosition.x > -315 & !canvasHandler.submitButtonPressed)
                {
                    submitText.text = "Submit";
                    anchoredPosition.x -= Time.deltaTime * 450f;
                }

                if (buttonHovered)
                {
                    readyToClick = true;
                }
            }
            else if (readyToClick)
            {
                if (anchoredPosition.x < -15)
                {
                    anchoredPosition.x += Time.deltaTime * 450f;
                    submitText.text = "Submitting...";
                }
                else
                {
                    canvasHandler.submitButtonPressed = true;
                }
            }
        
            waves_rectTransform.anchoredPosition = anchoredPosition;
        }
        
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonHovered = false;
        readyToClick = false;
    }

    public void toggleButton(bool value)
    {
        float value_float = value ? 1f: 0f;
        activated = value;
        submitButton.interactable = value;
        submitButtonImage.color = new Color(submitButtonImage.color.r,submitButtonImage.color.g,submitButtonImage.color.b,value_float*0.7f + 0.3f);
        submitText.color = new Color(submitButtonImage.color.r,submitButtonImage.color.g,submitButtonImage.color.b,value_float*0.7f + 0.3f);
        print(submitButtonImage.color);
    }

    public void appear()
    {
        toggleButton(false);
        anchoredPosition = new Vector2(-315,0);
        waves_rectTransform.anchoredPosition = anchoredPosition;
    }
}
