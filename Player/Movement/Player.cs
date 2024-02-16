using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : MonoBehaviour
{
    public Camera Camera;
    public CinemachineVirtualCamera Cmvcam;

    public GameObject DeathEffects;
    
    private Rigidbody2D rb;

    // GrapplingHook
    public Vector3 GrapplingHookSpotPosition;
    public LayerMask GrapplingHookLayerMask;
    public float GrappleDistance;
    public LineRenderer GrapplingLineRend;

    // Grounded
    public float GroundRaycastSize;
    public LayerMask GroundLayerMask;
    private RaycastHit2D GroundHit;

    public Color defaultColor;
    public SpriteRenderer sprite;

    public ParticleSystem CollisionPart;

    public float minVelocityCrash;

    public PlayerInputs Inputs;

    void Awake() {
        Inputs = new PlayerInputs();
    }
    void OnEnable() {
        Inputs.Enable();
    }

    void OnDisable() {
        if (Inputs != null) Inputs.Disable();
    }
    void Start()
    {
        Camera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Cmvcam = GameObject.FindWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool GrapplingHookCheck() {
        Vector3 mouseWorldPos = Camera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos = new Vector3(mouseWorldPos.x,mouseWorldPos.y,transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,(mouseWorldPos-transform.position).normalized,GrappleDistance,GrapplingHookLayerMask);
        if (hit.collider != null) {
            GrapplingHookSpotPosition = hit.point;
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
}
