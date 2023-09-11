using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

/// <summary>Handles the main canvas used for giving instructions to the user.
/// It handles the instruction animation, the background animation and </summary>
public class mainCanvasHandler : MonoBehaviour
{
    
/// Instance of the class handling the left button of the main canvas.
    public leftButtonHandler leftButton_handler;

/// Instance of the class handling the right button of the main canvas.
    public rightButtonHandler rightButton_handler;

/// The RectTransform component used to position the blue background of the main canvas.
    public RectTransform backgroundImage_rectTransform;

/// The RectTransform component used to position the main canvas itself.
    public RectTransform mainCanvas_rectTransform;

/// The Transform component used to position the first text instruction area.
    public Transform instructions0_transform;

/// The transform component used to position the second text instruction area.
    public Transform instructions1_transform;

/// The animator used to animate the sliding of the instructions and the sliding of the whole main canvas.
    public Animator anim;

/// The TextMeshProUGUI component for the first text instruction area, used to edit the text.
    public TextMeshProUGUI instructions0;

/// The TextMeshProUGUI component for the second text instruction area, used to edit the text.
    public TextMeshProUGUI instructions1;

/// Final position, relatively to its parent, that the main canvas will take once its animation of translating in will be finished.
    Vector2 mainCanvasAnchoredPosition;

/// Flag indicating whether yes or no the animation of the main canvas translating out of the screen is finished.
    public bool mainCanvasTranslationOutFinished;

/// Flag indicating whether yes or no the animation of the main canvas translating on the screen is finished.
    public bool mainCanvasTranslationInFinished;

/// Flag indicating whether yes or no the animation of the instruction translating from the right to the left (switching to the next slide) is finished.
    public bool textsTranslationRightFinished;

/// Flag indicating whether yes or no the animation of the instruction translating from the left to the right (switching to the previous slide) is finished.
    public bool textsTranslationLeftFinished;

/// Flag indicating if the right button has been clicked on or not.
    public bool rightClicked;

/// Flag indicating if the left button has been clicked on or not.
    public bool leftClicked;

    
/// Start is called before the first frame update
    void Start()
    {
        
    // Loading components

        leftButton_handler = GameObject.Find("Button Left").GetComponent<leftButtonHandler>();
        rightButton_handler = GameObject.Find("Button Right").GetComponent<rightButtonHandler>();
        
        instructions0 = GameObject.Find("Instructions0").GetComponent<TextMeshProUGUI>();
        instructions1 = GameObject.Find("Instructions1").GetComponent<TextMeshProUGUI>();
        
        backgroundImage_rectTransform = GameObject.Find("Background Image").GetComponent<RectTransform>();
        mainCanvas_rectTransform = GetComponent<RectTransform>();

        instructions0_transform = GameObject.Find("Instructions0").GetComponent<Transform>();
        instructions1_transform = GameObject.Find("Instructions1").GetComponent<Transform>();

        anim = GetComponent<Animator>();

    // The position for the background after its translation in.
        mainCanvasAnchoredPosition = new Vector2(960,540);

    // Setting flags
        mainCanvasTranslationOutFinished = true;
        mainCanvasTranslationInFinished = true;
        textsTranslationRightFinished = true;
        textsTranslationLeftFinished = true;

        //instructions1.enabled = false;



    }

/// Update is called once per frame
    void Update()
    {
        
    }

/// <summary>
/// This method is called when the main canvas is finished translating out of the screen. It updates the value of the flag.
/// </summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void onMainCanvasTranslationOutFinished()
    {   
        mainCanvasTranslationOutFinished = true;
    }

/// <summary>
/// This method is called when the main canvas is finished translating on the screen. It updates the value of the flag.
/// </summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void onMainCanvasTranslationInFinished()
    {
    // The rebind of the animation is done manually to be able to change the position of the maincanvas and to rotate it.
        anim.Rebind();

        mainCanvas_rectTransform.anchoredPosition = mainCanvasAnchoredPosition;
        
    // The position is rotated in order to take back its initial position and orientation.
        backgroundImage_rectTransform.Rotate(0,0,180,Space.Self);

        mainCanvasTranslationInFinished = true;

    }

/// <summary>
/// This method is called when at the moment main canvas begins to translate on the screen.
/// It rotates the canvas in order to make the waves drawn on the background image of the main canvas appear on the screen during the translation.
/// </summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void mainCanvasTranslationIn()
    {
        backgroundImage_rectTransform.Rotate(0,0,180,Space.Self);
        anim.Play("mainCanvasAnimationIn");
        mainCanvasTranslationInFinished = false;
    }

/// <summary>
/// This method is called when at the moment main canvas begins to translate out of the screen.
/// It rotates the canvas in order to make the waves drawn on the background image of the main canvas appear on the screen during the translation.
/// </summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void mainCanvasTranslationOut()
    {
        anim.Play("mainCanvasAnimationOut");
        mainCanvasTranslationOutFinished = false;
    }

/// <summary>
/// This method is called when the right button is pressed. It updates the <b>rightClicked</b> flag. This flag will be updated to false once the class <b>gameManager</b> has handled it.
/// </summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void onClickRightMainCanvas()
    {
        rightClicked = true;
    }

/// <summary>
/// This method is called when the left button is pressed. It updates the <b>leftClicked</b> flag. This flag will be updated to false once the class <b>gameManager</b> has handled it.
/// </summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void onClickLeftMainCanvas()
    {
        leftClicked = true;
    }

/// <summary>
/// This method is used to set both text instructions.
/// Note that the first text instruction is the one that is currently displayed on the main canvas and the second one is the one that will replace the other when switching to either the previous or the next slide.
/// </summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void setTexts(String text1, String text2)
    {
       instructions0.text = text1;
       instructions1.text = text2;
    }

/// <summary>
/// This method is used to set one of the two text instructions.
/// Note that the first text instruction is the one that is currently displayed on the main canvas and the second one is the one that will replace the other when switching to either the previous or the next slide.
/// </summary>
/// <param name = "numText"> The number of the instruction that will be changed by the method. If its value is 0 then the first instruction will be changed, otherwise the second instruction will be changed.
/// <param>
/// <param name = "text"> The new text.
/// <returns>
/// It returns nothing
/// </returns>
    public void setText(int numText, String text)
    {
        if (numText == 0)
        {
            instructions0.text = text;
        }
        else
        {
            instructions1.text = text;
        }
    }

/// <summary>
/// This method is used to set one of the two text instructions' properties.
/// Note that the first text instruction is the one that is currently displayed on the main canvas and the second one is the one that will replace the other when switching to either the previous or the next slide.
/// </summary>
/// <param name = "numText"> The number of the instruction that will be changed by the method. If its value is 0 then the first instruction will be changed, otherwise the second instruction will be changed.
/// </param>
/// <param name = "size"> The font size of the text.
/// </param>
/// <param name = "alignment"> The alignment options for the text.
/// </param>
/// <param name = "style"> This is the style of the font, underlined etc...
/// </param>
/// <returns>
/// It returns nothing
/// </returns>
    public void setTextProperties(int numText, float size, TextAlignmentOptions alignment, FontStyles style)
    {
        if (numText == 0)
        {
            instructions0.fontSize = size;
            instructions0.alignment = alignment;
            instructions0.fontStyle = style;
        }
        else
        {
            instructions1.fontSize = size;
            instructions1.alignment = alignment;
            instructions1.fontStyle = style;
        }
    }

/// <summary>
/// This method reverses the content and settings of both the texts instruction. It is used after the two texts are finished translating and at the moment each of them goes back to its initial location to simulate them switching places.
/// <returns>
/// It returns nothing
/// </returns>
    public void reverseTexts()
    {
        String tmpString = instructions0.text;
        float size = instructions0.fontSize;
        TextAlignmentOptions alignment = instructions0.alignment;
        FontStyles style = instructions0.fontStyle;
        //TextAnchor anchor = instructions0.anchor;

        instructions0.text = instructions1.text;
        instructions1.text = tmpString;

        instructions0.fontSize = instructions1.fontSize;
        instructions1.fontSize = size;

        instructions0.alignment = instructions1.alignment;
        instructions1.alignment = alignment;

        instructions0.fontStyle = instructions1.fontStyle;
        instructions1.fontStyle = style;
        
        //instructions0.anchor = instructions1.anchor;
        //instructions1.anchor = anchor;
    }

/// <summary>This method is called to start the animation of translation of the texts from the right to the left (switching to the next slide).</summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void textsTranslationRight()
    {
        instructions1.enabled = true;
        anim.Play("textsTranslationRight");
        textsTranslationRightFinished = false;
    }

/// <summary>This method is called to start the animation of translation of the texts from the left to the right (switching to the previous slide).</summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void textsTranslationLeft()
    {
        instructions1.enabled = true;
        reverseTexts();
        anim.Play("textsTranslationLeft");
        textsTranslationLeftFinished = false;
    }

/// <summary>This method is called when the animation of translating of the texts from the right to the left (switching to the next slide) is finished.</summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void onTextsTranslationRightFinished()
    {   
        reverseTexts();
        textsTranslationRightFinished = true;
    // This is done manually to reset the animation in case there are some bugs related to the animation.
        anim.Rebind();
        instructions1.enabled = false;
    }

/// <summary>This method is called when the animation of translating of the texts from the left to the right (switching to the previous slide) is finished.</summary>
/// <returns>
/// It returns nothing
/// </returns>
    public void onTextsTranslationLeftFinished()
    {
        textsTranslationLeftFinished = true;
    // This is done manually to reset the animation in case there are some bugs related to the animation.
        anim.Rebind();
        instructions1.enabled = false;
    }

/// <summary> This methodsis used to enable or disable each buttons separately. </summary>
/// <param name = "leftButtonEnabled"> If its value is true then the left button will be enabled, otherwiste it will be disabled.
/// </param>
/// <param name = "rightuttonEnabled"> If its value is true then the right button will be enabled, otherwiste it will be disabled.
/// </param>
    public void setButtonsEnabled(bool leftButtonEnabled, bool rightButtonEnabled)
    {
        leftButton_handler.setButtonEnabled(leftButtonEnabled);
        rightButton_handler.setButtonEnabled(rightButtonEnabled);
    }

}
