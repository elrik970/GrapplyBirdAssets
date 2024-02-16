using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailcolorPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer Player;
    private ParticleSystem part;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var main = part.main;
        main.startColor = Player.color;
    }
}
