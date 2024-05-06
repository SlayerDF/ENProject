using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    public GameObject Target { get => target; set => target = value; }

    private Bounds cameraWorldBounds;

    public void SetCameraBoundsFromWorldBounds(BoundsInt worldBounds)
    {
        var camera = GetComponent<UnityEngine.Camera>();

        var height = camera.orthographicSize;
        var width = height * camera.aspect;

        var minX = worldBounds.xMin + width;
        var maxX = worldBounds.xMax - width;

        var minY = worldBounds.yMin + height;
        var maxY = worldBounds.yMax - height;

        cameraWorldBounds = new Bounds();
        cameraWorldBounds.SetMinMax(
            new Vector3(minX, minY, 0),
            new Vector3(maxX, maxY, 0)
        );
    }

    private void LateUpdate()
    {
        if (target == null) return;

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
