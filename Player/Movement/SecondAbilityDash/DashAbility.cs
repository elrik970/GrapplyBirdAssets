using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New DashAbilityState", menuName = "States/Player/DashAbilityState")]
public class DashAbility : PlayerState<Player>
{
    private PlayerInputs Inputs;
    public float speed;
    public Color dashColor;
    public float JumpForce = 15f;
    public PlayerState<Player> risingState;
    public PlayerState<Player> grapplingState;
    public AnimationCurve animationCurve;
    public float maxTime;
    private float curTime;
    public float velocityDivider;
    public float ogSpeed;
    public bool mouseDirection;
    private Vector3 direction;
    

    // Start is called before the first frame update
    public override void Init(Player parent) {
        base.Init(parent);

        Inputs = Player.Inputs;
        Inputs.Normal.Grapple.performed+=OnGrapple;
        Inputs.Normal.Jump.performed += OnJump;

        player.secondAbilityPassedTime = 0f;

        curTime = 0f;

        speed = ogSpeed;

        if (rb.velocity.x > ogSpeed) {
            speed = rb.velocity.x;
        }

        if (!mouseDirection) {
            rb.velocity = new Vector2(speed*animationCurve.Evaluate(curTime),0f);
        }
        else {
            direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition)-player.transform.position).normalized;
            rb.velocity = direction*speed;
        }

        player.dashSoundFx.pitch = Random.Range(0.85f, 1.2f);
        player.dashSoundFx.Play();
        
    }

    public override void ConstantUpdate() {
        player.Color(dashColor);
        curTime+=Time.deltaTime;
        if (curTime > maxTime) {
            runner.SetState(risingState);
        }

        if (player.OnGround()) {
            runner.SetState(risingState);
            return;
        }
    }
    public override void ChangeState() {
        
    }
    // Unity Update
    public override void CaptureInputs() {
        
    }
    public override void PhysicsUpdate() {
        if (rb.velocity.x > speed) {
            runner.SetState(risingState);
        }
        else {
            if (!mouseDirection) {
                rb.velocity = new Vector2(speed*animationCurve.Evaluate(curTime),0f);
            }
            else {
                direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition)-player.transform.position).normalized;
                rb.velocity = direction*speed;
            }
        }
    }

    public override void Exit() {
        Inputs.Normal.Grapple.performed-=OnGrapple;
        Inputs.Normal.Jump.performed -= OnJump;

        player.secondAbilityPassedTime = 0f;

        rb.velocity /= velocityDivider;

        player.Color(player.defaultColor);
        speed = ogSpeed;
        
    }

    void OnGrapple(InputAction.CallbackContext context) {
        if (player.GrapplingHookCheck()) {
            runner.SetState(grapplingState);
        }
    }

    void OnJump(InputAction.CallbackContext context) {
        player.Jump();
        runner.SetState(risingState);
    }
}
