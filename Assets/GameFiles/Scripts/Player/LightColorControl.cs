using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightColorControl : MonoBehaviour
{
    // TODO test this out!
    
    [SerializeField] PlayerData playerData;
    [SerializeField] private Light2D lightComponent;
    
    private void OnEnable()
    {
        lightComponent.color = playerData.PlayerColor;
    }
}
