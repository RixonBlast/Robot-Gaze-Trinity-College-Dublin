using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class mainCanvasHandler : MonoBehaviour
{
    
    public leftButtonHandler leftButton_handler;
    public rightButtonHandler rightButton_handler;

    public RectTransform backgroundImage_rectTransform;
    public RectTransform mainCanvas_rectTransform;

    public Transform instructions0_transform;
    public Transform instructions1_transform;

    public Animator anim;

    public TextMeshProUGUI instructions0;
    public TextMeshProUGUI instructions1;

    Vector3 textsTranslation_vector;

    Vector2 mainCanvasAnchoredPosition;

    public bool mainCanvasTranslationOutFinished;
    public bool mainCanvasTranslationInFinished;
    public bool textsTranslationRightFinished;
    public bool textsTranslationLeftFinished;
    public bool rightClicked;
    public bool leftClicked;

    
    // Start is called before the first frame update
    void Start()
    {
        leftButton_handler = GameObject.Find("Button Left").GetComponent<leftButtonHandler>();
        rightButton_handler = GameObject.Find("Button Right").GetComponent<rightButtonHandler>();

        instructions0 = GameObject.Find("Instructions0").GetComponent<TextMeshProUGUI>();
        instructions1 = GameObject.Find("Instructions1").GetComponent<TextMeshProUGUI>();

        backgroundImage_rectTransform = GameObject.Find("Background Image").GetComponent<RectTransform>();
        mainCanvas_rectTransform = GetComponent<RectTransform>();

        instructions0_transform = GameObject.Find("Instructions0").GetComponent<Transform>();
        instructions1_transform = GameObject.Find("Instructions1").GetComponent<Transform>();

        anim = GetComponent<Animator>();

        mainCanvasAnchoredPosition = new Vector2(960,540);

        mainCanvasTranslationOutFinished = true;
        mainCanvasTranslationInFinished = true;
        textsTranslationRightFinished = true;
        textsTranslationLeftFinished = true;

        instructions1.enabled = false;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onMainCanvasTranslationOutFinished()
    {   
        mainCanvasTranslationOutFinished = true;
    }

    public void onMainCanvasTranslationInFinished()
    {
        
        anim.Rebind();

        mainCanvas_rectTransform.anchoredPosition = mainCanvasAnchoredPosition;
        backgroundImage_rectTransform.Rotate(0,0,180,Space.Self);

        mainCanvasTranslationInFinished = true;

    }

    public void mainCanvasTranslationIn()
    {
        backgroundImage_rectTransform.Rotate(0,0,180,Space.Self);
        anim.Play("mainCanvasAnimationIn");
        mainCanvasTranslationInFinished = false;
    }

    public void mainCanvasTranslationOut()
    {
        anim.Play("mainCanvasAnimationOut");
        mainCanvasTranslationOutFinished = false;
    }

    public void onClickRightMainCanvas()
    {
        rightClicked = true;
    }

    public void onClickLeftMainCanvas()
    {
        leftClicked = true;
    }

    public void setTexts(String text1, String text2)
    {
       instructions0.text = text1;
       instructions1.text = text2;
    }

    public void reverseTexts()
    {
        String tmpString = instructions0.text;
        instructions0.text = instructions1.text;
        instructions1.text = tmpString;
    }

    public void textsTranslationRight()
    {
        instructions1.enabled = true;
        anim.Play("textsTranslationRight");
        textsTranslationRightFinished = false;
    }

    public void textsTranslationLeft()
    {
        instructions1.enabled = true;
        reverseTexts();
        anim.Play("textsTranslationLeft");
        textsTranslationLeftFinished = false;
    }

    public void onTextsTranslationRightFinished()
    {   
        reverseTexts();
        textsTranslationRightFinished = true;
        anim.Rebind();
        instructions1.enabled = false;
    }

    public void onTextsTranslationLeftFinished()
    {
        textsTranslationLeftFinished = true;
        anim.Rebind();
        instructions1.enabled = false;
    }

    public void setButtonsEnabled(bool leftButtonEnabled, bool rightButtonEnabled)
    {
        leftButton_handler.setButtonEnabled(leftButtonEnabled);
        rightButton_handler.setButtonEnabled(rightButtonEnabled);
    }

}
