using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private LevelGrid levelGrid;

    private Bounds cameraWorldBounds;

    private void Awake()
    {
        levelGrid.OnInitialized += SetCameraBoundsFromWorldBounds;
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        transform.position = GetCameraBounds(target.transform.position);
    }

    private void OnDestroy()
    {
        levelGrid.OnInitialized -= SetCameraBoundsFromWorldBounds;
    }

    private void SetCameraBoundsFromWorldBounds()
    {
        var worldBounds = levelGrid.Bounds;
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

    private Vector3 GetCameraBounds(Vector3 targetPosition) => new(
        Mathf.Clamp(targetPosition.x, cameraWorldBounds.min.x, cameraWorldBounds.max.x),
        Mathf.Clamp(targetPosition.y, cameraWorldBounds.min.y, cameraWorldBounds.max.y),
        transform.position.z
    );
}
