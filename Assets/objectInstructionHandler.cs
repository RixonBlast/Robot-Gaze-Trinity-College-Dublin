using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objectInstructionHandler : MonoBehaviour
{

    public TextMeshProUGUI instruction;

    // Start is called before the first frame update
    void Start()
    {
        instruction = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText(string text)
    {
        instruction.text = text;
    }
}
