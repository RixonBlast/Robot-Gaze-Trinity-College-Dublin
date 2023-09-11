using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Handles the eyes and pupils movements during the simulation. </summary>
public class eyesHandler : MonoBehaviour
{
/// Material for the eyes
    public Material screenEyes_material;

/// Material for the pupils
    public Material screenPupils_material;

/// Current state of the eyes
    public int eyesCurrentState;

/// Current translation of the eyes
    public float xTranslationScreenCurrent;

/// Order of translation for the eyes
    public float translationOrder;

/// Flag indicating whether the eyes are currently moving or not.
    public bool onMovement;

/// When set on True the eyes can be moved, when on False they can't
    public bool eyesMovementEnabled;

// --- Customizable eyes parameters --- //

/// Number of different eye positions. The pupils position will depend on the eyes position. Each eyes position will be equally distant from the other.
    public int nbEyesState;

/// Amplitude max for the translation of the eyes
    public float translationAmplitude;

/// Speed of translation for the eyes
    public float translationSpeed;

/// Start is called before the first frame update
    void Start()
    {
        // Get the materials of the eyes and pupils
        screenPupils_material = GetComponent<Renderer>().materials[1];
        screenEyes_material = GetComponent<Renderer>().materials[0];

        // Set the initial state of the onMovement flag
        onMovement = false;

        eyesCurrentState = nbEyesState/2;

        setEyesMovementEnabled(false);

    /// --- Customizable eyes parameters --- 

        nbEyesState = 5;
        translationAmplitude = 0.3f;
        translationSpeed = 0.15f;
    }

/// Update is called once per frame
    void Update()
    {

    // If the eyes are considered to be moving
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
        // stop the movement when the eyes have reached their target state
            else
            {
                onMovement = false;
            }
        }
    }

/// <summary> This method allows to change the position of the eyes and the pupils by inputting an integer number between 0 and the value of <b> nbEyesState - 1 </b>.
/// Note that inputting <b>0</b> will make the robot look on its right and inputting <b>nbEyesState - 1</b> will make it look on its left.
/// </summary>
/// <param name = "eyesState"> This value has to be included between 0 and <b>nbEyesState - 1</b> and will determine the position of the eyes on the robot's screen.</param>
/// <returns> It returns nothing </returns>
    public void setEyesState(int eyesState)
    {
    // Verify that eyesState value is within the range of possible states
        if (eyesState < 0 || eyesState > (nbEyesState-1))
        {
            print("eyesState exceeding limits, should be between 0 and "+(nbEyesState-1)+" but is "+eyesState);
        }
    // Verify that the eyes' smooth movement is disabled.
        else if (onMovement)
        {
            print("The eyes are already smoothly moving using the other method.");
        }
        else
        {
    // Calculate the distance of translation required for the eyes to reach the desired state
            translationOrder = -((float)eyesState / (float)(nbEyesState-1) * translationAmplitude - translationAmplitude/2);
            float translation = translationOrder - xTranslationScreenCurrent;
            xTranslationScreenCurrent = translationOrder;

    // Apply the translation to the eyes and pupils textures
            screenEyes_material.mainTextureOffset = new Vector2(translation,0);
            screenPupils_material.mainTextureOffset = new Vector2(translation + translation/3f,0);
        }
    }

/// <summary> This method allows to change the position of the eyes and the pupils by inputting an integer number between 0 and the value of <b> nbEyesState - 1 </b>.
/// The movement will be smooth, therefore it will take time for the eyes to get to their final location on the robot's screen.
/// The method activates the movement of the eyes and the <b>Update</b> method is operating the smooth movement.
/// Note that inputting <b>0</b> will make the robot look on its right and inputting <b>nbEyesState</b> will make it look on its left.
/// </summary>
/// <param name = "eyesState"> This value has to be included between 0 and <b>nbEyesState - 1</b> and will determine the position of the eyes on the robot's screen.</param>
/// <returns> It returns nothing </returns>
    public void smoothEyesMovement(int eyesState)
    {
    // Verify that eyesState value is within the range of possible states
        if (eyesState < 0 || eyesState > (nbEyesState-1))
        {
            print("eyesState exceeding limits, should be between 0 and "+(nbEyesState-1)+" but is "+eyesState);
        }
        else
        {
        // Calculate the distance of translation required for the eyes to reach the desired state
            translationOrder = -((float)eyesState / (float)(nbEyesState-1) * translationAmplitude - translationAmplitude/2);
            onMovement = true;
        }
    }

/// <summary>
/// This methods allows to enable or disable the eyes movement.
/// When the eyes movements are disabled the eyes won't be able to move using any of the two functions <b>setEyesState</b> or <b>smoothEyesMovement</b>, otherwise it will be possible.
/// </summary>
/// <param name = "value">
/// If value is set to true the eyes' movements will be enabled, if set to false they will be disabled.
/// </param>
/// <returns>
/// It returns nothing
/// </returns>
    public void setEyesMovementEnabled(bool value)
    {
        eyesMovementEnabled = value;
    }
}
