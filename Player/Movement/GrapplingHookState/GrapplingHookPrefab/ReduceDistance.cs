using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceDistance : MonoBehaviour
{
    private SpringJoint2D joint;
    [SerializeField]private float TimeToDecreaseToZero;
    [SerializeField]private float minDistance = 0f;
    float startingDistance;
    float TimePassed;
    void Start()
    {
        joint = GetComponent<SpringJoint2D>();
        startingDistance = joint.distance;
        TimePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        TimePassed+=Time.deltaTime;
        if (joint.distance > minDistance) {
            // Debug.Log(((TimeToDecreaseToZero-TimePassed)/TimeToDecreaseToZero));
            // joint.distance-=DecreaseAmountPerSecond*Time.deltaTime;
            joint.distance = startingDistance*((TimeToDecreaseToZero-TimePassed)/TimeToDecreaseToZero);
        }
        
    }
}
