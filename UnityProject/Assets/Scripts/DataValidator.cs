using UnityEngine;

public class DataValidator : MonoBehaviour
{
    // 1. 도감 관리자랑 연결할 변수
    public PokemonDatabase pokemonDB;

    void Start()
    {
        // 게임 시작하자마자 테스트해보기
        Debug.Log("🔍 포켓몬 데이터 검색 테스트를 시작합니다...");

        // 테스트 1: 리자몽 찾기
        FindAndPrint("리자몽");

        // 테스트 2: 뮤츠 찾기
        FindAndPrint("뮤츠");

        // 테스트 3: 없는 포켓몬 찾기 (에러 처리 확인)
        FindAndPrint("아구몬"); 
    }

    // 검색하고 결과를 출력하는 함수
    void FindAndPrint(string name)
    {
        PokemonData data = pokemonDB.FindPokemon(name);

        if (data != null)
        {
            Debug.Log($"[발견!] 이름: {data.name} / ID: {data.id} / 타입: {data.types[0]} / HP: {data.baseStats.hp}");
        }
        else
        {
            Debug.LogWarning($"[실패] '{name}'(이)라는 포켓몬은 도감에 없습니다.");
        }
    }
}