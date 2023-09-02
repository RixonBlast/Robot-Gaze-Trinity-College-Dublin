using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary> Handles the right button of the main canvas used to give instruction to the user. This class only handles the visual aspect of the button.
/// The right button is used to go to the next slide.
/// It appears white when the mouse is hovering it and in a dark blue color when it is not hovered by the mouse.
/// The right button disappears when there is no page to go to.</summary>
public class rightButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{

/// Reference to the right button's arrow's <b>GameObject</b>
    public GameObject rightArrow;

/// Component to which will be assigned the texture of the button, that is an arrow. 
    public RawImage rightArrow_rawImage;

/// Texture for the white version of the button
    public Texture2D rightArrow_rawImageOn_texture;

/// Texture for the darker version of the button
    public Texture2D rightArrow_rawImageOff_texture;

/// <summary> Start is called before the first frame update </summary>
    void Start()
    {
        
    // Finding the right button's arrow's GameObject
        rightArrow = GameObject.Find("RawImage Button Right");

    // Loading the white texture
        rightArrow_rawImageOn_texture = (Texture2D)Resources.Load("Texture/right_arrow_on");

    // Looading the darker texture
        rightArrow_rawImageOff_texture = (Texture2D)Resources.Load("Texture/right_arrow_off");

    // Loading the RawImage component
        rightArrow_rawImage = GameObject.Find("RawImage Button Right").GetComponent<RawImage>();
        rightArrow_rawImage.texture = rightArrow_rawImageOff_texture;        
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
        rightArrow_rawImage.texture = rightArrow_rawImageOn_texture;
    }

/// <summary> This method detects when the mouse exits the button's hitbox and applies the darker texture.
/// </summary>
/// <param name = "eventData"> Event payload associatied with pointer but it isn't used in the method.
/// </param>
/// <returns> It returns nothing </returns>
    public void OnPointerExit(PointerEventData eventDate)
    {
        rightArrow_rawImage.texture = rightArrow_rawImageOff_texture;
    }

/// <summary>
/// This methods allows to enable or disable the right button.
/// It shall be disabled when there is no next instruction to go to. Basically at the end of the simulation.
/// </summary>
/// <param name = "value">
/// If value is set to true the right button will be enabled, if false then it will be disabled.
/// </param>
/// <returns>
/// It returns nothing
/// </returns>
    public void setButtonEnabled(bool value)
    {
        gameObject.SetActive(value);
        rightArrow.SetActive(value);
    }
}
