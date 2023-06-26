using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHandler : MonoBehaviour
{
    // Reference to the robot GameObject
    public GameObject robot;

    public GameObject gameManager;

    // Reference to the camera and robot transforms
    public Transform camera_transform;
    public Transform robot_transform;

    // Vector3 for the y axis
    Vector3 humanSize;

    // Number of camera states
    public int nbCameraState;

    // Test integer variable
    public int test;

    // Amplitude of the angle of view of the camera
    public float angleViewAmplitude;
    // Current y angle of the camera
    public float yAngleCameraCurrent;
    public float cameraDistance;

    public float rotationSpeed;

    public float currentCameraAngle;

    public bool cameraMovementEnabled;
    // Start is called before the first frame update
    void Start()
    {
        // Find the robot GameObject
        robot = GameObject.Find("Robot_stevie");
        gameManager = GameObject.Find("Game Manager");

        // Get the camera and robot transforms
        camera_transform = GetComponent<Transform>();
        robot_transform = robot.GetComponent<Transform>();

        // Initialize the yAxis vector
        humanSize = new Vector3(0,6.9f,0);

        // Initialize the number of camera states and the angle of view amplitude
        nbCameraState = 37;
        angleViewAmplitude = 180;

        cameraDistance = 13;

        // Initialize the current y angle of the camera
        yAngleCameraCurrent = 0;



        rotationSpeed = 100;

        // Initialize the test variable
        test=18;

        currentCameraAngle = 90;

        camera_transform.Translate((robot_transform.forward)*cameraDistance+robot_transform.position-camera_transform.position + humanSize,Space.World);
        print(robot.GetComponent<Collider>().bounds.size);
        setCameraMovementEnabled(false) ;                                                                                                                                                                                                                                                                  
    }

    // Update is called once per frame
    void Update()
    {   
        
       

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
            camera_transform.RotateAround(robot_transform.position,robot_transform.up,rotation);
        }
    }

    public void setCameraAngle(float angle)
    {
        camera_transform.RotateAround(robot_transform.position,robot_transform.up,-(angle-currentCameraAngle));
        currentCameraAngle = angle;
        print("CAMERA TOURNE");
    }

    public void setCameraMovementEnabled(bool value)
    {
        cameraMovementEnabled = value;
    }
}