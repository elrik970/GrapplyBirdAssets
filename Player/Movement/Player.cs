using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : MonoBehaviour
{
    public PlayerState<Player> secondAbility;

    public Camera Camera;
    public CinemachineVirtualCamera Cmvcam;

    public GameObject DeathEffects;
    
    private Rigidbody2D rb;

    // GrapplingHook
    public Vector3 GrapplingHookSpotPosition;
    public LayerMask GrapplingHookLayerMask;
    public float GrappleDistance;
    public LineRenderer GrapplingLineRend;
    public GameObject GrapplingHookHitGameObject;

    // Grounded
    public float GroundRaycastSize;
    public LayerMask GroundLayerMask;
    private RaycastHit2D GroundHit;

    public Color defaultColor;
    public Color movementColor;
    public Color coolDownColor;
    public SpriteRenderer sprite;

    public ParticleSystem CollisionPart;

    public float minVelocityCrash;

    public static PlayerInputs Inputs;

    public float secondAbilityPassedTime = 0f;
    public float secondAbilityCoolDownTime = 0.5f;
    public AudioSource dashSoundFx;

    public AudioSource jumpSoundFx;
    public float JumpForce = 15f;

    public LayerMask KillLayerMask;

    // void Awake() {
    //     Inputs = new PlayerInputs();
    // }
    void OnEnable() {
        Inputs.Enable();
    }

    void OnDisable() {
        if (Inputs != null) Inputs.Disable();
    }
    void Start()
    {

        secondAbilityPassedTime = secondAbilityCoolDownTime;
        Camera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Cmvcam = GameObject.FindWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
        secondAbility = GetComponent<SecondAbility>().secondAbility;
    }

    // Update is called once per frame
    void Update()
    {
        secondAbilityPassedTime+=Time.deltaTime;
        if (!secondAbilityCoolDown()) {
            Color(coolDownColor);
        }
    }
    public bool GrapplingHookCheck() {
        Vector3 mouseWorldPos = Camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos = new Vector3(mouseWorldPos.x,mouseWorldPos.y,transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,(mouseWorldPos-transform.position).normalized,GrappleDistance,GrapplingHookLayerMask);
        if (hit.collider != null) {
            GrapplingHookSpotPosition = hit.point;
            GrapplingHookHitGameObject = hit.collider.gameObject;
            return true;
        }
        return false;
    }
    public bool OnGround() {
        RaycastHit2D OnGroundHit = Physics2D.CircleCast(transform.position, GroundRaycastSize,Vector2.zero ,0,GroundLayerMask);
        if (OnGroundHit.collider != null) {
            return true;
        }
        return false;
    }

    public RaycastHit2D OnGroundOverride(Vector2 Direction, float Length) {
        GroundHit = Physics2D.CircleCast(transform.position, GroundRaycastSize,Direction,Length,GroundLayerMask);

        // Debug.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y+(GroundRaycastSize/2),transform.position.z));
        // Debug.DrawLine(transform.position,new Vector3(transform.position.x+(GroundRaycastSize/2),transform.position.y,transform.position.z));
        return GroundHit;
    }

    public void MoveRight(float Accel,float maxSpeed) {
        if (rb.velocity.x < maxSpeed) {
            float moveDif = (maxSpeed-rb.velocity.x)*Accel;
            rb.AddForce(Vector2.right*moveDif,ForceMode2D.Impulse);
        }
    }
    public void MoveLeft(float Accel,float maxSpeed) {
        if (rb.velocity.x > -maxSpeed) {
            float moveDif = (-maxSpeed-rb.velocity.x)*Accel;
            rb.AddForce(Vector2.right*moveDif,ForceMode2D.Impulse);
        }
    }
    public void Still(float Accel) {
            float moveDif = -rb.velocity.x*Accel;
            rb.AddForce(Vector2.right*moveDif,ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Kill")) {
            GameObject fx = (GameObject)GameObject.Instantiate(DeathEffects,transform.position,Quaternion.identity);
            Destroy(gameObject);
            Cmvcam.Follow = fx.transform;
        }
        if (rb.velocity.magnitude > minVelocityCrash) {
            CollisionPart.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Kill")) {
            GameObject fx = (GameObject)GameObject.Instantiate(DeathEffects,transform.position,Quaternion.identity);
            Destroy(gameObject);
            Cmvcam.Follow = fx.transform;
        }
    }
    public void Color(Color color) {
        sprite.color = color;
    }

    public bool secondAbilityCoolDown() {
        if (secondAbilityPassedTime > secondAbilityCoolDownTime) {
            
            return true;

        }
        else {
            return false;
        }
    }

    public void checkForDeath() {
        RaycastHit2D Hit = Physics2D.CircleCast(transform.position, GroundRaycastSize,Vector2.zero ,0,KillLayerMask);
        if (Hit.collider != null) {
            GameObject fx = (GameObject)GameObject.Instantiate(DeathEffects,transform.position,Quaternion.identity);
            Destroy(gameObject);
            Cmvcam.Follow = fx.transform;
        }
    }
    public void Jump() {
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.AddForce(Vector2.up*JumpForce,ForceMode2D.Impulse);
        jumpSoundFx.pitch = Random.Range(1.5f, 2f);
        jumpSoundFx.Play();
    }

    
}
