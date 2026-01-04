using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System; // Action 기능을 쓰기 위해 필요

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;

    [Header("연결 필수!")]
    public CanvasGroup curtainCanvasGroup;

    [Header("설정")]
    public float fadeDuration = 0.5f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // 1. 씬 이동용 페이드 (기존 기능)
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(ProcessFade(sceneName));
    }

    // 2. 패널 열기용 페이드 (새로 추가된 기능! ⭐)
    // "어두워진 뒤에 이 행동(action)을 실행해라"
    public void PlayTransition(Action actionAfterFade)
    {
        StartCoroutine(ProcessTransition(actionAfterFade));
    }

    // 씬 이동 코루틴
    private IEnumerator ProcessFade(string sceneName)
    {
        yield return StartCoroutine(Fade(0f, 1f)); // 어두워짐
        SceneManager.LoadScene(sceneName);         // 씬 이동
        yield return new WaitForSeconds(0.5f);     // 대기
        yield return StartCoroutine(Fade(1f, 0f)); // 밝아짐
    }

    // 패널 열기 코루틴
    private IEnumerator ProcessTransition(Action action)
    {
        yield return StartCoroutine(Fade(0f, 1f)); // 어두워짐
        
        // 깜깜할 때 몰래 패널을 켬!
        if (action != null) action.Invoke();
        
        yield return new WaitForSeconds(0.5f);     // 대기
        yield return StartCoroutine(Fade(1f, 0f)); // 밝아짐
    }

    // 페이드 로직 (재사용)
    private IEnumerator Fade(float start, float end)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            curtainCanvasGroup.alpha = Mathf.Lerp(start, end, timer / fadeDuration);
            yield return null;
        }
        curtainCanvasGroup.alpha = end;
    }
}