# 🎮 Unity Pokemon Database Project (WIP)

## 📖 프로젝트 소개
PokeAPI를 활용하여 포켓몬 데이터를 자동으로 수집하고, Unity 엔진에서 JSON 파싱을 통해 도감 UI를 구현하는 프로젝트입니다.
백엔드 데이터 처리(Node.js)와 클라이언트 데이터 연동(Unity C#) 과정을 학습하고 구현하는 데 초점을 맞추었습니다.

## 🛠 사용 기술 (Tech Stack)
* **Engine:** Unity 202x.x (C#)
* **Data Collection:** Node.js, Axios
* **Data Format:** JSON
* **API:** [PokeAPI](https://pokeapi.co/)

## 🚀 주요 구현 기능

### 1. 포켓몬 데이터 크롤러 (Node.js)
* `fetch_all_pokemon.js` 스크립트를 작성하여 PokeAPI의 데이터를 정밀 수집
* **자동화:** 1세대~9세대 및 특수 폼(메가진화, 리전폼, 거다이맥스 등) 자동 분류
* **데이터 가공:** 유니티에서 사용하기 쉬운 JSON 형태로 가공하여 저장 (이름, 타입, 종족값, 기믹 플래그 등)
* **특이사항:** * API의 10000번대 특수 ID 처리 로직 구현
    * '거다이맥스', 'Z기술' 등 최신 기믹 플래그(Flag) 데이터 추가

### 2. Unity 데이터베이스 시스템
* `PokemonDatabase.cs`: 대량의 JSON 데이터를 효율적으로 로딩하고 파싱
* `[System.Serializable]` 클래스 구조 설계를 통한 JSON 매핑 최적화
* 데이터 무결성 검사 및 검색 알고리즘 구현

### 3. 도감 UI (진행 중)
* 포켓몬 이름 검색 기능
* 검색 결과(능력치, 타입, 상세 정보) 시각화
* TextMeshPro(TMP)를 활용한 한글 폰트 적용 및 UI 레이아웃 구성

## 📂 프로젝트 구조