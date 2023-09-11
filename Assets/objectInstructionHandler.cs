using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the instructions written on the <b>Object Instruction</b> <b>GameObject</b> associated to the
/// object selection canvas.
/// It allows to set the text written in it.</summary>
public class objectInstructionHandler : MonoBehaviour
{
///The TextMeshProUGUI component for the instruction area, used to edit the text.
    public TextMeshProUGUI instruction;

    /// Start is called before the first frame update
    void Start()
    {
        
        instruction = GetComponent<TextMeshProUGUI>();
    }

    /// Update is called once per frame
    void Update()
    {
        
    }

/// <summary>This method can be used to change the text written in the instruction area of the <b>Object Instruction</b> <b>GameObject</b>.</summary>
/// <parameter name="text"> string containing the text that will be displayed in the instruction area.</param>
    public void setText(string text)
    {
        instruction.text = text;
    }
}
