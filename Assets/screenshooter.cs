using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshooter : MonoBehaviour
{
    public Transform parent_transform;

    public Transform currentObject_transform;

    public Vector3 tmpShift;

    public int count;

    // Start is called before the first frame update
    void Start()
    {
        parent_transform = GetComponent<Transform>();

        tmpShift = new Vector3(100,0,0);

        count = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (count< 2)
        {
            count++;
        }
        else if (count == 2)
        {
            currentObject_transform = parent_transform.GetChild(count-2);
            currentObject_transform.Translate(tmpShift,Space.World);
            count++;
        }
        else if (count < 9)
        {
            currentObject_transform.GetChild(1).GetComponent<Camera>().enabled = false;
            currentObject_transform.Translate(-tmpShift,Space.World);
            currentObject_transform = parent_transform.GetChild(count-2);
            currentObject_transform.Translate(tmpShift,Space.World);
            count++;
        }
        else if (count == 9)
        {
            currentObject_transform.GetChild(1).GetComponent<Camera>().enabled = false;
            currentObject_transform.Translate(-tmpShift,Space.World);
            count++;
        }
        
    }
}
