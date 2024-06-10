using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New FallingState", menuName = "States/Player/FallingState")]
public class FallingState : PlayerState<Player>
{
    private PlayerInputs Inputs;
    public PlayerState<Player> GrappleState;
    public PlayerState<Player> IdleState;
    public float GravityScale = 3f;

    // public Color movementColor;
    public float Accel;
    public float DeAccelRate;
    public float maxSpeed;

    public float JumpForce;

    // public PlayerState<Player> FallingState;
    private Camera Camera;

    public override void Init(Player parent) {
        base.Init(parent);
        Camera = Camera.main;
        Inputs = Player.Inputs;
        Inputs.Normal.Grapple.performed+=OnGrapple;
        Inputs.Normal.Jump.performed += OnJump;
        rb.gravityScale = GravityScale;
    }
    // Unity Update
    public override void ConstantUpdate() {

    }
    // Unity Update
    public override void ChangeState() {
        if (player.OnGroundOverride(rb.velocity.normalized,0)) {
            runner.SetState(IdleState);
        }
    }
    // Unity Update
    public override void CaptureInputs() {
        
    }
    // Unity FixedUpdate
    public override void PhysicsUpdate() {
        if (Input.GetKey(KeyCode.D)) {
            // Debug.Log("RUNNING");
            player.MoveRight(Accel,maxSpeed);
            player.Color(player.movementColor);
        }
        else if (Input.GetKey(KeyCode.A)) {
            // Debug.Log("RUNNING");
            player.MoveLeft(Accel,maxSpeed);
            player.Color(player.movementColor);
        }
        else {
            player.Color(player.defaultColor);
            player.Still(DeAccelRate);
        }
    }
    public override void Exit() {
        Inputs.Normal.Grapple.performed-=OnGrapple;
        Inputs.Normal.Jump.performed -= OnJump;
        player.Color(player.defaultColor);
    }
    private void DeAccel(Vector3 Speed) {
    }
    void OnGrapple(InputAction.CallbackContext context) {
        if (player.GrapplingHookCheck()) {
            runner.SetState(GrappleState);
        }
    }
    void OnJump(InputAction.CallbackContext context) {
        player.Jump();
    }
}
