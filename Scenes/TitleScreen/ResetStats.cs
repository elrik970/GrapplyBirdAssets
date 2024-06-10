using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResetStats : MonoBehaviour
{
    public TMP_Dropdown drop;
    public void DELETE() {
        PlayerPrefs.DeleteAll();
        drop.value = 0;
    }
}
