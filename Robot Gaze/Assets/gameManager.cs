using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>This class utilizes the state machine principle to manage the simulation flow.
/// Each state corresponds to a transition point, an instruction page, or a robot simulation with a question-answer user interface.
/// Each state has input and output instructions, as well as looping instructions to execute.
/// The transition state is a special state where transitions occur.
/// It allows waiting for animation completion before proceeding with other states.</summary>
public class gameManager : MonoBehaviour
{

///System.Random instance for generating random values
    public System.Random rng = new System.Random();

//Instances of the classes handling a lot of behaviours.
    public eyesHandler eyesHandler;
    public headState headHandler;
    public cameraHandler cameraHandler;
    public mainCanvasHandler canvasHandler;
    public sliderCanvasHandler sliderCanvasHandler;
    public objectCanvasHandler objectCanvasHandler;
    public sliderHandler slider;
    public objectInstructionHandler objectInstructionHandler;
    public sliderInstructionHandler sliderInstructionHandler;

///The <b>TextMeshProUGUI</b> component used for the instruction of the current instruction page
    public TextMeshProUGUI instructions0;

///The <b>TextMeshProUGUI</b> component used for the instruction of the next instruction page
    public TextMeshProUGUI instructions1;

///List containing the random sequence of eye and head positions combinations for the Robot for the first part of the procedure.
    public List<int> order0;

///List containing the random sequence of eye and head positions combinations for the Robot for the second part of the procedure.
    public List<int> order1;

///The index corresponding to the state the simulation is currently playing
    public int state;

///The total number of test that the user will have to complete in each procedure. A test is bounded by instruction pages.
    public int nbTest;

///The index of the test that is taking place.
    public int testIndex;

///The number of different possible combinations for the test randomizer to randomize them.
    public int nbPossibilities;

///Previous state. Used to differentiate the origin of the transition in a specific state and use proper animation.
    public int previousState;

///When set to true the transition state will be 
    public bool transitionState;
    public bool stateEntry;



    // Start is called before the first frame update
    void Start()
    {


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
        eyesHandler = GameObject.Find("Eye_Screen").GetComponent<eyesHandler>();

        //Y axis definition.

        //Object instruction handler initialization
        objectInstructionHandler = GameObject.Find("Object Instruction").GetComponent<objectInstructionHandler>();

        //Slider instruction handler initialization
        sliderInstructionHandler = GameObject.Find("Slider Instruction").GetComponent<sliderInstructionHandler>();

        //Sets the state of the test
        state = 0;
        previousState = 0;
        transitionState = false;
        stateEntry = true;

        //Initialize the slider
        slider = GameObject.Find("Slider").GetComponent<sliderHandler>();

        //Orders initialization
        nbPossibilities = eyesHandler.nbEyesState * headHandler.nbHeadState;

        for (int i=0;i<nbPossibilities;i++)
        {
            order0.Add(i);
        }
        for (int i=0;i<nbPossibilities;i++)
        {
            order1.Add(i);
        }

        Shuffle(order0);
        Shuffle(order1);

        nbTest = 2;

        testIndex = 0;

    }

