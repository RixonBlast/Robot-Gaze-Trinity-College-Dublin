using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class leftButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject leftArrow;
    public RawImage leftArrow_rawImage;

    public Texture2D leftArrow_rawImageOn_texture;
    public Texture2D leftArrow_rawImageOff_texture;

    // Start is called before the first frame update
    void Start()
    {
        leftArrow = GameObject.Find("RawImage Button Left");
        leftArrow_rawImageOn_texture = (Texture2D)Resources.Load("Texture/left_arrow_on");
        leftArrow_rawImageOff_texture = (Texture2D)Resources.Load("Texture/left_arrow_off");

        leftArrow_rawImage = GameObject.Find("RawImage Button Left").GetComponent<RawImage>();

        leftArrow_rawImage.texture = leftArrow_rawImageOff_texture;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        leftArrow_rawImage.texture = leftArrow_rawImageOn_texture;
    }

    public void OnPointerExit(PointerEventData eventDate)
    {
        leftArrow_rawImage.texture = leftArrow_rawImageOff_texture;
    }

    public void setButtonEnabled(bool value)
    {
        gameObject.SetActive(value);
        leftArrow.SetActive(value);
    }
}
