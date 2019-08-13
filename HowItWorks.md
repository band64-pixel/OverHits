# How It Works
## Overwatch AimBOT 'OverHits'

## in v1.0 b10 ~ v7.8.1 b11
First, OverHits get screenshots from Windows GDI+. (using graphics.CopyFromScreen()) AutoItX3 scans pixels if its color is red. 
If pixel's color is red, call mouse_event() (imports dll from user32.dll, operation required with Position x y). then your characters aims enemy's head automatically.

## in v7.8.2 b12
![OverHits v7.8.2 b12 Ins. 1 [English]](https://i.imgur.com/hK5qrY1.png)
![OverHits v7.8.2 b12 Ins. 2 [English]](https://i.imgur.com/ERCUgox.png)
First, OverHits gets screenshots from DirectX hooks. (using Pointer, Process Hooking, etc..) it scans pixels if its color is red from 'IsRed' Function. 
(Please reference [this repository](https://github.com/jpxue/Overwatch-Aim-Assist)) If pixel's color is red,
call mouse_event() (imports dll from user32.dll, operation required with x y pos). then your characters aims enemy's head automatically.

# How Overwatch detect AimBot users

### Please reference [this issue](https://github.com/jpxue/Overwatch-Aim-Assist/issues/30). then you will comportable to read this.
![AutoMouseAPI_1-en](https://i.imgur.com/TrKep6m.png)

Overwatch detects mouse_event() function with some complex hooks. For example, mouse_event() function use SendInput() function.
it detects SendInput() function with hooks.

If it hooked these functions, it makes black the screen while capturing screenshots.
Then, your profile will be recorded as 'Hack User' in log. If this method is repeated until a certain number is reached, you will be ban in it.

## How To Bypass it?

In [this issue](https://github.com/jpxue/Overwatch-Aim-Assist/issues/30),

1. Go internal and capture from the backbuffer. Advantages: much faster than GDI, you can capture almost every frame with minimal overhead, you can still use mouse_event or sendinput.
Inject a DLL -> Present Hook -> obtain a copy from the BackBuffer. You can process that screenshot internally or you can share it between processes (IPC) by means of a shared memory space and process it through another app (involves some work but is the preferred way).

2. You can implement a Mouse Input Driver and generate input directly from the HID stack. Windows Driver Samples. I never really bothered with this because the process of installing drivers for testing is an abysmal headache (even on VMs using various exploits/driver loaders which never seem to work because of Windows updates 😠).

3. Inject and call SetWindowDisplayAffinity from the Overwatch processes.

### Enjoy! 🗡
