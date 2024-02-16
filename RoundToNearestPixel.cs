using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundToNearestPixel : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 vectorInPixels;
    public int PPU = 32;
    void Start()
    {
        
    }

    
    void LateUpdate() {
        vectorInPixels = new Vector2(Mathf.RoundToInt(transform.position.x*PPU),Mathf.RoundToInt(transform.position.y*PPU));
        transform.position = vectorInPixels / PPU;
    }
}
