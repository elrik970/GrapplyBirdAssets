using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ControlsText : MonoBehaviour
{
    // Start is called before the first frame update
    public string text = "SecondAbility: ";
    public TextMeshProUGUI TMProText;
    public string controlFunc = "SecondAbility";
    void Start()
    {
        TMProText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        InputActionMap playerActionMap = Player.Inputs.Normal;
        string button = playerActionMap.FindAction(controlFunc,false).GetBindingDisplayString(0);
        TMProText.text = text + button;
    }
}
