using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectCanvasHandler : MonoBehaviour
{
    
    public submitButtonObjectHandler submitButtonHandler;

    public togglesHandler togglesHandler;

    public objectInstructionHandler instructionHandler;

    public RectTransform canvas_rectTransform;

    public Animator anim;

    public Vector2 displayPosition;

    public Vector2 hidePosition;

    public bool objectCanvasTransitionInFinished;
    public bool objectCanvasTransitionOutFinished;

    public bool submitButtonPressed;

    //part for toggles
    //public Camera currentCam;

    //public Transform currentObject_transform;
    //public Transform parent_transform;
    //public Transform toggles_transform;

    //public RenderTexture renderTexture;

    //public Texture2D currentTexture;

    //public Vector3 tmpShift;
    //END of it 

    // Start is called before the first frame update
    void Start()
    {
        submitButtonHandler = GameObject.Find("Bot Subcanvas Button/Submit Button Object").GetComponent<submitButtonObjectHandler>();

        togglesHandler = GameObject.Find("Toggles").GetComponent<togglesHandler>();

        instructionHandler = GameObject.Find("Object Instruction").GetComponent<objectInstructionHandler>();

        //part for toggles
        
        //toggles_transform = GameObject.Find("Toggles").GetComponent<Transform>();

        //parent_transform = GameObject.Find("Objects").GetComponent<Transform>();

        //renderTexture = new RenderTexture(1920,1080,0);
        //renderTexture = (RenderTexture)Resources.Load("RenderTexture/RenderTextureTest");

        //currentTexture = new Texture2D(1920,1080);

        //tmpShift = new Vector3(100,0,0);
        //END of it

        canvas_rectTransform = GetComponent<RectTransform>();

        anim = GetComponent<Animator>();

        displayPosition = new Vector2(0,0);

        hidePosition = new Vector2(1920,0);

        objectCanvasTransitionInFinished = true;
        objectCanvasTransitionOutFinished = true;

        submitButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void objectCanvasTransitionIn()
    {
        anim.enabled = true;

        togglesHandler.appear();
        submitButtonHandler.appear();

        canvas_rectTransform.anchoredPosition = displayPosition;
        objectCanvasTransitionInFinished = false;
        anim.Play("objectCanvasAnimationIn");
    }

    public void objectCanvasTransitionOut()
    {
        togglesHandler.setInteractable(false);
        anim.enabled = true;
        objectCanvasTransitionOutFinished = false;
        anim.Play("objectCanvasAnimationOut");
    }

    public void onObjectCanvasTransitionInFinished()
    {
        objectCanvasTransitionInFinished = true;
        anim.Rebind();
        anim.enabled = false;
        togglesHandler.setInteractable(true);
    }

    public void onObjectCanvasTransitionOutFinished()
    {
        canvas_rectTransform.anchoredPosition = hidePosition;
        objectCanvasTransitionOutFinished = true;
        anim.Rebind();
        anim.enabled = false;
    }
}
