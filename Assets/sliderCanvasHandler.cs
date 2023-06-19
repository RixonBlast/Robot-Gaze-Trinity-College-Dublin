using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderCanvasHandler : MonoBehaviour
{

    public RectTransform canvas_rectTransform;

    public Animator anim;

    public Vector2 displayPosition;

    public Vector2 hidePosition;

    public bool sliderCanvasTransitionInFinished;
    public bool sliderCanvasTransitionOutFinished;

    public bool submitButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        canvas_rectTransform = GetComponent<RectTransform>();

        anim = GetComponent<Animator>();

        displayPosition = new Vector2(0,0);

        hidePosition = new Vector2(1920,0);

        sliderCanvasTransitionInFinished = true;
        sliderCanvasTransitionOutFinished = true;

        submitButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sliderCanvasTransitionIn()
    {
        canvas_rectTransform.anchoredPosition = displayPosition;
        sliderCanvasTransitionInFinished = false;
        anim.Play("sliderCanvasAnimationIn");
    }

    public void sliderCanvasTransitionOut()
    {
        sliderCanvasTransitionOutFinished = false;
        anim.Play("sliderCanvasAnimationOut");
    }

    public void onSliderCanvasTransitionInFinished()
    {
        sliderCanvasTransitionInFinished = true;
        anim.Rebind();
    }

    public void onSliderCanvasTransitionOutFinished()
    {
        canvas_rectTransform.anchoredPosition = hidePosition;
        sliderCanvasTransitionOutFinished = true;
        anim.Rebind();
    }
}
