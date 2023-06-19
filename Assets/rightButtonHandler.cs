using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class rightButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public GameObject rightArrow;
    public RawImage rightArrow_rawImage;

    public Texture2D rightArrow_rawImageOn_texture;
    public Texture2D rightArrow_rawImageOff_texture;

    // Start is called before the first frame update
    void Start()
    {
        
        rightArrow = GameObject.Find("RawImage Button Right");
        rightArrow_rawImageOn_texture = (Texture2D)Resources.Load("Texture/right_arrow_on");
        rightArrow_rawImageOff_texture = (Texture2D)Resources.Load("Texture/right_arrow_off");

        rightArrow_rawImage = GameObject.Find("RawImage Button Right").GetComponent<RawImage>();

        rightArrow_rawImage.texture = rightArrow_rawImageOff_texture;        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rightArrow_rawImage.texture = rightArrow_rawImageOn_texture;
    }

    public void OnPointerExit(PointerEventData eventDate)
    {
        rightArrow_rawImage.texture = rightArrow_rawImageOff_texture;
    }

    public void setButtonEnabled(bool value)
    {
        gameObject.SetActive(value);
        rightArrow.SetActive(value);
    }
}
