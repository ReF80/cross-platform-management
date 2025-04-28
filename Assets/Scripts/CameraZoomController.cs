using System;
using System.Collections;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{
    [Header("Настройки камеры")]
    [SerializeField] private Transform playerTarget; 
    [SerializeField] private float startSize = 1f; 
    [SerializeField] private float targetSize = 5f; 
    [SerializeField] private float zoomDuration = 3f; 
    
    [SerializeField] private Camera mainCamera;
    private Vector3 initialOffset;
    private bool isZooming = true;
    //public event Action OnEndZoom;

    private void Awake()
    {
        mainCamera.orthographicSize = startSize;
        initialOffset = transform.position - playerTarget.position;
    }

    private void Start()
    {
        StartCoroutine(ZoomOutCoroutine());
    }

    private void LateUpdate()
    {
        if (isZooming)
        {
            transform.position = playerTarget.position + initialOffset;
        }
    }

    private IEnumerator ZoomOutCoroutine()
    {
        float elapsedTime = 0f;
        float currentSize = startSize;
        
        while (elapsedTime < zoomDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / zoomDuration);
            
            currentSize = Mathf.Lerp(startSize, targetSize, t);
            mainCamera.orthographicSize = currentSize;
            
            yield return null;
        }
        
        mainCamera.orthographicSize = targetSize;
        isZooming = false;
        //OnEndZoom?.Invoke();
    }
}
