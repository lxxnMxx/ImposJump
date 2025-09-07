using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;

public class LevelLoadingHandler : MonoBehaviour
{
    [SerializeField] private Vector2 renderingRange;
    [SerializeField] private Camera cam;
    [SerializeField] private float movementThreshold = 1f;
    
    [SerializeField] private List<GameObject> chunks;

    private Vector3 _lastCamPosition;
    
    // caching variables
    private float _camX;
    private float _minX;
    private float _maxX;
    private float _chunkX;
    private bool _shouldBeActive;

    private void Start()
    {
        _lastCamPosition = cam.transform.position;
        UpdateChunks();
    }

    void LateUpdate()
    {
        if (!(Mathf.Abs(cam.transform.position.x - _lastCamPosition.x) > movementThreshold)) return;
        _lastCamPosition = cam.transform.position;
        UpdateChunks();
    }

    void UpdateChunks()
    {
        _camX = cam.transform.position.x;
        _minX = _camX + renderingRange.y;
        _maxX = _camX + renderingRange.x;

        foreach (GameObject chunk in chunks)
        {
            _chunkX = chunk.transform.position.x;
            _shouldBeActive = _chunkX >= _minX && _chunkX <= _maxX;
            
            if(chunk.activeSelf != _shouldBeActive)
                chunk.SetActive(_shouldBeActive);
        }
    }
}
