using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActivatedByGrapplingHook : MonoBehaviour
{
    public bool End = false;
    public abstract void OnHit();

}
