using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerState<T> : ScriptableObject where T : MonoBehaviour
{
    public StateManager runner;
    public Player player;
    public Rigidbody2D rb;
    
    public virtual void Init(T parent) {
        player = parent.GetComponent<Player>();
        runner = parent.GetComponent<StateManager>();
        rb = parent.GetComponent<Rigidbody2D>();
    }

    public abstract void ConstantUpdate();
    public abstract void CaptureInputs();
    public abstract void ChangeState();
    public abstract void PhysicsUpdate();
    public abstract void Exit();
    
}
