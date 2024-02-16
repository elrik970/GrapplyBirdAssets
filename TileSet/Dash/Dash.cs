using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    // Start is called before the first frame update
    public float DashForce;
    public float VeticalDashForce;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")){
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            // rb.velocity = new Vector2(rb.velocity.x,0f);
            rb.AddForce(DashForce*Vector2.right,ForceMode2D.Impulse);
            rb.AddForce(VeticalDashForce*Vector2.up,ForceMode2D.Impulse);
            Destroy(gameObject);
        }
    }
}
