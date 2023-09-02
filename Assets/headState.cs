
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Handles the head's movements during the simulation. </summary>
public class headState : MonoBehaviour
{

/// The transform component of the head
    public Transform headAssembly_transform;

/// Current state of the head, determines the angle of it.
    public int headCurrentState;

/// The current angle of rotation of the head
    public float yAngleHeadCurrent;

/// The desired angle of the head, the rotation instruction.
    public float rotationOrder;

/// A flag indicating whether the head should be moving or not
    public bool onMovement;

/// A flag to allow headmovement 
    public bool headMovementEnabled;

// --- Customizable head parameters --- //

/// The number of possible neck states (angle). Each angle will be equally distant from the other.
    public int nbHeadState;

/// The maximum angle of rotation that the head can achieve.
    public float rotationAmplitude;

/// The rotation speed of the head
    public float rotationSpeed;

/// Start is called before the first frame update
    void Start()
    {

    // Get the transform component of the head assembly
        headAssembly_transform = GetComponent<Transform>();

    // Set the initial angle of the head to zero
        yAngleHeadCurrent = 0;

    // Set the onMovement flag to false (the head is not in motion at the beginning)
        onMovement = false;

    //Set the initial head position.
        headCurrentState = nbHeadState / 2;

    /// --- Customizable head parameters ---

    // Set the number of neck states to 3 (you can change this value)
        nbHeadState = 5;

    // Set the maximum angle of rotation to 90 degrees (you can change this value)
        rotationAmplitude = 90;

    // Set the speed of rotation of the head to 50 degrees per second (you can change this value)
        rotationSpeed = 50f;

    }

/// Update is called once per frame
    void Update()
    {
        
        // If the head is currently in motion, rotate it towards the desired angle
        if (onMovement)
        {
            // If the current angle is less than the desired angle, rotate the head clockwise
            if (rotationOrder - yAngleHeadCurrent >1)
            {
                headAssembly_transform.Rotate(0,rotationSpeed*Time.deltaTime,0,Space.Self);
                yAngleHeadCurrent += rotationSpeed*Time.deltaTime;
            }
            // If the current angle is greater than the desired angle, rotate the head counterclockwise
            else if (rotationOrder - yAngleHeadCurrent<-1)
            {
                headAssembly_transform.Rotate(0,-rotationSpeed*Time.deltaTime,0,Space.Self);
                yAngleHeadCurrent -= rotationSpeed*Time.deltaTime;
            }
            // If the head has reached the desired angle, stop the motion
            else
            {
                onMovement = false;
            }
        }
        
    }

/// <summary> This method allows to change the position of the head by inputting an integer number between 0 and the value of <b> nbHeadState - 1 </b>.
/// Note that inputting <b>0</b> will make the robot turn his head on its right and inputting <b>nbHeadState - 1</b> will make it turn its head on its left.
/// </summary>
/// <param name = "headState"> This value has to be included between 0 and <b>nbHeadState - 1</b> and will determine the angle of rotation of the robot's head.</param>
/// <returns> It returns nothing </returns>
    public void setHeadState(int headState)
    {
        // Check if headState is within valid range
        if (headState < 0 || headState > (nbHeadState - 1))
        {
            print("headState exceeding limits, should be between 0 and " + (nbHeadState - 1) + " but is " + headState);
        }
        // Verify that the eyes' smooth movement is disabled.
        else if (onMovement)
        {
            print("The head is already smoothly moving using the other method.");
        }
        else
        {
        // Calculate the rotation order for the head assembly based on the neck state
            rotationOrder = -((float)headState / (float)(nbHeadState - 1) * rotationAmplitude - rotationAmplitude / 2) % 360;

        // Calculate the amount of rotation needed to move from the current head angle to the desired rotation angle
            float rotation = (rotationOrder - yAngleHeadCurrent) % 360;

        // Update the current head angle to the desired rotation angle
            yAngleHeadCurrent = rotationOrder;

        // Rotate the head assembly by the calculated amount of rotation around the y-axis in local space
            headAssembly_transform.Rotate(0, rotation, 0, Space.Self);
        }
    }

/// <summary> This method allows to change the position of the head by inputting an integer number between 0 and the value of <b> nbHeadState - 1 </b>.
/// The movement will be smooth, therefore it will take time for the head to rotate to its final angle.
/// The method activates the movement of the head and the <b>Update</b> method is operating the smooth movement.
/// Note that inputting <b>0</b> will make the robot turn its head on its right and inputting <b>nbHeadState - 1</b> will make it turn its head on its left.
/// </summary>
/// <param name = "headState"> This value has to be included between 0 and <b>nbHeadState - 1</b> and will determine the angle of rotation of the robot's head.</param>
/// <returns> It returns nothing </returns>
    public bool smoothHeadMovement(int headState)
    {
    // Check if headState is within valid range
        if (headState < 0 || headState > (nbHeadState - 1))
        {
            print("headState exceeding limits, should be between 0 and " + (nbHeadState - 1) + " but is " + headState);
            return false;
        }
        else
        {
        // Calculate the rotation order for the head assembly based on the neck state
            rotationOrder = -((float)headState / (float)(nbHeadState - 1) * rotationAmplitude - rotationAmplitude / 2) % 360;

        // Set the flag for head movement to start
            onMovement = true;
            return true;
        }
    }

/// <summary>
/// This methods allows to enable or disable the head movement.
/// When the head movements are disabled the head won't be able to rotate using any of the two functions <b>setHeadState</b> or <b>smoothHeadMovement</b>, otherwise it will be possible.
/// </summary>
/// <param name = "value">
/// If value is set to true the head movements will be enabled, if set to false they will be disabled.
/// </param>
/// <returns>
/// It returns nothing
/// </returns>
    public void setHeadMovementEnabled(bool value)
    {
        headMovementEnabled = value;
    }
}

