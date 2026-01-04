using UnityEngine;
// 씬 이동은 이제 TransitionManager가 담당하므로 SceneManagement는 없어도 됩니다.

public class TitleController : MonoBehaviour
{
    [Header("이동할 씬 이름 설정")]
    public string firstTimeScene = "NewMenuScene"; // 데이터 없을 때
    public string continueScene = "MenuScene";     // 데이터 있을 때

    void Update()
    {
        // 엔터(Return) 키 입력 감지
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckDataAndMove();
        }
    }

    void CheckDataAndMove()
    {
        // 1. 저장된 데이터("HasPlayData")가 있는지 검사
        if (PlayerPrefs.HasKey("HasPlayData"))
        {
            // 데이터 있음 -> 이어하기 메뉴로 페이드 이동
            Debug.Log("💾 저장된 데이터 발견! -> MenuScene으로 이동");
            TransitionManager.Instance.LoadSceneWithFade(continueScene);
        }
        else
        {
            // 데이터 없음 -> 새 게임 메뉴로 페이드 이동
            Debug.Log("✨ 데이터 없음 (뉴비) -> NewMenuScene으로 이동");
            TransitionManager.Instance.LoadSceneWithFade(firstTimeScene);
        }
    }
}