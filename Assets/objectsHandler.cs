using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


///<summary>
///Handles the 7 different objects to which the robot will be looking at.
///Each object is designed as a cube composed of 6 faces that individually colored at the start of the simulation.</summary>
public class objectsHandler : MonoBehaviour
{



///The <b>GameObject</b> component of the robot.
    public GameObject robot;

///The <b>Transform</b> component of the robot.
    public Transform robot_transform;

///The <b>Transform</b> component of the specific object that is being placed.
public Transform currentFace_transform;

///The <b>Transform</b> component of the <b>GameObject</b> containing the list of the 7 objects.
    public Transform parent_transform;

///The <b>Material</b> component of the current face of the current object that is being colored.
    public Material currentFace_material;
    
///The amplitude for the angles of the positions of the objects relatively to the robot.
    float angleAmplitude;

///The distance between the objects and the robot
    float objectDistance;

    /// Start is called before the first frame update
    void Start()
    {

        Transform currentObject_transform;
        //Objects initialization
        robot = GameObject.Find("Robot_stevie");

        //Transform initialization
        robot_transform = robot.GetComponent<Transform>();
        parent_transform = GetComponent<Transform>();

        /// --- Customizable camera parameters instantiation --- 
        angleAmplitude = 120;
        objectDistance = 3;
        
        //Loop through each object.
        for (int i =0;i<parent_transform.childCount;i++)
        {
            //Getting the object's transform that's containing the list of faces.
             currentObject_transform = parent_transform.GetChild(i).GetChild(0);
             
             //Loop through the faces
            for (int j=0;j<currentObject_transform.childCount;j++)
            {
                //Coloring each face.
                currentFace_transform = currentObject_transform.GetChild(j);
                currentFace_material = currentFace_transform.GetComponent<Renderer>().material;
                currentFace_material.SetColor("_Color",Color.HSVToRGB((float)i/(float)parent_transform.childCount,1,1));
            }
            //Placing each object.
            parent_transform.localPosition = new Vector3(0,1.5f,-objectDistance);
            currentObject_transform.RotateAround(robot_transform.position,robot_transform.up,i*angleAmplitude/(parent_transform.childCount-1) - angleAmplitude/2);
        }

    }

    /// Update is called once per frame
    void Update()
    {
        
    }
}
