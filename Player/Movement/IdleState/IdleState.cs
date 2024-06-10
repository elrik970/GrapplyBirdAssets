using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New IdleState", menuName = "States/Player/IdleState")]
public class IdleState : PlayerState<Player>
{
    private PlayerInputs Inputs;
    public PlayerState<Player> GrappleState;
    public PlayerState<Player> RisingState;
    public PlayerState<Player> FallingState;
    public float GravityScale;
    public float Drag;
    public float VelocityDivider = 2;
    private Camera Camera;
    public ConstantForce2D ConstantForce;

    // public Color movementColor;
    public float Accel;
    public float maxSpeed;
    public float JumpForce;

    public override void Init(Player parent) {
        base.Init(parent);

        Inputs = Player.Inputs;
        Inputs.Normal.Grapple.performed+=OnGrapple;
        Inputs.Normal.Jump.performed += OnJump;
        Inputs.Normal.SecondAbility.performed += OnAbility;

        if (!player.OnGround()) {
            runner.SetState(RisingState);
            return;
        }
        // Debug.Log(player.OnGroundOverride(Vector2.zero,0).point);
        
        // StickSpot = player.OnGroundOverride(Vector2.zero,0).point;

        

        rb.gravityScale = GravityScale;
        rb.velocity = rb.velocity/VelocityDivider;
        // rb.freezeRotation = true;
        ConstantForce = player.GetComponent<ConstantForce2D>();
        
        
        
        
    }
    // Unity Update
    public override void ConstantUpdate() {
        if (!player.OnGround()) {
            runner.SetState(RisingState);
        }
    }
    // Unity Update
    public override void ChangeState() {
        
    }
    // Unity Update
    public override void CaptureInputs() {
        
    }
    // Unity FixedUpdate
    public override void PhysicsUpdate() {
        if (Input.GetKey(KeyCode.D)) {
            player.MoveRight(Accel,maxSpeed);
            player.Color(player.movementColor);
        }
        else if (Input.GetKey(KeyCode.A)) {
            player.MoveLeft(Accel,maxSpeed);
            player.Color(player.movementColor);
        }
        else {
            player.Color(player.defaultColor);
        }
    }
    public override void Exit() {
        Inputs.Normal.Grapple.performed-=OnGrapple;
        Inputs.Normal.Jump.performed -= OnJump;
        rb.drag = 0f;
        player.GetComponent<ConstantForce2D>().force = Vector2.zero;
        player.Color(player.defaultColor);
        
    }
    private void DeAccel(Vector3 Speed) {
    }
    void OnGrapple(InputAction.CallbackContext context) {
        if (player.GrapplingHookCheck()) {
            runner.SetState(GrappleState);
        }
    }

    void OnAbility(InputAction.CallbackContext context) {
        if (player.secondAbility != null&&player.secondAbilityCoolDown()) {
            runner.SetState(player.secondAbility);
        }
    }

    void OnJump(InputAction.CallbackContext context) {
        player.Jump();
    }
}
