using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : ActivatedByGrapplingHook
{
    public float speed;
    private bool activated = false;
    public float shrinkSpeed;
    private Rigidbody2D rb;
    public ParticleSystem ps;
    public Collider2D collider;
    public AnimationCurve speedCurve;
    private float timePassed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated&&!End) {
            timePassed+=Time.deltaTime;

            transform.localScale = Vector3.Lerp(transform.localScale,Vector3.zero,Time.deltaTime*shrinkSpeed);

            float curveValue = speedCurve.Evaluate(timePassed);

            rb.velocity = transform.right*curveValue*speed;

            if (transform.localScale.x < 1f) {
                var emission = ps.emission;
                emission.enabled = false;
                collider.enabled = false;
                rb.velocity = Vector2.zero;
                transform.localScale = Vector3.zero;
                Destroy(gameObject,2);
                End = true;

            }
        }
    }

    public override void OnHit() {
        // rb.AddForce(transform.right.normalized*speed,ForceMode2D.Impulse);
        if (!activated) {

            activated = true;
            
            var emission = ps.emission;
            emission.enabled = true;

            timePassed = 0f;
        }
    }

}
