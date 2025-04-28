using System;
using UnityEngine;

public class UIBackgroundScaler : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CameraZoomController cameraZoomController;
 
    private void Start()
    {
        //cameraZoomController.OnEndZoom += ScaleBackground;
    }
    
    private void ScaleBackground()
    {
        if (canvas == null) return;
        
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}
  