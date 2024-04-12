using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    
    private Bounds cameraWorldBounds;

    void Start()
    {
        var camera = GetComponent<Camera>();

        var height = camera.orthographicSize;
        var width = height * camera.aspect;

        var minX = Globals.WorldBounds.xMin + width;
        var maxX = Globals.WorldBounds.xMax - width;

        var minY = Globals.WorldBounds.yMin + height;
        var maxY = Globals.WorldBounds.yMax - height;

        cameraWorldBounds = new Bounds();
        cameraWorldBounds.SetMinMax(
            new Vector3(minX,minY, 0),
            new Vector3(maxX, maxY, 0)
        );
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(
            target.transform.position.x,
            target.transform.position.y,
            transform.position.z
        );
        transform.position = GetCameraBounds();
    }

    private Vector3 GetCameraBounds() => new Vector3(
        Mathf.Clamp(transform.position.x, cameraWorldBounds.min.x, cameraWorldBounds.max.x),
        Mathf.Clamp(transform.position.y, cameraWorldBounds.min.y, cameraWorldBounds.max.y),
        transform.position.z
    );
}
