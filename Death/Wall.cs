using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed = 20f;
    public Rigidbody2D Player;
    public float IncrementDivider = 100;
    private float ogSpeed;
    public float CurveMultiplier;
    private bool Died = false;
    public float distance = 100f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // rb.AddForce(speed*Vector2.right,ForceMode2D.Impulse);
        GameObject PlayerObject = GameObject.FindWithTag("Player");
        if (PlayerObject != null) {
            Player = PlayerObject.GetComponent<Rigidbody2D>();
        }
        ogSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null&&Died) {
            rb.velocity = Vector2.zero;
        }
        if (Player == null&&!Died) {
            GameObject PlayerObject = GameObject.FindWithTag("Player");
            if (PlayerObject != null) {
                Player = PlayerObject.GetComponent<Rigidbody2D>();
            }
        }
        else {
            // Debug.Log(Player.transform.position.x-transform.position.x);
            if (Player.transform.position.x > 0) {
                speed = ogSpeed + (Player.transform.position.x / 3 / IncrementDivider);
            }
            // if (Player.velocity.x > speed) {
            //     rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x,Player.velocity.x*0.85f,Time.deltaTime*CurveMultiplier),0);
            // }
            // else {
            //     rb.velocity = new Vector2(speed,0);
            // }

            if (Player.transform.position.x-transform.position.x > distance) {
                transform.position = new Vector3(Player.transform.position.x-distance,0,0);
            }

            rb.velocity = new Vector2(speed,0);

            Died = true;
        }
    }
}
