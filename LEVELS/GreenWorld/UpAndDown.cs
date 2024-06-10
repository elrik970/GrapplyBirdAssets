using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 MoveAmount;
    private Vector3 startingPos;
    public float speed;
    private Rigidbody2D rb;
    public LineRenderer rend;
    void Start()
    {
        startingPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        // rend = GetComponent<LineRenderer>();

        rend.SetPosition(0,startingPos+new Vector3(MoveAmount.x,MoveAmount.y,startingPos.z+1));
        rend.SetPosition(1,startingPos+new Vector3(-MoveAmount.x,-MoveAmount.y,startingPos.z+1));
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate() {
        rb.MovePosition(transform.position + new Vector3(MoveAmount.x,MoveAmount.y,0) * Time.deltaTime * speed);
        float distance = (startingPos+ new Vector3(MoveAmount.x,MoveAmount.y,0)-transform.position).magnitude;
        if (distance > -0.5 && distance < 0.5) {
            MoveAmount*= -1;
        }
    }
}
