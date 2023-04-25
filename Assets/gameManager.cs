using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameManager : MonoBehaviour
{
    public List<GameObject> objectList;

    public GameObject currentObject;
    public GameObject robot;

    public Material[] canvas_materials;
    public CanvasRenderer canvas_renderer;
    public RawImage bigCanvasImage;
    public RectTransform bigCanvas_transform;

    public Transform currentObject_transform;
    public Transform robot_transform;

    Vector3 yAxis;
    Vector3 robotSize;
    Vector3 bigCanvasTranslationVector;

    Vector2 currentBigCanvasTranslation;

    public Animator anim;

    Texture2D bigCanvasTexture; 

    public float angleAmplitude;
    public float objectDistance;
    public float bigCanvasAnimationTime;

    public int objectNb;

    public bool bigCanvasTranslatingIn;
    public bool bigCanvasTranslatingOut;

    // Start is called before the first frame update
    void Start()
    {

        robot = GameObject.Find("Robot_stevie");

        //canvas_materials = GameObject.Find("RawImage").GetComponent<CanvasRenderer>().materials;
        canvas_renderer = GameObject.Find("RawImage").GetComponent<CanvasRenderer>();
        bigCanvasImage = GameObject.Find("RawImage").GetComponent<RawImage>();

        bigCanvas_transform = GameObject.Find("RawImage").GetComponent<RectTransform>();

        bigCanvasTexture = (Texture2D)Resources.Load("bigCanvas");

        anim = GameObject.Find("RawImage").GetComponent<Animator>();

        canvas_renderer.SetTexture(bigCanvasTexture);

        if (GameObject.Find("RawImage").GetComponent<CanvasRenderer>())
        {
            print("Yes it's not null!");
        }
        else{
            print("bro failed");
        }

        robot_transform = robot.GetComponent<Transform>();

        robotSize = robot.GetComponent<Collider>().bounds.size;

        yAxis = new Vector3(0,1,0);

        

        bigCanvasTranslationVector = new Vector3(1f,-1f,0);
        currentBigCanvasTranslation = new Vector2(0,0);

        int objectNb = 0;

        angleAmplitude = 120;
        objectDistance = 5;
        bigCanvasAnimationTime = 0;


        objectList = new List<GameObject>();

        while (currentObject = GameObject.Find("object"+objectNb))
        {
            objectList.Add(currentObject);
            objectNb += 1;
        }
        
        //print("Object list: "+objectList[0]+" AND "+objectList[1]);
        for (int i =0;i<objectNb;i++)
        {
            currentObject_transform = objectList[i].GetComponent<Transform>();

            currentObject_transform.Translate((robot_transform.forward)*objectDistance+robot_transform.position-currentObject_transform.position + new Vector3(0,robotSize.y,0),Space.World);

            currentObject_transform.RotateAround(robot_transform.position,yAxis,i*angleAmplitude/(objectNb-1) - angleAmplitude/2);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i"))
        {
            bigCanvasTranslationIn();
        }
        else if (Input.GetKeyDown("o"))
        {
            bigCanvasTranslationOut();
        }

        if (bigCanvasTranslatingIn)
        {
            if (bigCanvasAnimationTime < 1f)
            {
                bigCanvasAnimationTime += Time.deltaTime;

                currentBigCanvasTranslation.x -= Time.deltaTime * 500;
                currentBigCanvasTranslation.y += Time.deltaTime * 500;

                //canvas_materials[0].mainTextureOffset = currentBigCanvasTranslation;
                //canvas_material.mainTextureOffset = currentBigCanvasTranslation;
                //bigCanvasImage.uvRect.x = -100;
                //bigCanvas_transform.Translate(bigCanvasTranslationVector*Time.deltaTime,Space.Self);
                anim.Play("bigCanvasAnimationIn");
                print("End of the in movement");

            }
            else
            {
                bigCanvasAnimationTime = 0;
                bigCanvasTranslatingIn = false;
            }
        }

        if (bigCanvasTranslatingOut)
        {
            if (bigCanvasAnimationTime < 1f)
            {
                bigCanvasAnimationTime += Time.deltaTime;

                currentBigCanvasTranslation.x += Time.deltaTime * 500;
                currentBigCanvasTranslation.y -= Time.deltaTime * 500;

                //canvas_materials[0].mainTextureOffset = currentBigCanvasTranslation;
                //canvas_renderer.mainTextureOffset = currentBigCanvasTranslation;
                //bigCanvas_transform.Translate(-bigCanvasTranslationVector*Time.deltaTime,Space.Self);
                anim.Play("bigCanvasAnimationOut");
                print("End of the out movement");

            }
            else
            {
                bigCanvasAnimationTime = 0;
                bigCanvasTranslatingOut = false;
            }
        }

    }

    void bigCanvasTranslationIn()
    {
        bigCanvasTranslatingIn = true;
    }

    void bigCanvasTranslationOut()
    {
        bigCanvasTranslatingOut = true;
    }
}
