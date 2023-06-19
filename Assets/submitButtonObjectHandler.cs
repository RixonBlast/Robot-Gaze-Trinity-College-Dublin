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

    public TextMeshProUGUI submitText;

    public Vector2 anchoredPosition;

    public bool wavesEnabled;

    public bool buttonHovered;

    public bool readyToClick;


    // Start is called before the first frame update
    void Start()
    {

        canvasHandler = GameObject.Find("Game Canvas Object").GetComponent<objectCanvasHandler>();

        button_transform = GetComponent<Transform>();

        waves_rectTransform = button_transform.GetChild(0).GetComponent<RectTransform>();
        
        submitText = button_transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        anchoredPosition = new Vector2(-315,0);
        
        wavesEnabled = false;

        buttonHovered = false;

        readyToClick = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (buttonHovered && !Input.GetMouseButton(0))
        {
            readyToClick = true;
        }
        else if (buttonHovered && Input.GetMouseButton(0) && readyToClick)
        {
            if (anchoredPosition.x < -15)
            {
                anchoredPosition.x += Time.deltaTime * 450f;
                submitText.text = "Submitting...";
            }
            else
            {
                print("Submit button selected");
                canvasHandler.submitButtonPressed = true;
            }
        }
        else
        {
            readyToClick = false;

            if (anchoredPosition.x > -315 & !canvasHandler.submitButtonPressed)
            {
                submitText.text = "Submit";
                anchoredPosition.x -= Time.deltaTime * 450f;
            }
        }
        
        waves_rectTransform.anchoredPosition = anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonHovered = false;
    }
}
