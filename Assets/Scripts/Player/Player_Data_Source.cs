using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object Class That Contains The Player Component Variales
/// </summary>
[CreateAssetMenu(menuName = "DataSources/Player")]
public class Player_Data_Source : ScriptableObject
{
    public PlayerComponent _player { get; set; }
}
