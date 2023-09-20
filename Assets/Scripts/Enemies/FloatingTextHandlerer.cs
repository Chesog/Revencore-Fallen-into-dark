using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextHandlerer : MonoBehaviour
{
    public void ShowFloatingText(GameObject floatingTextPrefab, Transform floatingTextSpawn, float currentHealth)
    {
        var go = Instantiate(floatingTextPrefab, floatingTextSpawn.position, Quaternion.identity, transform);
        go.SetActive(true);
        go.GetComponent<TextMesh>().text = currentHealth.ToString();
    }
}
