using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyesState : MonoBehaviour
{
    // Materials for the eyes and pupils
    public Material screenEyes_material;
    public Material screenPupils_material;

    // Number of different eye states
    public int nbEyesState;

    // Translation amplitude of the eyes
    public float translationAmplitude;

    // Current translation of the eyes
    public float xTranslationScreenCurrent;

    // Order of translation for the eyes
    public float translationOrder;

    // Speed of translation for the eyes
    public float translationSpeed;

    // Flag indicating whether the eyes are currently moving
    public bool onMovement;

    // Start is called before the first frame update
    void Start()
    {
        // Get the materials of the eyes and pupils
        screenPupils_material = GetComponent<Renderer>().materials[1];
        screenEyes_material = GetComponent<Renderer>().materials[0];

        // Set the number of eye states
        nbEyesState = 5;

        // Set the translation amplitude of the eyes
        translationAmplitude = 0.3f;

        // Set the initial state of the onMovement flag
        onMovement = false;

        // Set the speed of translation for the eyes
        translationSpeed = 0.15f;

        // Set the initial eye state
        // setEyesState(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            smoothEyesMovement(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            smoothEyesMovement(1);
        }
        else if (Input.GetKeyDown("3"))
        {
            smoothEyesMovement(2);
        }
        else if (Input.GetKeyDown("4"))
        {
            smoothEyesMovement(3);
        }
        else if (Input.GetKeyDown("5"))
        {
            smoothEyesMovement(4);
        }

        if (onMovement)
        {
            // update the eyes' textures to create a smooth movement effect
            if (translationOrder - xTranslationScreenCurrent >0.01)
            {
                screenEyes_material.mainTextureOffset = new Vector2(xTranslationScreenCurrent,0);
                screenPupils_material.mainTextureOffset = new Vector2(xTranslationScreenCurrent+xTranslationScreenCurrent/3f,0);
                xTranslationScreenCurrent += translationSpeed*Time.deltaTime;
            }
            else if (translationOrder - xTranslationScreenCurrent<-0.01)
            {
                screenEyes_material.mainTextureOffset = new Vector2(xTranslationScreenCurrent,0);
                screenPupils_material.mainTextureOffset = new Vector2(xTranslationScreenCurrent+xTranslationScreenCurrent/3f,0);
                xTranslationScreenCurrent -= translationSpeed*Time.deltaTime;
            }
            else
            {
                // stop the movement when the eyes have reached their target state
                onMovement = false;
                print("End of movement!!");
            }
        }
    }

    public void setEyesState(int eyesState)
    {
        // Check if eyesState value is within the range of possible states
        if (eyesState < 0 || eyesState > (nbEyesState-1))
        {
            print("eyesState exceeding limits, should be between 0 and "+(nbEyesState-1)+" but is "+eyesState);
        }
        else
        {
            // Calculate the amount of translation required for the eyes to reach the desired state
            translationOrder = -((float)eyesState / (float)(nbEyesState-1) * translationAmplitude - translationAmplitude/2);
            float translation = translationOrder - xTranslationScreenCurrent;
            xTranslationScreenCurrent = translationOrder;

            // Apply the translation to the eyes and pupils textures
            screenEyes_material.mainTextureOffset = new Vector2(translation,0);
            screenPupils_material.mainTextureOffset = new Vector2(translation + translation/3f,0);
        }
    }

    void smoothEyesMovement(int eyesState)
    {
        // Check if eyesState value is within the range of possible states
        if (eyesState < 0 || eyesState > (nbEyesState-1))
        {
            print("eyesState exceeding limits, should be between 0 and "+(nbEyesState-1)+" but is "+eyesState);
        }
        else
        {
            // Calculate the amount of translation required for the eyes to reach the desired state
            translationOrder = -((float)eyesState / (float)(nbEyesState-1) * translationAmplitude - translationAmplitude/2);
            onMovement = true;
        }
    }
}
