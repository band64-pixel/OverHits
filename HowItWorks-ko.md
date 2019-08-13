# OverHits 프로젝트 정리
## 오버워치 에임핵 (OverHits) 원리

## v1.0 b10 ~ v7.8.1 b11
먼저, OverHits는 Windows GDI+를 통해 스크린샷을 캡처합니다. (graphics.CopyFromScreen() 사용) AutoItX3은 픽셀을 스캔하고 그 픽셀의 색이 빨간색인지 확인후,
만약 그 픽셀의 색이 빨간색이라면, mouse_event()를 호출합니다. (user32.dll에서 호출, x y 포인트의 연산 필요) 그러면 자동으로 적의 헤드를 조준하게 됩니다.

## in v7.8.2 b12
![OverHits v7.8.2 b12 Ins. 1 [Korean]](https://i.imgur.com/kblNu9M.png)
![OverHits v7.8.2 b12 Ins. 2 [English]](https://i.imgur.com/ERCUgox.png)
먼저, OverHits는 DirectX 훅을 통해 스크린샷을 캡처합니다. (포인터, 프로세스 후킹 등을 사용) 그리고 'IsRed' 함수를 통해 한 픽셀의 색이 빨간색인지 확인합니다.
([이 레포지토리](https://github.com/jpxue/Overwatch-Aim-Assist)를 참고해주세요)
만약 그 픽셀의 색이 빨간색이라면, mouse_event()를 호출합니다. (user32.dll에서 호출, x y 포인트의 연산 필요) 그러면 자동으로 적의 헤드를 조준하게 됩니다.

# 오버워치가 에임핵 유저를 잡는 원리

### [이 이슈](https://github.com/jpxue/Overwatch-Aim-Assist/issues/30)를 참고해주세요. 읽으면 이 텍스트들을 읽기 좀 더 쉬워질 겁니다.
### [이 자료](https://www.kdata.or.kr/info/info_04_view.html?field=&keyword=&type=techreport&page=128&dbnum=128563)도 참고해주세요.
### [정말 도움이 되는 글](https://crefunx.tistory.com/13)도 참고해주시면 감사하겠습니다..!
![AutoMouseAPI_1-ko](http://www.dbguide.net/images/know/tech/091203_cc3.jpg)

오버워치는 좀 복잡한 훅을 통해 mouse_event() 함수를 감지합니다. 예를 들면, mouse_evnet 함수는 SendInput() 함수를 사용하죠.
오버워치는 훅을 통해 SendInput() 함수를 감지합니다.

만약 오버워치가 이 함수들을 훅했다면, 오버워치는 스크린샷을 캡처할 동안 화면을 검게 만듭니다.
그러면, 당신의 프로파일은 로그에 '핵 유저'로 기록될 겁니다. 만약 이 방법이 특정한 수에 도달하면, 당신은 오버워치 밴을 당할 겁니다.

## 어떻게 우회를 하죠?

[이 이슈](https://github.com/jpxue/Overwatch-Aim-Assist/issues/30)에 따르면,

1. 내부로 이동하여 백 버퍼에서 캡처합니다. 장점 : GDI보다 훨씬 빠르므로 최소한의 오버 헤드로 거의 모든 프레임을 캡처 할 수 있으며 mouse_event 또는 sendinput을 계속 사용할 수 있습니다.
DLL 삽입-> 현재 후크-> BackBuffer에서 사본을 확보하십시오. 스크린샷을 내부적으로 처리하거나 공유 메모리 공간을 통해 프로세스 (IPC)간에 공유하고 다른 앱을 통해 처리 할 수 있습니다 (일부 작업이 필요하지만 선호되는 방법).

2. 마우스 입력 드라이버를 구현하고 HID 스택에서 직접 입력을 생성 할 수 있습니다. Windows 드라이버 샘플. 테스트를 위해 드라이버를 설치하는 프로세스가 심하게 골치 아프기 때문에 이 문제를 전혀 신경 쓰지 않았습니다 (Windows 업데이트로 인해 작동하지 않는 다양한 익스플로잇 / 드라이버 로더를 사용하는 VM에서도 마찬가지 입니다 😠).

3. Overwatch 프로세스에서 SetWindowDisplayAffinity를 주입하고 호출하십시오.

### Enjoy! 🗡
