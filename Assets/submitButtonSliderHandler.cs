using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class submitButtonSliderHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{

    public sliderCanvasHandler canvasHandler;    

    public RectTransform waves_rectTransform;

    public Transform button_transform;

    public TextMeshProUGUI submitText;

    public Vector2 anchoredPosition;

    public bool wavesEnabled;

    public bool buttonHovered;


    // Start is called before the first frame update
    void Start()
    {

        canvasHandler = GameObject.Find("Game Canvas Slider").GetComponent<sliderCanvasHandler>();

        button_transform = GetComponent<Transform>();

        waves_rectTransform = button_transform.GetChild(0).GetComponent<RectTransform>();
        
        submitText = button_transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        anchoredPosition = new Vector2(-915f,0);
        
        wavesEnabled = false;

        buttonHovered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonHovered & Input.GetMouseButton(0))
        {
            print("Pressed!");
            if (anchoredPosition.x < -15)
            {
                anchoredPosition.x += Time.deltaTime * 1372.5f;
                submitText.text = "Submitting...";
            }
            else
            {
                canvasHandler.submitButtonPressed = true;
            }
        }
        else
        {
            if (anchoredPosition.x > -930 & !canvasHandler.submitButtonPressed)
            {
                submitText.text = "Submit";
                anchoredPosition.x -= Time.deltaTime * 1372.5f;
            }
        }
        
        waves_rectTransform.anchoredPosition = anchoredPosition;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Started the hovering");
        buttonHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Stopped it");
        buttonHovered = false;
    }
}
