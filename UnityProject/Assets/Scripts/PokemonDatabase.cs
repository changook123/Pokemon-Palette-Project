using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 능력치 정보
[System.Serializable]
public class BaseStats
{
    public int hp;
    public int atk;
    public int def;
    public int spAtk;
    public int spDef;
    public int speed;
}

// 2. 포켓몬 데이터 정의
[System.Serializable]
public class PokemonData
{
    public int id;
    public string name;      
    public string[] types;   
    public BaseStats baseStats;
}

// 3. 리스트 포장지
[System.Serializable]
public class PokemonListWrapper
{
    public List<PokemonData> items;
}

public class PokemonDatabase : MonoBehaviour
{
    public TextAsset jsonFile; 
    public List<PokemonData> pokemonList = new List<PokemonData>();

    void Awake()
    {
        if (jsonFile != null)
        {
            string jsonString = "{ \"items\": " + jsonFile.text + "}";
            try 
            {
                PokemonListWrapper wrapper = JsonUtility.FromJson<PokemonListWrapper>(jsonString);
                pokemonList = wrapper.items;
                Debug.Log($"[성공] 총 {pokemonList.Count}마리 로딩 완료!");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"JSON 오류: {e.Message}");
            }
        }
    }

    public PokemonData FindPokemon(string pokemonName)
    {
        foreach (var pokemon in pokemonList)
        {
            if (pokemon.name == pokemonName) return pokemon;
        }
        return null;
    }
}