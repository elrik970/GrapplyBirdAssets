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
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) {
            GetComponent<Button>().enabled = true;
            GetComponent<Image>().enabled = true;
            transform.GetChild(0).GetComponent<TMP_Text>().enabled = true;
        }
    }

    public void RestartCurrentScene() {
        SceneManager.LoadScene(scene);
    }
}
