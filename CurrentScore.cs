using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentScore : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    private float score;
    public TMPro.TMP_Text text;
    public TMPro.TMP_Text maxScoreText;
    public float Divider;

    

    public string BeforeNum = "";
    public string AfterNum = "M";
    void Start()
    {
        maxScoreText.text = "Best: " + PlayerPrefs.GetInt("MaxScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate() {
        if (rb == null) {
            GameObject obj = GameObject.FindWithTag("Player");
            if (obj != null) {
                rb = obj.GetComponent<Rigidbody2D>();
            }
        }
        else if (rb.velocity.x > 0) {
            if (rb.transform.position.x > 0) {
                score =  rb.transform.position.x;
            }
            int finalScore = (int)(score/Divider);
            text.text = BeforeNum + (finalScore).ToString() + AfterNum;

            if (finalScore > PlayerPrefs.GetInt("MaxScore")) {
                PlayerPrefs.SetInt("MaxScore",finalScore);
            }
            maxScoreText.text = "Best: " + PlayerPrefs.GetInt("MaxScore").ToString();

        }
    }
}
