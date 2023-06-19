using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class togglesHandler : MonoBehaviour
{

    public Transform currentToggle;


    public List<Toggle> toggles;

    public Camera currentCam;

    public Transform currentObject_transform;
    public Transform parent_transform;
    public Transform toggles_transform;

    public RenderTexture renderTexture;

    public Texture2D currentTexture;

    public Vector3 tmpShift;

    // Start is called before the first frame update
    void Start()
    {

        toggles_transform = GetComponent<Transform>();

    /*
        parent_transform = GameObject.Find("Objects").GetComponent<Transform>();

        renderTexture = new RenderTexture(1920,1080,0);

        currentTexture = new Texture2D(1920,1080);

        tmpShift = new Vector3(100,0,0);
    */
        for (int i =0;i<toggles_transform.childCount;i++)
        {
            currentToggle = toggles_transform.GetChild(i);
            toggles.Add(currentToggle.GetComponent<Toggle>());
            //currentToggle.GetChild(2).GetComponent<RawImage>().texture = (RenderTexture)Resources.Load("RenderTexture/RenderTexture"+i);
        }

        /*
         for (int i =0;i<parent_transform.childCount;i++)
        {

            currentObject_transform = parent_transform.GetChild(i).GetComponent<Transform>();

            currentCam = currentObject_transform.GetChild(1).GetComponent<Camera>();

            currentObject_transform.Translate(tmpShift,Space.World);

            currentCam.targetTexture = renderTexture;

            RenderTexture.active = renderTexture;
             
            currentTexture.ReadPixels(new Rect(0,0,1920,1080),0,0);
            currentTexture.Apply();
             

            //currentTexture = (Texture2D)Resources.Load("Texture/left_arrow_off");

            toggles_transform.GetChild(i).GetComponent<Transform>().GetChild(2).GetComponent<RawImage>().texture = currentTexture;

            RenderTexture.active = null;
            currentCam.targetTexture = null;
            renderTexture.Release();

            currentObject_transform.Translate(-tmpShift,Space.World);

        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleUp(int toggle)
    {
        for (int i = 0;i<toggle; i++)
        {
            toggles[i].SetIsOnWithoutNotify(false);
        }

            toggles[toggle].SetIsOnWithoutNotify(true);

        for (int i = toggle+1;i<toggles_transform.childCount;i++)
        {
            toggles[i].SetIsOnWithoutNotify(false);
        }
    }

}