    /// Update is called once per frame
    void Update()
    {
        
        //Transition state in which the simulation will let the animations play before going back to the normal states.
        if (transitionState)
        {
            //Once there isn't any animation playing...
            if (canvasHandler.mainCanvasTranslationInFinished & canvasHandler.mainCanvasTranslationOutFinished &
            canvasHandler.textsTranslationLeftFinished & canvasHandler.textsTranslationRightFinished &
            sliderCanvasHandler.sliderCanvasTransitionInFinished & sliderCanvasHandler.sliderCanvasTransitionOutFinished &
            objectCanvasHandler.objectCanvasTransitionInFinished & objectCanvasHandler.objectCanvasTransitionOutFinished &
            !headHandler.onMovement & !eyesHandler.onMovement)
            {
                //Some parameters are reset
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
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(false,true);
                canvasHandler.setTexts("                            INTRODUCTION\n You will have to respond to a few questions.\n The experimentation implements two different testing procedures and its duration is approximatively 12 minutes.\n For each procedure, you will have to perform a certain number of tests following the same process each time, it might seem slightly repetitive.\n Be careful, you can only answer once to each question.\n Please don't answer randomly.","FIRST PROCEDURE");
                canvasHandler.setTextProperties(1,80,TextAlignmentOptions.Center, FontStyles.Underline);
                stateEntry = false;

            }
            //If the right button is clicked to get to the next page
            else if (canvasHandler.rightClicked)
            {
                state=1;
                transitionState = true;
                canvasHandler.textsTranslationRight();
                stateEntry = true;
            }
        }

        //Introduction of the simulation screen
        else if (state==1)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(true,true);
                stateEntry = false;
            }
            //If the left button is clicked to get to the previous page
            else if (canvasHandler.leftClicked)
            {
                state=0;
                transitionState = true;
                canvasHandler.setText(1,"                            INTRODUCTION\n You will have to respond to a few questions.\n The experimentation implements two different testing procedures and its duration is approximatively 12 minutes.\n For each procedure, you will have to perform a certain number of tests following the same process each time, it might seem slightly repetitive.\n Be careful, you can only answer once to each question.\n Please don't answer randomly.");
                
                canvasHandler.textsTranslationLeft();
                stateEntry = true;
            }
            //If the right button is clicked to get to the next page
            else if (canvasHandler.rightClicked)
            {
                state=2;
                transitionState = true;
                canvasHandler.setText(1,"FIRST TESTING PROCEDURE (1/"+nbTest+" iteration)\n You will soon be shown a scene with a robot looking somewhere.\n Your goal will be to: \n    -Select the object you think the robot is looking at. \n    -Submit your response.\n   -Observe carefully the robot moving its neck and eyes and repeat the process.");
                canvasHandler.textsTranslationRight();
                stateEntry = true;
            }
        }


        //Explanation of the procedures screen
        else if (state==2)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(true,true);
                stateEntry = false;
            }
            //If the left button is clicked to get to the previous page
            else if (canvasHandler.leftClicked)
            {
                state=1;
                transitionState = true;
                canvasHandler.textsTranslationLeft();
                stateEntry = true;
            }
            //If the right button is clicked to get to the next page
            else if (canvasHandler.rightClicked)
            {
                state=3;
                transitionState = true;
                canvasHandler.mainCanvasTranslationOut();
                headHandler.smoothHeadMovement(order0[testIndex*2]/eyesHandler.nbEyesState);
                eyesHandler.smoothEyesMovement(order0[testIndex*2]%eyesHandler.nbEyesState);
                stateEntry = true;
            }
        }


        //First part of first procedure test state
        else if (state==3)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                objectInstructionHandler.setText("|1st PART|\n\n Select the object you think the robot is looking at, submit your answer and observe the robots face and eyes moving.");
                objectCanvasHandler.objectCanvasTransitionIn();
                transitionState = true;
                cameraHandler.setCameraMovementEnabled(true);
                stateEntry = false;
            }
            else if (objectCanvasHandler.submitButtonPressed)
            {
                state = 4;
                objectCanvasHandler.objectCanvasTransitionOut();
                headHandler.smoothHeadMovement(order0[testIndex*2+1]/eyesHandler.nbEyesState);
                eyesHandler.smoothEyesMovement(order0[testIndex*2+1]%eyesHandler.nbEyesState);
                transitionState = true;
                stateEntry = true;
            }
        }

        //Second part of first procedure test state
        else if (state==4)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                objectInstructionHandler.setText("|2nd PART|\n\n The robot is now looking elsewhere. Select again the object you think the robot is now looking at and submit your answer.");
                objectCanvasHandler.objectCanvasTransitionIn();
                transitionState = true;
                cameraHandler.setCameraMovementEnabled(true);
                stateEntry = false;
            }
            else if (objectCanvasHandler.submitButtonPressed)
            {
                testIndex++;
                if (testIndex < nbTest)
                {
                    state = 5;
                }
                else
                {
                    state = 6;
                }
                objectCanvasHandler.objectCanvasTransitionOut();
                transitionState = true;
                stateEntry = true;
            }
        }


        //Instruction to next iteration of first procedure test
        else if (state==5)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(false,true);
                canvasHandler.setTexts("FIRST TESTING PROCEDURE ("+(testIndex +1)+"/"+nbTest+" iteration)\n You will soon be shown a scene with a robot looking somewhere.\n Your goal will be to: \n    -Select the object you think the robot is looking at. \n    -Submit your response.\n   -Observe carefully the robot moving its neck and eyes and repeat the process.","");
                canvasHandler.mainCanvasTranslationIn();
                transitionState = true;
                stateEntry = false;
            }
            //If the right button is clicked to get to the next page
            else if (canvasHandler.rightClicked)
            {
                state=3;
                transitionState = true;
                canvasHandler.mainCanvasTranslationOut();
                headHandler.smoothHeadMovement(order0[testIndex*2]/eyesHandler.nbEyesState);
                eyesHandler.smoothEyesMovement(order0[testIndex*2]%eyesHandler.nbEyesState);
                stateEntry = true;
            }
        }

        //Page announcing procedure switch
        else if (state==6)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
            canvasHandler.setButtonsEnabled(false,true);
            canvasHandler.setText(0,"                            PROCEDURE SWITCH\n Congratulation you have reached the second part of the experimentation.");
            canvasHandler.setText(1,"SECOND PROCEDURE");
            canvasHandler.setTextProperties(1,80,TextAlignmentOptions.Center, FontStyles.Underline);
                if (previousState!=7)
                {
                    canvasHandler.mainCanvasTranslationIn();
                    transitionState = true;
                }
                testIndex = 0;
                canvasHandler.setButtonsEnabled(false,true);
                stateEntry = false;
            }
            //If the right button is clicked to get to the next page
            else if (canvasHandler.rightClicked)
            {
                state=7;
                transitionState = true;
                canvasHandler.textsTranslationRight();
                stateEntry = true;
            }
        }

        //Big title "SECOND PROCEDURE" for the second procedure of testing
        else if (state==7)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(true,true);
                stateEntry = false;
            }
            //If the left button is clicked to get to the previous page
            else if (canvasHandler.leftClicked)
            {
                previousState = state;
                state=6;
                transitionState = true;
                canvasHandler.setText(1,"                            PROCEDURE SWITCH\n Congratulation you have reached the second part of the experimentation.");
                canvasHandler.textsTranslationLeft();
                stateEntry = true;
            }
            //If the right button is clicked to get to the next page
            else if (canvasHandler.rightClicked)
            {
                state=8;
                transitionState = true;
                canvasHandler.setText(1,"Second TESTING PROCEDURE ("+(testIndex +1)+"/"+nbTest+" iteration)\n You will soon be shown a scene with a robot looking somewhere.\n Your goal will be to: \n    -Place yourself where the robot is looking at using the slider.\n    -Submit your response.\n   -Observe carefully the robot moving its neck and eyes and select the object you think the robot is looking at.");
                canvasHandler.textsTranslationRight();
                stateEntry = true;
            }
        }
        
        //Explanation of the second procedure
        else if (state==8)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(true,true);
                stateEntry = false;
            }
            //If the left button is clicked to get to the previous page
            else if (canvasHandler.leftClicked)
            {
                state=7;
                transitionState = true;
                canvasHandler.textsTranslationLeft();
                stateEntry = true;
            }
            //If the right button is clicked to get to the next page
            else if (canvasHandler.rightClicked)
            {
                headHandler.smoothHeadMovement(order1[testIndex*2]/eyesHandler.nbEyesState);
                eyesHandler.smoothEyesMovement(order1[testIndex*2]%eyesHandler.nbEyesState);
                state=9;
                transitionState = true;
                canvasHandler.mainCanvasTranslationOut();
                stateEntry = true;
            }
        }


        //First part of second procedure test state
        else if (state==9)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                sliderCanvasHandler.sliderCanvasTransitionIn();
                transitionState = true;
                cameraHandler.setCameraMovementEnabled(true);
                stateEntry = false;
            }
            else if (sliderCanvasHandler.submitButtonPressed)
            {
                state = 10;
                sliderCanvasHandler.sliderCanvasTransitionOut();
                headHandler.smoothHeadMovement(order1[testIndex*2+1]/eyesHandler.nbEyesState);
                eyesHandler.smoothEyesMovement(order1[testIndex*2+1]%eyesHandler.nbEyesState);
                transitionState = true;
                stateEntry = true;
            }
        }


        //Second part of second procedure test state
        else if (state==10)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                objectInstructionHandler.setText("|2nd PART|\n\n The robot is now looking elsewhere. Select the object you think the robot is now looking at and submit your answer.");
                objectCanvasHandler.objectCanvasTransitionIn();
                transitionState = true;
                cameraHandler.setCameraMovementEnabled(true);
                stateEntry = false;
            }
            else if (objectCanvasHandler.submitButtonPressed)
            {
                testIndex++;
                if (testIndex < nbTest)
                {
                    state = 11;
                }
                else
                {
                    state = 12;
                }
                objectCanvasHandler.objectCanvasTransitionOut();
                transitionState = true;
                stateEntry = true;
            }
        }


        //Instruction to next iteration of second procedure test
        else if (state==11)
        {
            //Lines of code to be executed at the entry of this state
            if (stateEntry)
            {
                canvasHandler.setButtonsEnabled(false,true);
                canvasHandler.setText(0,"Second TESTING PROCEDURE ("+(testIndex +1)+"/"+nbTest+" iteration)\n You will soon be shown a scene with a robot looking somewhere.\n Your goal will be to: \n    -Place yourself where the robot is looking at using the slider.\n    -Submit your response.\n   -Observe carefully the robot moving its neck and eyes and select the object you think the robot is looking at.");
                canvasHandler.mainCanvasTranslationIn();
                transitionState = true;
                stateEntry = false;
                
            }
            //If the right button is clicked to get to the next page
            else if (canvasHandler.rightClicked)
            {
                state=9;
                transitionState = true;
                canvasHandler.mainCanvasTranslationOut();
                headHandler.smoothHeadMovement(order1[testIndex*2]/eyesHandler.nbEyesState);
                eyesHandler.smoothEyesMovement(order1[testIndex*2]%eyesHandler.nbEyesState);
                stateEntry = true;
            }
        }

        //Final page
        else if (state==12)
        {
            //Lines of code to be executed at the entry of this state
            if(stateEntry)
            {
                stateEntry = false;
                canvasHandler.setButtonsEnabled(false,false);
                canvasHandler.setText(0,"                            		Thank you !\n                        Every tests have been completed !");
            
                canvasHandler.mainCanvasTranslationIn();
                transitionState = true;
            }
            
        }

    }

/// <summary>This method is used to shuffle a list</summary>
/// <param name="order"> This is the list that will be shuffled by the method</param>
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

       
}