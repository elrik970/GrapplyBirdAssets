using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraAim : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform Player;
    private CinemachineVirtualCamera Cmvcam;
    
    void Start()
    {
        Camera Camera = Camera.main;
        Cmvcam = GameObject.FindWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) {
            GameObject obj = GameObject.FindWithTag("Player");
            if (obj != null) {
                Player = obj.transform;
                Cmvcam.Follow = Player;
            }
        }
    }
}
