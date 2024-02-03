using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogInTabInputField : MonoBehaviour
{
    public TMP_InputField Input1;
    public TMP_InputField Input2;

    // Start is called before the first frame update
    public int InputSelected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            InputSelected--;
            if (InputSelected < 0)
            {
                InputSelected = 1;
            }
            SelectedInputField();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputSelected++;
            if (InputSelected > 1)
            {
                InputSelected = 0;
            }
            SelectedInputField();
        }
    }

    void SelectedInputField()
    {
        switch (InputSelected)
        {
            case 0:
                Input1.Select();
                break;
            case 1:
                Input2.Select();
                break;
        }
    }
    public void Input1Selected() => InputSelected = 0;
    public void Input2Selected() => InputSelected = 1;
}
