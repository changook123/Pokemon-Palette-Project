# 🎨 Pokemon Palette (Unity 2D Project)

유니티(Unity 6)로 제작 중인 포켓몬스터 스타일의 2D 게임 프로젝트입니다.
고전 게임의 UI 감성을 살리면서, 부드러운 화면 전환과 효율적인 씬 관리 시스템을 구축하는 데 중점을 두었습니다.

## 🛠️ 개발 환경 (Tech Stack)
- **Engine:** Unity 6 (6000.3.2f1)
- **Language:** C# (Scripting)
- **VCS:** Git & GitHub
- **Platform:** PC (Windows)

## ✨ 현재 구현된 핵심 기능 (Current Features)

### 1. 타이틀 & 메뉴 시스템 (Title & Menu)
- **데이터 기반 오프닝:** `PlayerPrefs`를 활용하여 저장된 데이터 유무에 따라 '새로 하기' 또는 '이어 하기'로 분기 처리.
- **UI 애니메이션:** `Mathf.PingPong`을 활용한 부드러운 텍스트/이미지 깜빡임(Blink) 효과 구현.
- **키보드 인터랙션:** 방향키와 엔터키를 이용한 직관적인 메뉴 조작 및 스프라이트 교체(Highlight) 효과.

### 2. 고급 화면 전환 시스템 (Transition System)
- **싱글톤(Singleton) 패턴:** `TransitionManager`를 구현하여 게임 전체에서 유일한 전환 관리자로 동작.
- **페이드 인/아웃(Fade In/Out):** `CanvasGroup`의 Alpha 값을 코루틴(Coroutine)으로 제어하여 끊김 없는 화면 전환 연출.
- **Action 콜백 활용:** 단순 씬 이동뿐만 아니라, 화면이 어두워진 후 특정 함수(예: 게임 종료, 패널 열기)가 실행되도록 확장성 있게 구현.

### 3. 화면 비율 대응 (Resolution)
- **Camera Ratio:** 기기 해상도와 상관없이 고정된 종횡비(16:9 등)를 유지하도록 레터박스(Letterbox) 처리.

### 4. 입력 시스템 (Input)
- **Input System 호환:** Legacy Input과 New Input System을 모두 지원하도록 설정하여 확장성 확보.

## 📂 프로젝트 구조 (Scripts)
Assets/Scripts
├── Controllers
│   ├── TitleController.cs    # 타이틀 화면 로직 (데이터 체크 및 씬 분기)
│   └── MenuController.cs     # 메뉴 선택 및 버튼 기능 매핑
├── Managers
│   └── TransitionManager.cs  # [Singleton] 화면 페이드 효과 및 씬 이동 관리
├── UI
│   └── BlinkingEffect.cs     # 텍스트 및 UI 깜빡임 연출 (Mathf.Sin/PingPong)
└── Utils
    └── CameraRatio.cs        # 화면 해상도 고정 비율 유지

## 📅 추후 개발 계획 (Roadmap)

### ✅ Phase 1: 기반 시스템 구축 (완료)
- [x] 타이틀 및 메뉴 UI 네비게이션
- [x] 화면 전환(Fade) 매니저 구현
- [x] 씬(Scene) 이동 구조 설계

### 🚀 Phase 2: 콘텐츠 확장 (개발 예정)
- [ ] **옵션(Option) 기능 개발**: 사운드 볼륨 조절, 텍스트 속도 조절 등 세부 설정 구현.
- [ ] **게임 튜토리얼(Tutorial) 개발**: 조작법 안내 및 기초 게임플레이 흐름 구현.
- [ ] 인게임 캐릭터 이동 및 상호작용 시스템.

---
*이 프로젝트는 개인 포트폴리오 및 학습 목적으로 제작되었습니다.*