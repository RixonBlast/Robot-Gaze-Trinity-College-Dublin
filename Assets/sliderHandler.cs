using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderHandler : MonoBehaviour
{
    public cameraHandler cameraHandler;

    public Slider slider;

    public Transform camera_transform;

    public float arrowsMotionSpeed;

    // Start is called before the first frame update
    void Start()
    {
        cameraHandler = GameObject.Find("Main Camera").GetComponent<cameraHandler>();

        slider = GetComponent<Slider>();

        camera_transform = GameObject.Find("Main Camera").GetComponent<Transform>();

        arrowsMotionSpeed = 15;

        setSliderEnabled(false);
    }

    // Update is called once per frame
    void Update()
    {   
        
        if (Input.GetKey("left"))
        {
            slider.value -= arrowsMotionSpeed * Time.deltaTime;
        }

        if (Input.GetKey("right"))
        {
            slider.value += arrowsMotionSpeed * Time.deltaTime;
        }
        
    }

    public void OnSliderChange()
    {
        cameraHandler.setCameraAngle(slider.value);
        print("Slider state: "+slider.enabled);
    }

    public void setSliderEnabled(bool value)
    {
        enabled = value;
    }
}
