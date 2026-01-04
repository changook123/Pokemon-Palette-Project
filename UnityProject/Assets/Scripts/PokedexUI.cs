using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Collections.Generic; // 리스트(List)를 쓰기 위해 필요!

public class PokedexUI : MonoBehaviour
{
    public PokemonDatabase database;
    public TMP_InputField inputField;
    public TextMeshProUGUI resultText;

    public void OnClickSearch()
    {
        // 1. 검색어 가져오기 (앞뒤 공백 제거)
        string nameToFind = inputField.text.Trim(); 

        if (string.IsNullOrEmpty(nameToFind)) return; // 빈칸이면 검색 안 함

        // 2. DB에서 '모두' 찾기 (FindAllPokemon 사용!)
        List<PokemonData> results = database.FindAllPokemon(nameToFind);

        if (results.Count > 0)
        {
            // 3. 찾은 포켓몬들의 정보를 텍스트 하나로 합치기
            string finalOutput = "";

            foreach (var data in results)
            {
                string typeString = string.Join(", ", data.types);
                int totalStats = data.baseStats.hp + data.baseStats.atk + data.baseStats.def + 
                                 data.baseStats.spAtk + data.baseStats.spDef + data.baseStats.speed;

                // 정보 쌓기 (+= 사용)
                finalOutput += $"<size=45>{data.name}</size>  <size=25>(No.{data.id})</size>\n" +
                               $"타입: {typeString}\n" +
                               $"HP: {data.baseStats.hp} / 공: {data.baseStats.atk} / 방: {data.baseStats.def}\n" +
                               $"특공: {data.baseStats.spAtk} / 특방: {data.baseStats.spDef} / 스피드: {data.baseStats.speed}\n" +
                               $"총합: {totalStats}\n" +
                               $"<color=yellow>────────────────────────</color>\n\n";
            }

            // 4. 화면에 출력
            resultText.text = finalOutput;
        }
        else
        {
            resultText.text = $"<color=red>'{nameToFind}'</color>을(를) 찾을 수 없습니다.";
        }
    }
}