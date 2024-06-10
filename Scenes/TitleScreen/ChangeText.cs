using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
public class ChangeText : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI TMProText;
    public string controlFunc = "SecondAbility";
    void Start()
    {
        animator = GetComponent<Animator>(); 
        TMProText = GetComponent<TextMeshProUGUI>();
    }

    public void Text(string text) {
        string finalText = "";
        for (int i = 0; i < text.Length; i++) {
            if (text[i].ToString() == "#") {
                InputActionMap playerActionMap = Player.Inputs.Normal;
                finalText+=playerActionMap.FindAction(controlFunc,false).GetBindingDisplayString(0);
            }
            else {
                finalText+=text[i];
            }
        }
        TMProText.text = finalText;
        animator.Play("Fade", -1, 0.0f);;
    }
}
