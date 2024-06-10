using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDropDown : MonoBehaviour
{
    public GameObject[] buttons;
    void Start()
    {
        foreach (GameObject button in buttons) {
            button.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOff() {
        foreach (GameObject button in buttons) {
            button.SetActive(!button.activeSelf);
        }
    }
}
