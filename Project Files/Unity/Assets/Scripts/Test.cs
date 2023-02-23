using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;
    public void Activate()
    {
        debugText.text = "AR Active";
    }

    public void Deactivate()
    {
        debugText.text = "AR Inactive";
    }
}
