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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(speed*Vector2.right,ForceMode2D.Impulse);
        Player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        ogSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) {
            rb.velocity = Vector2.zero;
        }
        else {
            if (Player.transform.position.x > 0) {
                speed = ogSpeed + (Player.transform.position.x / IncrementDivider);
            }
            if (Player.velocity.x > speed) {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x,Player.velocity.x,Time.deltaTime*3.5f),0);
            }
            else {
                rb.velocity = new Vector2(speed,0);
            }
        }
    }
}
