using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public List<GameObject> objectList;

    public GameObject currentObject;
    public GameObject robot;

    public Transform currentObject_transform;
    public Transform robot_transform;

    Vector3 yAxis;
    Vector3 robotSize;
    public float angleAmplitude;
    public float objectDistance;

    public int objectNb;

    // Start is called before the first frame update
    void Start()
    {

        robot = GameObject.Find("Robot_stevie");

        robot_transform = robot.GetComponent<Transform>();

        robotSize = robot.GetComponent<Collider>().bounds.size;

        yAxis = new Vector3(0,1,0);
        int objectNb = 0;

        angleAmplitude = 120;
        objectDistance = 5;


        objectList = new List<GameObject>();

        while (currentObject = GameObject.Find("object"+objectNb))
        {
            objectList.Add(currentObject);
            objectNb += 1;
        }
        
        //print("Object list: "+objectList[0]+" AND "+objectList[1]);
        for (int i =0;i<objectNb;i++)
        {
            currentObject_transform = objectList[i].GetComponent<Transform>();

            currentObject_transform.Translate((robot_transform.forward)*objectDistance+robot_transform.position-currentObject_transform.position + new Vector3(0,robotSize.y,0),Space.World);

            currentObject_transform.RotateAround(robot_transform.position,yAxis,i*angleAmplitude/(objectNb-1) - angleAmplitude/2);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
