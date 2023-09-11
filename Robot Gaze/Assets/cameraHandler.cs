using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Handles the camera's initial position and movements during the simulation. </summary>
public class cameraHandler : MonoBehaviour
{

/// Reference to the Robot GameObject
    private GameObject robot;

/// Reference to the gameManager GameObject
    private GameObject gameManager;

/// Camera's transform
    private Transform camera_transform;

/// Robot's transform 
    private Transform robot_transform;

/// Robot's Collider
    private Collider robot_boxCollider;

/// Current angle between the camera's position and the robot's vertical axis. Its value varies between 0 and 180.
    private float currentCameraAngle;

/// If its value is false then the camera won't be able to move using any of the two functions, otherwise it will be possible to do so.
    private bool cameraEnable;

// --- Customizable camera parameters declaration --- //

/// Distance between the camera and the Robot's GameObject
    private float cameraDistance;

/// Angle in degrees for the position of the camera. It is the angle formed between the line connecting the robot's head and the camera, and the line formed by the direction of the robot's gaze.
    private float cameraPositionAngle;

/// Angle in degrees for the orientation of the camera. It is the angle formed between the line connecting the robot's head and the camera, and the line formed by the direction of the camera's orientation.
    private float cameraOrientationAngle;

/// Amplitude for the camera's rotation around the robot in degrees.
    private float angleViewAmplitude;

/// Number of different angles for the camera that will be considered when using the function setCameraState. For instance, if its value is 5, the camera can be positioned at 5 equidistant locations across the arc of the circle of angleViewAmplitude degrees that is facing the robot.
    private float nbCameraState;


/// Start is called before the first frame update
    void Start()
    {

    // Getting GameObjects

        robot = GameObject.Find("Robot_stevie");
        gameManager = GameObject.Find("Game Manager");

    // Getting Transforms

        camera_transform = GetComponent<Transform>();
        robot_transform = robot.GetComponent<Transform>();

    // Getting Collider
        robot_boxCollider = robot.GetComponent<BoxCollider>();

    /// --- Customizable camera parameters instantiation --- 
     
        cameraDistance = 5f;
        cameraPositionAngle = 15f;
        cameraOrientationAngle = 10f;
        angleViewAmplitude = 180f;
        nbCameraState = 5;

    // Initializing the position and orientation of the camera.
        
        cameraEnable = false;
        
        currentCameraAngle = 90;

        camera_transform.localPosition = robot_transform.localPosition + new Vector3(0,robot_boxCollider.bounds.size.z*2,-cameraDistance);
        camera_transform.RotateAround(robot_transform.position+new Vector3(0,robot_boxCollider.bounds.size.z*2,0),robot_transform.right,-cameraPositionAngle);
        camera_transform.Rotate(new Vector3(cameraOrientationAngle,0f,0f),Space.Self);


                                                                                                                                                                                                                                                                    
    }

    /// Update is called once per frame
    void Update()
    {    

    }

/// <summary> This method allows to change the angle of the position of the camera only by inputting an integer number between 0 and the value of <b> nbCameraState - 1 </b>.
/// </summary>
/// <remarks>
/// For example if the value of <b>nbCameraState</b>  is 5 and the value of <b>angleViewAmplitude</b> is 180 then the camera will be positioned the following way depending on the input of the method:
/// | <b>cameraState</b> 's value | angle of position of the camera in degrees |
/// |---------|---------|
/// | 0 | 0  |
/// | 1 | 45 |
/// | 2 | 90 |
/// | 3 | 135 |
/// | 4 | 180 |
/// Note that when the angle of position of the camera is 0 degrees then it will be facing the right side of the robot, when its value is 180 degrees it will be facing the left side of the robot.
/// </remarks>
/// <param name = "cameraState"> This value has to be included between 0 and <b>nbCameraState - 1</b> and will determine the camera's position around the robot.</param>
/// <returns> It returns nothing </returns>
    public void setCameraState(int cameraState)
    {
    // Check if the camera is enabled
        if (cameraEnable)
        {
        //Verify that the camera state is within the limits
            if (cameraState < 0 || cameraState > (nbCameraState-1))
            {
            // Print an error message if the camera state is outside of the limits
                Debug.Log("cameraState exceeding limits, should be between 0 and "+(nbCameraState-1)+" but is "+cameraState);
            }
            else
            {
            // Calculate the rotation order based on the current camera state and the angle of view amplitude
                float rotationOrder = -((float)cameraState / (float)(nbCameraState-1) * angleViewAmplitude - angleViewAmplitude/2) %360;
            // Calculate the rotation needed based on the current and desired camera states
                float rotation = (rotationOrder - currentCameraAngle) %360;
            // Update the current y angle of the camera
                currentCameraAngle = rotationOrder;
            // Rotate the camera around the robot using the calculated rotation
                camera_transform.RotateAround(robot_transform.position,robot_transform.up,rotation);
            } 
        }
    }

/// <summary>
/// This methods allows to enable or disable the camera movement.
/// When the camera movements are disabled the camera won't be able to move using any of the two functions <b>setCameraAngle</b> or <b>setCameraState</b>, otherwise it will be possible.
/// </summary>
/// <param name = "value">
/// If value is set to true the camera's movements will be enabled, if not they will be disabled.
/// </param>
/// <returns>
/// It returns nothing
/// </returns>
    public void setCameraMovementEnabled(bool value)
    {
        cameraEnable = value;
    }

/// <summary>
/// This method allows to change the angle of the position of the camera by inputting the said angle in degrees.
/// </summary>
/// <remarks>
/// The angle can vary from 0 degrees to the value of <b>angleViewAmplitude</b> , 0 when the camera is facing the right side of the robot and the value of <b>angleViewAmplitude</b> when it is facing the left side.
/// </remarks>
/// <param name = "angle">
/// The angle, in degrees, desired for the position of the camera.
/// </param>
/// <returns>
/// It returns nothing
/// </return>
    public void setCameraAngle(float angle)
    {
    // Verify that the camera is enabled
    if (cameraEnable)
    {
        //Print an error message if the angle is exceeding the limits
            if (angle < 0 || angle > angleViewAmplitude)
            {
                Debug.Log("angle value exceeding limits, should be between 0 and "+(angleViewAmplitude)+" but is"+(angle));
            }
            else
            {
            // Rotating the camera
                camera_transform.RotateAround(robot_transform.position,robot_transform.up,-(angle-currentCameraAngle));
            // Updating the value of the current camera angle of position.
                currentCameraAngle = angle;
            }
    
        }
    }
}