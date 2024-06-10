using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangePlayerType : MonoBehaviour
{
    public static int value = 0;
    public GameObject[] PlayerTypes;
    public string[] Text;
    public string[] FailText;
    public ChangeText ChangeTutorText;
    public ChangeText ChangeFailText;
    private TMP_Dropdown drop;

    private bool manualChange = false;

    void Start() {
        drop = GetComponent<TMP_Dropdown>();
        drop.value = value;
    }
    
    

    public void ChangePlayer(int val) {

        if (manualChange) {
            manualChange = false;
            return;
        }

        if (MeetsReq(val) && manualChange == false) {
            PlayerSpawning.PlayerObject = PlayerTypes[val];
            ChangeTutorText.Text(Text[val]);
            value = val;

        }

        

        if (! MeetsReq(val)) {
            manualChange = true;

            ChangeFailText.Text(FailText[val]);
            drop.value = value;

            
            
        }

    }

    private bool MeetsReq(int val) {
        if (val == 0) {
            return true;
        }
        if (val == 1) {
            if (PlayerPrefs.GetFloat("maxSpeed") > 75) {
                return true;
            }
            return false;
        }
        if (val == 2) {
            if (PlayerPrefs.GetFloat("maxSpeed") > 110) {
                return true;
            }
            return false;
        }
        if (val == 3) {
            if (PlayerPrefs.GetInt("MaxScore") > 400) {
                return true;
            }
            return false;
        }
        if (val == 4) {
            if (PlayerPrefs.GetInt("MaxScore") > 800) {
                return true;
            }
            return false;
        }
        if (val == 5) {
            if (PlayerPrefs.GetInt("MaxScore") > 1100) {
                return true;
            }
            return false;
        }
        return true;
    }
}
