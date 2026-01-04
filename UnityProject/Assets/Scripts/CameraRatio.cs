using UnityEngine;

public class CameraRatio : MonoBehaviour
{
    void Start()
    {
        // 우리가 원하는 고정 비율 (1:1 정사각형)
        float targetAspect = 1.0f / 1.0f;

        // 현재 모니터(창)의 비율
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // 비율 계산
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        // 1. 모니터가 너무 가로로 길 때 (양옆에 레터박스 필요)
        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        // 2. 모니터가 너무 세로로 길 때 (위아래 레터박스 필요)
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
    }
}