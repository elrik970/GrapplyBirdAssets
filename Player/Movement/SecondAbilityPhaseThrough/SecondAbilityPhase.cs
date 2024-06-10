using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New PhaseAbilityState", menuName = "States/Player/PhaseThroughAbilityState")]
public class SecondAbilityPhaseThrough : PlayerState<Player>
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
    public float refreshTime;



    // Start is called before the first frame update
    public override void Init(Player parent) {
        base.Init(parent);
        
        player.secondAbilityPassedTime = 0f;

        Inputs = Player.Inputs;
        Inputs.Normal.Grapple.performed+=OnGrapple;
        Inputs.Normal.Jump.performed += OnJump;

        parent.GetComponent<Collider2D>().enabled = false;

        curTime = 0f;

        speed = ogSpeed;

        if (rb.velocity.x > ogSpeed) {
            speed = rb.velocity.x;
        }

        rb.velocity = new Vector2(speed,0f);

        player.dashSoundFx.pitch = Random.Range(0.85f, 1.2f);
        player.dashSoundFx.Play();
        
    }

    public override void ConstantUpdate() {
        player.Color(dashColor);
        curTime+=Time.deltaTime;
        if (curTime > maxTime) {
            if (player.OnGround()) {
                curTime = maxTime-refreshTime;
            }
            else {
                runner.SetState(risingState);
            }
        }

        // if (player.OnGround()) {
        //     runner.SetState(risingState);
        // }

        
    }
    public override void ChangeState() {
        
    }
    // Unity Update
    public override void CaptureInputs() {
        
    }
    public override void PhysicsUpdate() {
        player.checkForDeath();
        if (rb.velocity.x > speed) {
            runner.SetState(risingState);
        }
        else {
            rb.velocity = new Vector2(speed,0f);
        }
    }

    public override void Exit() {
        Inputs.Normal.Grapple.performed-=OnGrapple;
        Inputs.Normal.Jump.performed -= OnJump;

        rb.GetComponent<Collider2D>().enabled = true;

        rb.velocity = new Vector2(rb.velocity.x/velocityDivider,rb.velocity.y);

        player.secondAbilityPassedTime = 0f;


        player.Color(player.defaultColor);
        
    }

    void OnGrapple(InputAction.CallbackContext context) {
        if (!player.OnGround()) {
            if (player.GrapplingHookCheck()) {
                runner.SetState(grapplingState);
            }
        }
    }

    void OnJump(InputAction.CallbackContext context) {
        if (!player.OnGround()) {
            player.Jump();
            runner.SetState(risingState);
        }
    }
}
