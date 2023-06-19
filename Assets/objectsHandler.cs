using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectsHandler : MonoBehaviour
{
    public Transform parent_transform;
    public GameObject robot;

    public Camera currentCam;

    public Transform robot_transform;
    public Transform toggles_transform;
    public Transform currentFace_transform;

    public Material currentFace_material;

    public RenderTexture renderTexture;

    public Texture2D currentTexture;

    public Vector3 robotSize;
    public Vector3 tmpShift;
    
    float angleAmplitude;
    float objectDistance;

    public Material mat;
    // Start is called before the first frame update
    void Start()
    {

        Transform currentObject_transform;
        //Objects initialization
        robot = GameObject.Find("Robot_stevie");

        robot_transform = robot.GetComponent<Transform>();
        parent_transform = GetComponent<Transform>();
        toggles_transform = GameObject.Find("Toggles").GetComponent<Transform>();

        renderTexture = new RenderTexture(1920,1080,0);

        currentTexture = new Texture2D(1920,1080);

        robotSize = robot.GetComponent<Collider>().bounds.size;
        tmpShift = new Vector3(100,0,0);

        angleAmplitude = 120;
        objectDistance = 7;
        
        //print("Object list: "+objectList[0]+" AND "+objectList[1]);
        for (int i =0;i<parent_transform.childCount;i++)
        {
             currentObject_transform = parent_transform.GetChild(i).GetChild(0);
             
             for (int j=0;j<currentObject_transform.childCount;j++)
             {
                  currentFace_transform = currentObject_transform.GetChild(j);
                  currentFace_material = currentFace_transform.GetComponent<Renderer>().material;
                  currentFace_material.SetColor("_Color",Color.HSVToRGB((float)i/(float)parent_transform.childCount,1,1));
             }
             //Lines that will surely be removed when I'll be adding real objects to the scene and not cubes

             /*
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
            */
            
            /*Taking the screenshot in the RenderTexture files
            currentObject_transform.Translate(tmpShift,Space.World);

            currentObject_transform.GetChild(1).GetComponent<Camera>().enabled = false;

            currentObject_transform.Translate(-tmpShift,Space.World);
            */

            currentObject_transform.Translate((robot_transform.forward)*objectDistance+robot_transform.position-currentObject_transform.position + new Vector3(0,robotSize.y-0.7f,0),Space.World);

            currentObject_transform.RotateAround(robot_transform.position,robot_transform.up,i*angleAmplitude/(parent_transform.childCount-1) - angleAmplitude/2);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        print("TEST GOOD");
    }
}
