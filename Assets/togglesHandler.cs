using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>Handles the toggle buttons that are used to select the object the user thinks the robot is looking
/// at. Only one of them can be activated at a time.
/// Activating a toggle button by clicking on it makes the submit button interactable with.
/// Clicking on an activated toggle button deactivates it.</summary>
public class togglesHandler : MonoBehaviour
{
    
///The <b>GameObject</b> of the submit button.
    public GameObject button;

///Instance of the class handling the submit button.
    public submitButtonObjectHandler submitButtonHandler;

///The <b>Transform</b> component for the current toggle button that is being treated.
    public Transform currentToggle;

///The list of <b>Toggle</b> components attached to the toggle button.
    public List<Toggle> toggles;

///The <b>Transform</b> component associated to the <b>GameObject</b> containing the list of toggle buttons.
    public Transform parent_transform;

///The <b>Transform</b> component associated to each toggle buttons.
    public Transform toggles_transform;

///The index of the last toggle button that has been set to "up".
    public int lastToggledUp;

/// <summary>Start is called before the first frame update</summary>
    void Start()
    {

        //Submit button initialization
        button = GameObject.Find("Bot Subcanvas Button/Submit Button Object");
        submitButtonHandler = button.GetComponent<submitButtonObjectHandler>();
        toggles_transform = GetComponent<Transform>();

        //Loop through every toggle button's Transform component.
        for (int i =0;i<toggles_transform.childCount;i++)
        {
            currentToggle = toggles_transform.GetChild(i);
            //Add each of them to the list
            toggles.Add(currentToggle.GetComponent<Toggle>());
        }

        lastToggledUp = -1;
    }

/// <summary>Update is called once per frame</summary>
    void Update()
    {
        
    }

/// <summary>This method is used to set a toggle button up or down. It is called when a toggle button has been clicked on.
/// If the toggle button selected is already up it will set it back down.</summary>
/// <param name="toggle"> The index of the toggle button that will be switched.<
    public void toggleUp(int toggle)
    {
        //The toggle is activated.
        toggles[toggle].SetIsOnWithoutNotify(true);
        
        //If there was a toggle button up before.
        if (lastToggledUp!=-1)
        {
            //It is set down.
            toggles[lastToggledUp].SetIsOnWithoutNotify(false);
            //If the last toggle button up is the one that needs to be switched
            if (lastToggledUp == toggle)
            {
                //Since there isn't any toggle button up anymore
                lastToggledUp=-1;

                //The submit button isn't interactable anymore
                submitButtonHandler.toggleButton(false);
            }

            //If the toggle button that needs to be switched isn't the one that was up before
            else
            {
                //It gets up.
                lastToggledUp=toggle;
            }
        }
        //If there wasn't any toggle buttons up before.
        else
        {   
            //It gets up.
            lastToggledUp = toggle;
            //The submit button gets activated
            submitButtonHandler.toggleButton(true);
        }
    }

/// <summary>This method is called when the toggle buttons need to appear on the screen. It is used to
/// set the toggle button that was previously up back down.</summary>
    public void appear()
    {   
        //If there was a previous toggle button up...
        if (lastToggledUp!=-1)
        {
            //It is set back down.
            toggles[lastToggledUp].SetIsOnWithoutNotify(false);
            lastToggledUp = -1;
        }
        //The toggle buttons get deactivated until the animation is finished.
        setInteractable(false);
    }

    
/// <summary>This method is called to deactivate or activate the toggle buttons.</summary>
/// <param name="value"> If it is set to true then the toggle buttons will be activated. They will get deactivated
/// otherwise </param>
    public void setInteractable(bool value)
    {
        for (int i=0;i<7;i++)
        {
            toggles[i].interactable = value;
        }
    }

}
