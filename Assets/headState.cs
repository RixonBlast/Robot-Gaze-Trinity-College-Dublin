using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headState : MonoBehaviour
{
    // The transform component of the head assembly
    public Transform headAssembly_transform;
    
    // The number of possible neck states (positions)
    public int nbNeckState;

    // The maximum angle of rotation that the head can achieve
    public float rotationAmplitude;

    // The current angle of the head
    public float yAngleHeadCurrent;

    // The desired angle of the head (based on the chosen neck state)
    public float rotationOrder;

    // A flag indicating whether the head is currently in motion
    public bool onMovement;

    // The speed of rotation of the head
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Get the transform component of the head assembly
        headAssembly_transform = GetComponent<Transform>();

        // Set the initial angle of the head to zero
        yAngleHeadCurrent = 0;

        // Set the number of neck states to 3 (you can change this value)
        nbNeckState = 3;

        // Set the maximum angle of rotation to 90 degrees (you can change this value)
        rotationAmplitude = 90;

        // Set the onMovement flag to false (the head is not in motion at the beginning)
        onMovement = false;

        // Set the speed of rotation of the head to 50 degrees per second (you can change this value)
        rotationSpeed = 50f;

        // Uncomment the following line if you want to start with a specific neck state
        //smoothHeadMovement(2);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the user has pressed a key corresponding to a specific neck state
        /*
        if (Input.GetKeyDown("6"))
        {
            smoothHeadMovement(0);
        }
        else if (Input.GetKeyDown("7"))
        {
            smoothHeadMovement(1);
        }
        else if (Input.GetKeyDown("8"))
        {
            smoothHeadMovement(2);
        }
        */

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
                print("End of movement!!");
            }
        }
    }

    public void setHeadState(int neckState)
    {
        // Check if neckState is within valid range
        if (neckState < 0 || neckState > (nbNeckState - 1))
        {
            print("neckState exceeding limits, should be between 0 and " + (nbNeckState - 1) + " but is " + neckState);
        }
        else
        {
            // Calculate the rotation order for the head assembly based on the neck state
            rotationOrder = -((float)neckState / (float)(nbNeckState - 1) * rotationAmplitude - rotationAmplitude / 2) % 360;

            // Calculate the amount of rotation needed to move from the current head angle to the desired rotation angle
            float rotation = (rotationOrder - yAngleHeadCurrent) % 360;

            // Update the current head angle to the desired rotation angle
            yAngleHeadCurrent = rotationOrder;

            // Rotate the head assembly by the calculated amount of rotation around the y-axis in local space
            headAssembly_transform.Rotate(0, rotation, 0, Space.Self);
        }
    }

    void smoothHeadMovement(int neckState)
    {
        // Check if neckState is within valid range
        if (neckState < 0 || neckState > (nbNeckState - 1))
        {
            print("neckState exceeding limits, should be between 0 and " + (nbNeckState - 1) + " but is " + neckState);
        }
        else
        {
            // Calculate the rotation order for the head assembly based on the neck state
            rotationOrder = -((float)neckState / (float)(nbNeckState - 1) * rotationAmplitude - rotationAmplitude / 2) % 360;

            // Set the flag for head movement to start
            onMovement = true;
        }
    }
}
