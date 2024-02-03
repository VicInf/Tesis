using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabInputField : MonoBehaviour
{
    public TMP_InputField Input1;
    public TMP_InputField Input2;
    public TMP_InputField Input3;
    // Start is called before the first frame update
    public int InputSelected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            InputSelected--;
            if(InputSelected < 0) 
            {
                InputSelected = 2;
            }
            SelectedInputField();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if (InputSelected > 2)
            {
                InputSelected = 0;
            }
            SelectedInputField();
        }
    }

    void SelectedInputField()
    {
        switch(InputSelected)
        {
            case 0: Input1.Select();
                break;
            case 1: Input2.Select();
                break;
            case 2: Input3.Select();
                break;
        }
    }
    public void Input1Selected() => InputSelected = 0;
    public void Input2Selected() => InputSelected = 1;
    public void Input3Selected() => InputSelected = 2;
}
