using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Handles the slider that the user will have to use to place himself where he feels looked at by
/// the robot.</summary>
public class sliderHandler : MonoBehaviour
{

/// Instance of the class handling the camera.
    public cameraHandler cameraHandler;

///The <b>Slider</b> component used to create the slider.
    public Slider slider;

/// The <b>Transform</b> component used to position the camera.
    public Transform camera_transform;

/// The speed at which the camera will be moving from pressing the keys
    public float arrowsMotionSpeed;

    /// <summary> Start is called before the first frame update</summary>
    void Start()
    {
        
        cameraHandler = GameObject.Find("Main Camera").GetComponent<cameraHandler>();

        slider = GetComponent<Slider>(); 

        camera_transform = GameObject.Find("Main Camera").GetComponent<Transform>();

        /// --- Customizable camera parameters instantiation --- 
        arrowsMotionSpeed = 15;

        //Set to false by default. If it is set to true then the camera will be able to move using the arrow keys.
        setSliderEnabled(false);
    }

    /// <summary>Update is called once per frame</summary>
    void Update()
    {   
        //When the left arrow key is pressed
        if (Input.GetKey("left"))
        {
            slider.value -= arrowsMotionSpeed * Time.deltaTime;
        }

        //When the right arrow key is pressed
        if (Input.GetKey("right"))
        {
            slider.value += arrowsMotionSpeed * Time.deltaTime;
        }
        
    }

/// <summary>This method is called whenever the value of the slider changes and it moves the camera depending on this value.
/// </summary>
    public void OnSliderChange()
    {
        cameraHandler.setCameraAngle(slider.value);
    }

/// <summary>This method is called to allow or prevent the arrow keys to move the slider.</summary>
/// <param name="value"> When it is set to <b>true</b> it allows the arrow keys to move the slider.
/// When it is set to <b>false</b> it prevents them to do so. </param>
    public void setSliderEnabled(bool value)
    {
        enabled = value;
    }
}
