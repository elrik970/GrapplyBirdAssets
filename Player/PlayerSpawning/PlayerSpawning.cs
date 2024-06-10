using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSpawning : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static GameObject PlayerObject;
    public GameObject defaultDot;
    public GameObject dashDot;
    public TextMeshPro TMProText;
    
    

    void Start()
    {
        if (PlayerObject != null) {
            GameObject.Instantiate(PlayerObject,new Vector3(-15f,0f,0f),Quaternion.identity);
        }
        else {
            GameObject.Instantiate(defaultDot,new Vector3(-15f,0f,0f),Quaternion.identity);
        }

        if (PlayerObject == null || PlayerObject == dashDot) {
            TMProText.enabled = false;
        }
        else {
            TMProText.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
