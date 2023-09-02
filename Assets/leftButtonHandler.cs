using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary> Handles the left button of the main canvas used to give instruction to the user. This class only handles the visual aspect of the button.
/// The left button is used to go back to the previous slide.
/// It appears white when the mouse is hovering it and in a dark blue color when it is not hovered by the mouse.
/// The left button disappears when there is no page to go back to.</summary>
public class leftButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

/// Reference to the left button's arrow's GameObject.
    public GameObject leftArrow;

/// Component to which will be assigned the texture of the button, that is an arrow. 
    public RawImage leftArrow_rawImage;

/// Texture for the white version of the button
    public Texture2D leftArrow_rawImageOn_texture;

/// Texture for the darker version of the button
    public Texture2D leftArrow_rawImageOff_texture;

/// <summary> Start is called before the first frame update </summary>
    void Start()
    {
    
    // Finding the left button's arrow's GameObject
        leftArrow = GameObject.Find("RawImage Button Left");

    // Loading the white texture
        leftArrow_rawImageOn_texture = (Texture2D)Resources.Load("Texture/left_arrow_on");

    // Looading the darker texture
        leftArrow_rawImageOff_texture = (Texture2D)Resources.Load("Texture/left_arrow_off");

    // Loading the RawImage component
        leftArrow_rawImage = GameObject.Find("RawImage Button Left").GetComponent<RawImage>();
        leftArrow_rawImage.texture = leftArrow_rawImageOff_texture;
        
    }


/// <summary> Update is called once per frame </summary>
    void Update()
    {
        
    }

/// <summary> This method detects when the mouse enters the button's hitbox and applies the white texture.
/// </summary>
/// <param name = "eventData"> Event payload associatied with pointer but it isn't used in the method.
/// </param>
/// <returns> It returns nothing </returns>
    public void OnPointerEnter(PointerEventData eventData)
    {
    // Applies the white texture
        leftArrow_rawImage.texture = leftArrow_rawImageOn_texture;
    }

/// <summary> This method detects when the mouse exits the button's hitbox and applies the darker texture.
/// </summary>
/// <param name = "eventData"> Event payload associatied with pointer but it isn't used in the method.
/// </param>
/// <returns> It returns nothing </returns>
    public void OnPointerExit(PointerEventData eventDate)
    {
        leftArrow_rawImage.texture = leftArrow_rawImageOff_texture;
    }

/// <summary>
/// This methods allows to enable or disable the left button.
/// It shall be disabled when there is no previous instruction slide to go back to and enabled otherwise.
/// </summary>
/// <param name = "value">
/// If value is set to true the left button will be enabled, if false then it will be disabled.
/// </param>
/// <returns>
/// It returns nothing
/// </returns>
    public void setButtonEnabled(bool value)
    {
        leftArrow.SetActive(value);
    }
}
