using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("이동할 씬 이름들")]
    public string gameSceneName = "GameScene";     // 새 게임
    public string optionSceneName = "OptionScene"; // 옵션

    [Header("버튼 이미지들")]
    public Image[] menuButtons; // 0:새게임, 1:옵션, 2:종료

    [Header("교체할 그림 소스")]
    public Sprite selectedSprite;
    public Sprite normalSprite;

    private int currentIndex = 0;

    void Start()
    {
        UpdateMenuVisual();
    }

    void Update()
    {
        // 방향키 이동
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex++;
            if (currentIndex >= menuButtons.Length) currentIndex = 0;
            UpdateMenuVisual();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = menuButtons.Length - 1;
            UpdateMenuVisual();
        }
        
        // 엔터 키 입력
        if (Input.GetKeyDown(KeyCode.Return))
        {
             HandleEnterKey();
        }
    }

    void HandleEnterKey()
    {
        switch (currentIndex)
        {
            case 0: // [새 게임]
                Debug.Log("새 게임 화면으로 이동합니다.");
                TransitionManager.Instance.LoadSceneWithFade(gameSceneName);
                break;

            case 1: // [옵션]
                Debug.Log("옵션 씬으로 이동합니다.");
                TransitionManager.Instance.LoadSceneWithFade(optionSceneName);
                break;

            case 2: // [종료] -> 여기를 수정했습니다! ⭐
                Debug.Log("게임을 종료합니다.");
                
                // "화면을 어둡게 만든 뒤에 -> 종료해라" 라고 명령
                TransitionManager.Instance.PlayTransition(() => 
                {
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false; // 에디터는 멈춤
                    #else
                        Application.Quit(); // 실제 게임은 꺼짐
                    #endif
                });
                break;
        }
    }

    void UpdateMenuVisual()
    {
        for (int i = 0; i < menuButtons.Length; i++)
        {
            if (i == currentIndex) menuButtons[i].sprite = selectedSprite;
            else menuButtons[i].sprite = normalSprite;
        }
    }
}