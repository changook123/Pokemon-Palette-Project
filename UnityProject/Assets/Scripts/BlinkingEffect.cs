using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    
    [Header("깜빡임 설정")]
    [Tooltip("한 번 깜빡이는 데 걸리는 시간 (초)")]
    public float blinkDuration = 2.0f; // 2초로 설정

    private float startTime; // 시작 시간을 기억하기 위한 변수

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // 게임이 시작된 시점을 0초로 기준 잡기 (이게 있어야 무조건 투명하게 시작함)
        startTime = Time.time;
    }

    void Update()
    {
        // 경과 시간 계산
        float timePassed = Time.time - startTime;

        // PingPong: 0에서 1까지 갔다가 다시 0으로 일정하게 왕복하는 함수
        // blinkDuration(1초) 안에 0 -> 1 -> 0 이 다 들어가려면 속도를 조절해야 함
        // 계산식: (2.0f / 시간)을 곱해주면 정확히 그 시간에 한 바퀴 돔
        float alpha = Mathf.PingPong(timePassed * (2.0f / blinkDuration), 1.0f);
        
        canvasGroup.alpha = alpha;
    }
}