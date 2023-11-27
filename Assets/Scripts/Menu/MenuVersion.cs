using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuVersion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI versionText;

    private void Start()
    {
        versionText.text = "v" + Application.version;
    }
}
