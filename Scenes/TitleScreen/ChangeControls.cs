using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeControls : MonoBehaviour
{
    public string ButtonFunction = "SecondAbility"; 
    void Awake() {
        if (Player.Inputs == null) {
            Player.Inputs = new PlayerInputs();
        }
    }
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Change() {
        Player.Inputs.Disable();

        InputActionMap playerActionMap = Player.Inputs.Normal;

        InputAction rebindButton  = playerActionMap.FindAction(ButtonFunction, false);
        InputActionRebindingExtensions.RebindingOperation rebindOp = rebindButton.PerformInteractiveRebinding(0);

        Debug.Log(rebindOp);
        
        rebindOp.OnComplete(func => {
            func.Dispose();
        });

        rebindOp.Start();
    }
}
