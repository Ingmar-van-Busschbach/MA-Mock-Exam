using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ARDebugger : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;
    public void Activate()
    {
        debugText.text = "AR Active";
    }

    public void RouteCompleted()
    {
        debugText.text = "Route has been completed!";
    }

    public void ActivateImportant(int importance)
    {
        debugText.text = "AR Active, Importance " + importance;
    }

    public void Deactivate()
    {
        debugText.text = "AR Inactive";
    }
}
