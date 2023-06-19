using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectCanvasHandler : MonoBehaviour
{

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
        //Part for toggles
        /*
        for (int i =0;i<parent_transform.childCount;i++)
        {

            currentObject_transform = parent_transform.GetChild(i).GetComponent<Transform>();

            //currentCam = currentObject_transform.GetChild(1).GetComponent<Camera>();
            currentCam = GameObject.Find("Main Camera").GetComponent<Camera>();

            currentObject_transform.Translate(tmpShift,Space.World);

            currentCam.targetTexture = renderTexture;

            RenderTexture.active = renderTexture;
             
            currentTexture.ReadPixels(new Rect(0,0,256,256),0,0);
            currentTexture.Apply();

            //currentTexture = (Texture2D)Resources.Load("Texture/left_arrow_off");

            toggles_transform.GetChild(i).GetComponent<Transform>().GetChild(2).GetComponent<RawImage>().texture = currentTexture;

            RenderTexture.active = null;
            currentCam.targetTexture = null;
            renderTexture.Release();
            
            currentObject_transform.Translate(-tmpShift,Space.World);

        }
        */
        /*
        for (int i = 0; i < 7; i++)
        {
            currentObject_transform = parent_transform.GetChild(i);
            currentObject_transform.Translate(tmpShift,Space.World);
            currentObject_transform.GetChild(1).GetComponent<Camera>().enabled = false;
            currentObject_transform.Translate(-tmpShift,Space.World);
        }
        */
        //END of it

        canvas_rectTransform.anchoredPosition = displayPosition;
        objectCanvasTransitionInFinished = false;
        anim.Play("objectCanvasAnimationIn");
    }

    public void objectCanvasTransitionOut()
    {
        objectCanvasTransitionOutFinished = false;
        anim.Play("objectCanvasAnimationOut");
    }

    public void onObjectCanvasTransitionInFinished()
    {
        objectCanvasTransitionInFinished = true;
        anim.Rebind();
    }

    public void onObjectCanvasTransitionOutFinished()
    {
        canvas_rectTransform.anchoredPosition = hidePosition;
        objectCanvasTransitionOutFinished = true;
        anim.Rebind();
    }
}
