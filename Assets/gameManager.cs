using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameManager : MonoBehaviour
{

    public System.Random rng = new System.Random();

    public List<GameObject> objectList;

    public GameObject currentObject;
    public GameObject robot;

    public eyesState eyesHandler;
    public headState headHandler;
    public cameraHandler cameraHandler;
    public mainCanvasHandler canvasHandler;
    public sliderCanvasHandler sliderCanvasHandler;
    public objectCanvasHandler objectCanvasHandler;
    public sliderHandler slider;
    public objectInstructionHandler objectInstructionHandler;

    public TextMeshProUGUI instructions0;
    public TextMeshProUGUI instructions1;

    public Material[] canvas_materials;
    public CanvasRenderer canvas_renderer;
    public RawImage bigCanvasImage;
    public RawImage rightArrow;
    public RectTransform bigCanvas_transform;

    public Transform currentObject_transform;
    public Transform robot_transform;

    Vector3 robotSize;
    Vector3 bigCanvasTranslationVector;

    Vector2 currentBigCanvasTranslation;

    public Animator anim;

    Texture2D bigCanvasTexture; 
    public Texture2D rightArrowOn_texture;
    public Texture2D rightArrowOff_texture;

    public List<int> order0;
    public List<int> order1;

    public float angleAmplitude;
    public float objectDistance;
    public float bigCanvasAnimationTime;

    public int objectNb;
    public int state;
    public int nbTest;
    public int testIndex;

    public bool bigCanvasTranslatingIn;
    public bool bigCanvasTranslatingOut;
    public bool rightClicked;
    public bool leftClicked;
    public bool transitionState;
    public bool stateEntry;

    // Start is called before the first frame update
    void Start()
    {


        /*

        state = 0;


        //canvas_materials = GameObject.Find("RawImage").GetComponent<CanvasRenderer>().materials;
        canvas_renderer = GameObject.Find("Main Canvas").GetComponent<CanvasRenderer>();
        bigCanvasImage = GameObject.Find("Main Canvas").GetComponent<RawImage>();
        rightArrow = GameObject.Find("RawImage Button Right").GetComponent<RawImage>();
        instructions0 = GameObject.Find("Instructions0").GetComponent<TextMeshProUGUI>();
        instructions1 = GameObject.Find("Instructions1").GetComponent<TextMeshProUGUI>();

        canvasAnimation = GameObject.Find("Main Canvas").GetComponent<mainCanvasHandler>();

        bigCanvas_transform = GameObject.Find("Main Canvas").GetComponent<RectTransform>();

        bigCanvasTexture = (Texture2D)Resources.Load("bigCanvas");
        rightArrowOn_texture = (Texture2D)Resources.Load("Texture/right_arrow_on");
        rightArrowOff_texture = (Texture2D)Resources.Load("Texture/right_arrow_off");

        //print("rightArrowOff_texture: "+rightArrowOff_texture+"... yeah that's it'");
        //print("rightArrowOn_texture: "+rightArrowOn_texture+"... yeah that's it'");

        rightArrow.texture = rightArrowOff_texture;

        */

        //canvas_renderer.SetTexture(bigCanvasTexture);



        //Canvas handler initialization
        canvasHandler = GameObject.Find("Main Canvas").GetComponent<mainCanvasHandler>();

        //Slider Canvas initialization
        sliderCanvasHandler = GameObject.Find("Game Canvas Slider").GetComponent<sliderCanvasHandler>();

        //Object Canvas initialization
        objectCanvasHandler = GameObject.Find("Game Canvas Object").GetComponent<objectCanvasHandler>();

        //Instructions initialization
        canvasHandler.setTexts("Hello world.\n You will be participating to a test.","How are you?");

        //Camera initialization
        cameraHandler = GameObject.Find("Main Camera").GetComponent<cameraHandler>();

        //Head initialization
        headHandler = GameObject.Find("Head_Assembly").GetComponent<headState>();

        //Eyes initialization
        eyesHandler = GameObject.Find("Eye_Screen").GetComponent<eyesState>();

        //Y axis definition.

        //Object instruction handler initialization
        objectInstructionHandler = GameObject.Find("Object Instruction").GetComponent<objectInstructionHandler>();

        //Sets the state of the test
        state = 0;
        transitionState = false;
        stateEntry = true;

        //Initialize the slider
        slider = GameObject.Find("Slider").GetComponent<sliderHandler>();

        //Orders initialization
        nbTest = 625;

        for (int i=0;i<nbTest;i++)
        {
            order0.Add(i);
        }
        for (int i=0;i<nbTest;i++)
        {
            order1.Add(i);
        }

        Shuffle(order0);
        Shuffle(order1);

        //Debug part
        print("Liste 0:");
        for (int i=0;i<10;i++)
        {
            print(order0[i]);
        }
        print("Liste 0"+order0);
        print("Liste 1"+order1);
        
        //Debug part

        testIndex = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        if (transitionState)
        {
            if (canvasHandler.mainCanvasTranslationInFinished & canvasHandler.mainCanvasTranslationOutFinished &
            canvasHandler.textsTranslationLeftFinished & canvasHandler.textsTranslationRightFinished &
            sliderCanvasHandler.sliderCanvasTransitionInFinished & sliderCanvasHandler.sliderCanvasTransitionOutFinished &
            objectCanvasHandler.objectCanvasTransitionInFinished & objectCanvasHandler.objectCanvasTransitionOutFinished)
            {
                transitionState = false;
                canvasHandler.rightClicked = false;
                canvasHandler.leftClicked = false;
                sliderCanvasHandler.submitButtonPressed = false;
                objectCanvasHandler.submitButtonPressed = false;
            }
        }
        //First instruction screen
        else if (state==0)
        {
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(false,true);
                canvasHandler.setTexts("                            INTRODUCTION\n You will have to respond to a few questions.\n The experimentation implements two different testing methodologies and its duration is approximatively 12 minutes.\n For each methodology, you will have to perform a certain number of tests following the same process each time, it might seem slightly repetitive.\n Be careful, you can only answer once to each question.\n Please don't answer randomly.","FIRST METHODOLOGY");
                canvasHandler.setTextProperties(1,80,TextAlignmentOptions.Center, FontStyles.Underline);
                stateEntry = false;

            }
            else if (canvasHandler.rightClicked)
            {
                state=1;
                transitionState = true;
                canvasHandler.textsTranslationRight();
                stateEntry = true;
            }
        }

        //Introduction of the methodology screen
        else if (state==1)
        {
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(true,true);
                stateEntry = false;
            }
            else if (canvasHandler.leftClicked)
            {
                state=0;
                transitionState = true;
                canvasHandler.setText(1,"                            INTRODUCTION\n You will have to respond to a few questions.\n The experimentation implements two different testing methodologies and its duration is approximatively 12 minutes.\n For each methodology, you will have to perform a certain number of tests following the same process each time, it might seem slightly repetitive.\n Be careful, you can only answer once to each question.\n Please don't answer randomly.");
                
                canvasHandler.textsTranslationLeft();
                stateEntry = true;
            }
            else if (canvasHandler.rightClicked)
            {
                state=2;
                transitionState = true;
                canvasHandler.setText(1,"FIRST TESTING METHOD (1/"+nbTest+" iteration)\n You will soon be shown a scene with a robot looking somewhere.\n Your goal will be to: \n    -Select the object you think the robot is looking at. \n    -Submit your response.\n   -Observe carefully the robot moving its neck and eyes and repeat the process.");
                canvasHandler.textsTranslationRight();
                stateEntry = true;
            }
        }


        //Explanation of the methodology screen
        else if (state==2)
        {
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(true,true);
                stateEntry = false;
            }
            else if (canvasHandler.leftClicked)
            {
                state=1;
                transitionState = true;
                canvasHandler.textsTranslationLeft();
                stateEntry = true;
            }
            else if (canvasHandler.rightClicked)
            {
                state=3;
                transitionState = true;
                canvasHandler.mainCanvasTranslationOut();
                stateEntry = true;
            }
        }


        //First part of first methodology test transition state
        else if (state==3)
        {
            if (stateEntry)
            {
                objectInstructionHandler.setText("|1st PART|\n\n Select the object you think the robot is looking at, submit your answer and observe the robots face and eyes moving.");
                print("Hiya that's the "+testIndex+"th test");
            }

            objectCanvasHandler.objectCanvasTransitionIn();
            transitionState = true;
            state = 4;
        }

        //First part of first methodology test state
        else if (state==4)
        {

            if (stateEntry)
            {
                cameraHandler.setCameraMovementEnabled(true);
                stateEntry = false;
            }
            else if (objectCanvasHandler.submitButtonPressed)
            {
                print("Button pressed");
                state = 5;
                objectCanvasHandler.objectCanvasTransitionOut();
                transitionState = true;
                stateEntry = true;
            }
            //Create a class for Toggles and disable the submit button when not a single one toggle is enabled.
        }

        //Second part of first methodology test transition state
        else if (state==5)
        {
            if (stateEntry)
            {
                objectInstructionHandler.setText("|2nd PART|\n\n The robot is now looking elsewhere. Select again the object you think the robot is now looking at and submit your answer.");
            }

            objectCanvasHandler.objectCanvasTransitionIn();
            transitionState = true;
            state = 6;
        }

        //Second part of first methodology test state
        else if (state==6)
        {

            if (stateEntry)
            {
                cameraHandler.setCameraMovementEnabled(true);
                stateEntry = false;
            }
            else if (objectCanvasHandler.submitButtonPressed)
            {
                state = 7;
                objectCanvasHandler.objectCanvasTransitionOut();
                transitionState = true;
                stateEntry = true;
            }
            //Create a class for Toggles and disable the submit button when not a single one toggle is enabled.
        }


        //Transition to instruction for next iteration of first methodology test
        else if (state==7)
        {
            if (stateEntry)
            {
                testIndex++;
            }

            canvasHandler.setButtonsEnabled(false,true);
            canvasHandler.setTexts("FIRST TESTING METHOD ("+(testIndex + 1)+"/"+nbTest+" iteration)\n You will soon be shown a scene with a robot looking somewhere.\n Your goal will be to: \n    -Select the object you think the robot is looking at. \n    -Submit your response.\n   -Observe carefully the robot moving its neck and eyes and repeat the process.","");
            
            canvasHandler.mainCanvasTranslationIn();
            transitionState = true;
            state = 8;

        }

        //Instruction to next iteration of first methodology test
        else if (state==8)
        {
            if (stateEntry)
            {
                stateEntry = false;
                
            }
            else if (canvasHandler.rightClicked)
            {
                state=3;
                transitionState = true;
                canvasHandler.mainCanvasTranslationOut();
                stateEntry = true;
            }
        }

        //State for the slider canvas animation in
        else if (state==4)
        {
            if (stateEntry)
            {
                stateEntry = false;
            }
            else
            {
                sliderCanvasHandler.sliderCanvasTransitionIn();
                transitionState = true;
                state = 5;
                stateEntry = true;
            }
            
        }

        else if (state==5)
        {
            if (stateEntry)
            {

            }

            canvasHandler.mainCanvasTranslationIn();
            transitionState = true;
            state = 6;

        }

        else if (state==6)
        {
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(false,true);
                stateEntry = false;
            }

            /*
            else if (canvasHandler.leftClicked)
            {
                state=0;
                transitionState = true;
                canvasHandler.textsTranslationLeft();
                stateEntry = true;
            }
            */

            else if (canvasHandler.rightClicked)
            {
                state=2;
                transitionState = true;
                canvasHandler.mainCanvasTranslationOut();
                stateEntry = true;
            }
        }


        //Slider to change the position of the camera
        else if (state==5)
        {
            if (stateEntry)
            {
                print("ENTRY");
                canvasHandler.setButtonsEnabled(false,false);
                cameraHandler.setCameraMovementEnabled(true);
                headHandler.setHeadMovementEnabled(true);
                eyesHandler.setEyesMovementEnabled(true);
                slider.setSliderEnabled(true);
                stateEntry = false;

            }
            else if (sliderCanvasHandler.submitButtonPressed)
            {
                state = 6;
                sliderCanvasHandler.sliderCanvasTransitionOut();
                transitionState = true;
                stateEntry = true;
            }
        }


        else if (state==6)
        {
            if (stateEntry)
            {

            }

            canvasHandler.mainCanvasTranslationIn();
            transitionState = true;
            state = 1;

        }
    }

    public void Shuffle(List<int> order)
    {
        int n = order.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            int value = order[k];  
            order[k] = order[n];  
            order[n] = value;  
        }
    }

        /*

        if (state==0)
        {
            
            if (Input.GetKeyDown("i"))
            {
                mainCanvasTranslationIn();
            }
            else if (Input.GetKeyDown("o"))
            {
                mainCanvasTranslationOut();
            }
            if (canvasAnimation.finished==true)
            {
                state=1;
            }
           
        }
        else if (state==1)
        {
            if (Input.GetKeyDown("i"))
            {
                mainCanvasTranslationIn();
            }
            else if (Input.GetKeyDown("o"))
            {
                mainCanvasTranslationOut();
            }
            else if (Input.GetKeyDown("a"))
            {
                rightArrow.texture = rightArrowOn_texture;
                print("HAHAHAAAHAHA3");
            }
            else if (Input.GetKeyDown("b"))
            {
                anim.Play("textTranslation");
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
                
                
                    print("End of the in movement");
                    //#463dc2
                }
                else
                {
                    bigCanvasAnimationTime = 0;
                    bigCanvasTranslatingIn = false;
                }
                
                anim.Play("bigCanvasAnimationIn");
               
            }


        }
        
        */
}