using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera Camera;
    void Start()
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Music");
        if (obj.Length > 1) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
            Camera = Camera.main;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.transform.position;
    }
}
