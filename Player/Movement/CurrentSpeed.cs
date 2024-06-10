using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    public float Multiplier;
    public Rigidbody2D rb;
    public TMPro.TMP_Text text;
    public float maxSize = 3;
    public float SizeDivider = 15;
    public float transitionMultiplier;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ((int)(Mathf.Abs(rb.velocity.x)*Multiplier)).ToString();
        // float size = Mathf.Clamp((Mathf.Abs(rb.velocity.x)/SizeDivider),0,maxSize);
        // Debug.Log(Mathf.Abs(rb.velocity.x)/SizeDivider);
        text.fontSize = maxSize;

        if (rb.velocity.x * Multiplier > PlayerPrefs.GetFloat("maxSpeed")) {
            PlayerPrefs.SetFloat("maxSpeed",rb.velocity.x*Multiplier);
        }
    }
}
