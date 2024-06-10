using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public string scene = "TitleScreen";

    private Button button;
    private Image image;
    private TMP_Text text;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<TMP_Text>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) {
            button.enabled = true;
            image.enabled = true;
            text.enabled = true;
            Player = GameObject.FindWithTag("Player");
        }
        else {
            button.enabled = false;
            image.enabled = false;
            text.enabled = false;
            
        }
    }

    public void RestartCurrentScene() {
        SceneManager.LoadScene(scene);
    }
}
