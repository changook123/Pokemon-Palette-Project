using UnityEngine;
using TMPro; // 1. UI(텍스트메시프로)를 쓰려면 이게 꼭 필요합니다!
using UnityEngine.UI;

public class PokedexUI : MonoBehaviour
{
    public PokemonDatabase database;  // 데이터베이스 연결
    public TMP_InputField inputField; // 검색창 연결
    public TextMeshProUGUI resultText; // 결과 보여줄 텍스트 연결

    // 버튼을 누르면 실행될 함수
    public void OnClickSearch()
    {
        string nameToFind = inputField.text; // 1. 입력된 글자를 가져온다.
        PokemonData data = database.FindPokemon(nameToFind); // 2. DB에서 찾는다.

        if (data != null)
        {
            // 3. 찾았으면 정보를 예쁘게 보여준다.
            resultText.text = $"이름: {data.name}\n" +
                              $"타입: {data.types[0]}\n" +
                              $"HP: {data.baseStats.hp} / 공격: {data.baseStats.atk}";
        }
        else
        {
            // 4. 없으면 없다고 알려준다.
            resultText.text = "도감에 없는 포켓몬입니다.";
        }
    }
}