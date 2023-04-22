using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraState : MonoBehaviour
{
    // Reference to the robot GameObject
    public GameObject robot;

    // Reference to the camera and robot transforms
    public Transform camera_transform;
    public Transform robot_transform;

    // Vector3 for the y axis
    Vector3 yAxis;
    Vector3 robotSize;

    // Number of camera states
    public int nbCameraState;

    // Test integer variable
    public int test;

    // Amplitude of the angle of view of the camera
    public float angleViewAmplitude;
    // Current y angle of the camera
    public float yAngleCameraCurrent;
    public float cameraDistance;

    // Start is called before the first frame update
    void Start()
    {
        // Find the robot GameObject
        robot = GameObject.Find("Robot_stevie");

        // Get the camera and robot transforms
        camera_transform = GetComponent<Transform>();
        robot_transform = robot.GetComponent<Transform>();

        // Initialize the yAxis vector
        yAxis = new Vector3(0,1,0);
        robotSize = robot.GetComponent<Collider>().bounds.size;

        // Initialize the number of camera states and the angle of view amplitude
        nbCameraState = 37;
        angleViewAmplitude = 180;

        cameraDistance = 13;

        // Initialize the current y angle of the camera
        yAngleCameraCurrent = 0;

        // Initialize the test variable
        test=18;

        camera_transform.Translate((robot_transform.forward)*cameraDistance+robot_transform.position-camera_transform.position + new Vector3(0,robotSize.y,0),Space.World);


    }

    // Update is called once per frame
    void Update()
    {     
        // Call the cameraMovement function with an argument of 2 when the test variable is 1
        if (Input.GetKeyDown("right")){
            test+=1;
            print(test);
            cameraMovement(test);
        }
        if (Input.GetKeyDown("left")){
            test-=1;
            print(test);
            cameraMovement(test);
        }
    }

    // Function to move the camera to a specified camera state
    public void cameraMovement(int cameraState)
    {
        // Check if the camera state is within the limits
        if (cameraState < 0 || cameraState > (nbCameraState-1))
        {
            // Print an error message if the camera state is outside of the limits
            print("cameraState exceeding limits, should be between 0 and "+(nbCameraState-1)+" but is "+cameraState);
        }
        else
        {
            // Calculate the rotation order based on the current camera state and the angle of view amplitude
            float rotationOrder = -((float)cameraState / (float)(nbCameraState-1) * angleViewAmplitude - angleViewAmplitude/2) %360;
            // Calculate the rotation needed based on the current and desired camera states
            float rotation = (rotationOrder - yAngleCameraCurrent) %360;
            // Update the current y angle of the camera
            yAngleCameraCurrent = rotationOrder;
            // Rotate the camera around the robot using the calculated rotation
            camera_transform.RotateAround(robot_transform.position,yAxis,rotation);
        }
    }
}