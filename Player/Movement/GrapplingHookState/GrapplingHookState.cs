using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New GrapplingHookState", menuName = "States/Player/GrapplingHookState")]
public class GrapplingHookState : PlayerState<Player>
{
    private PlayerInputs Inputs;
    public PlayerState<Player> IdleState;
    private Camera Camera;
    public GameObject GrapplingHookPrefab;
    private GameObject GrapplingHookObject;
    public float TimeBeforeCanLatch;
    public float GravityScale = 3f;
    private float timePassed;
    public float Drag;
    public float DistanceBeforeLatch;
    public float TimeToFullyHook;
    private float hookedTime;

    public Color movementColor;
    public float Accel;
    public float maxSpeed;

    public float JumpForce;


    public override void Init(Player parent) {
        base.Init(parent);
        timePassed = 0f;
        Camera = Camera.main;
        Inputs = player.Inputs;
        Inputs.Normal.GrappleRelease.performed+=OnGrappleRelease;
        Inputs.Normal.Jump.performed += OnJump;
        GrapplingHookObject = (GameObject)GameObject.Instantiate(GrapplingHookPrefab,player.GrapplingHookSpotPosition,Quaternion.identity);
        SpringJoint2D joint = GrapplingHookObject.GetComponent<SpringJoint2D>();
        joint.distance = Vector3.Distance(player.transform.position,player.GrapplingHookSpotPosition);
        joint.connectedBody = rb;
        rb.gravityScale = GravityScale;
        rb.drag = Drag;

        player.GrapplingLineRend.enabled = true;
        

        hookedTime = 0f;
    }
    // Unity Update
    public override void ConstantUpdate() {
        hookedTime+=Time.deltaTime;
        if (hookedTime < TimeToFullyHook) {
            Vector3 GrapplingHookPos = GrapplingHookObject.transform.position;
            Vector3 playerPos = player.transform.position;

            Vector3 AddAmount = new Vector3(playerPos.x-GrapplingHookPos.x,playerPos.y-GrapplingHookPos.y,0);
            AddAmount = new Vector3(AddAmount.x*hookedTime/(TimeToFullyHook),AddAmount.y*hookedTime/(TimeToFullyHook),0);
            player.GrapplingLineRend.SetPosition(1,new Vector3(playerPos.x-AddAmount.x,playerPos.y-AddAmount.y,playerPos.z));
        }
        else {
            player.GrapplingLineRend.SetPosition(1,player.GrapplingHookSpotPosition);
        }
        
        player.GrapplingLineRend.SetPosition(0,player.transform.position);
        
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
            // Debug.Log("RUNNING");
            player.MoveRight(Accel,maxSpeed);
            player.Color(movementColor);
        }
        else if (Input.GetKey(KeyCode.A)) {
            // Debug.Log("RUNNING");
            player.MoveLeft(Accel,maxSpeed);
            player.Color(movementColor);
        }
        else {
            player.Color(player.defaultColor);
            // player.Still(Accel);
        }
    }
    public override void Exit() {
        Inputs.Normal.GrappleRelease.performed-=OnGrappleRelease;
        Inputs.Normal.Jump.performed -= OnJump;
        player.GrapplingLineRend.enabled = false;
        Destroy(GrapplingHookObject);
        player.Color(player.defaultColor);
    }
    private void DeAccel(Vector3 Speed) {
    }
    void OnGrappleRelease(InputAction.CallbackContext context) {
        runner.SetState(IdleState);
    }
    void OnJump(InputAction.CallbackContext context) {
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up*JumpForce,ForceMode2D.Impulse);
    }
}
