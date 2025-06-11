using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

public class GlobalKeyHook : MonoSingleton<GlobalKeyHook> {
	private Timer _timer;
	private float _time;
	private int _keysDown;
	private static int[] BUTTONS;
	private static bool[] WasPressed;
	private static bool[] IsDown;
	private const int BUTTONDOWN = -32768;
	private const int BUTTONUP = 0;
	public Action<int> OnKeyPressed;

	[DllImport("user32.dll")]
	private static extern short GetAsyncKeyState(int virtualKeyCode);

	private void Awake() {
		Array values = Enum.GetValues(typeof(Keys));
		BUTTONS = new int[values.Length];
		for (int i = 0; i < values.Length; i++) {
			BUTTONS[i] = (int)values.GetValue(i);
		}

		WasPressed = new bool[BUTTONS.Length];
		IsDown = new bool[BUTTONS.Length];
	}

	void Start() {
		_timer = new Timer(delegate { Process(); }, null, 0, 16);
	}

	private void OnDestroy() {
		_timer?.Dispose();
	}

	private void Update() {
		if (_keysDown != 0) {
			OnKeyPressed?.Invoke(_keysDown);
			_keysDown = 0;
		}
	}

	private void Process() {
		for (int i = 0; i < BUTTONS.Length; i++) {
			IsDown[i] = false;
			short asyncKeyState = GetAsyncKeyState(BUTTONS[i]);
			if (!WasPressed[i] && asyncKeyState == -32768) {
				WasPressed[i] = true;
				IsDown[i] = true;
			} else if (WasPressed[i] && asyncKeyState == 0) {
				WasPressed[i] = false;
			}
		}

		if (GetAnyButtonDown()) {
			_keysDown += Enumerable.Count(IsDown, (bool x) => x);
		}
	}

	private bool GetAnyButtonDown() {
		for (int i = 0; i < BUTTONS.Length; i++) {
			if (IsDown[i]) {
				return true;
			}
		}

		return false;
	}
}

public enum Keys {
	LeftMouseBtn = 1,
	RightMouseBtn = 2,
	CtrlBrkPrcs = 3,
	MidMouseBtn = 4,
	ThumbForward = 5,
	ThumbBack = 6,
	BackSpace = 8,
	Tab = 9,
	Clear = 12,
	Enter = 13,
	Pause = 19,
	CapsLock = 20,
	Kana = 21,
	Junju = 23,
	Final = 24,
	Hanja = 25,
	Escape = 27,
	Convert = 28,
	NonConvert = 29,
	Accept = 30,
	ModeChange = 31,
	Space = 32,
	PageUp = 33,
	PageDown = 34,
	End = 35,
	Home = 36,
	LeftArrow = 37,
	UpArrow = 38,
	RightArrow = 39,
	DownArrow = 40,
	Select = 41,
	Execute = 43,
	PrintScreen = 44,
	Insert = 45,
	Delete = 46,
	Help = 47,
	Num0 = 48,
	Num1 = 49,
	Num2 = 50,
	Num3 = 51,
	Num4 = 52,
	Num5 = 53,
	Num6 = 54,
	Num7 = 55,
	Num8 = 56,
	Num9 = 57,
	A = 65,
	B = 66,
	C = 67,
	D = 68,
	E = 69,
	F = 70,
	G = 71,
	H = 72,
	I = 73,
	J = 74,
	K = 75,
	L = 76,
	M = 77,
	N = 78,
	O = 79,
	P = 80,
	Q = 81,
	R = 82,
	S = 83,
	T = 84,
	U = 85,
	V = 86,
	W = 87,
	X = 88,
	Y = 89,
	Z = 90,
	LeftWin = 91,
	RightWin = 92,
	Apps = 93,
	Sleep = 95,
	Numpad0 = 96,
	Numpad1 = 97,
	Numpad2 = 98,
	Numpad3 = 99,
	Numpad4 = 100,
	Numpad5 = 101,
	Numpad6 = 102,
	Numpad7 = 103,
	Numpad8 = 104,
	Numpad9 = 105,
	Multiply = 106,
	Add = 107,
	Separator = 108,
	Subtract = 109,
	Decimal = 110,
	Divide = 111,
	F1 = 112,
	F2 = 113,
	F3 = 114,
	F4 = 115,
	F5 = 116,
	F6 = 117,
	F7 = 118,
	F8 = 119,
	F9 = 120,
	F10 = 121,
	F11 = 122,
	F12 = 123,
	F13 = 124,
	F14 = 125,
	F15 = 126,
	F16 = 127,
	F17 = 128,
	F18 = 129,
	F19 = 130,
	F20 = 131,
	F21 = 132,
	F22 = 133,
	F23 = 134,
	F24 = 135,
	NavigationView = 136,
	NavigationMenu = 137,
	NavigationUp = 138,
	NavigationDown = 139,
	NavigationLeft = 140,
	NavigationRight = 141,
	NavigationAccept = 142,
	NavigationCancel = 143,
	NumLock = 144,
	ScrollLock = 145,
	NumpadEqual = 146,
	FJ_Jisho = 146,
	FJ_Masshou = 147,
	FJ_Touroku = 148,
	FJ_Loya = 149,
	FJ_Roya = 150,
	LeftShift = 160,
	RightShift = 161,
	LeftCtrl = 162,
	RightCtrl = 163,
	LeftMenu = 164,
	RightMenu = 165,
	BrowserBack = 166,
	BrowserForward = 167,
	BrowserRefresh = 168,
	BrowserStop = 169,
	BrowserSearch = 170,
	BrowserFavorites = 171,
	BrowserHome = 172,
	VolumeMute = 173,
	VolumeDown = 174,
	VolumeUp = 175,
	NextTrack = 176,
	PrevTrack = 177,
	Stop = 178,
	PlayPause = 179,
	Mail = 180,
	MediaSelect = 181,
	App1 = 182,
	App2 = 183,
	OEM1 = 186,
	Plus = 187,
	Comma = 188,
	Minus = 189,
	Period = 190,
	OEM2 = 191,
	OEM3 = 192,
	Gamepad_A = 195,
	Gamepad_B = 196,
	Gamepad_X = 197,
	Gamepad_Y = 198,
	GamepadRightBumper = 199,
	GamepadLeftBumper = 200,
	GamepadLeftTrigger = 201,
	GamepadRightTrigger = 202,
	GamepadDPadUp = 203,
	GamepadDPadDown = 204,
	GamepadDPadLeft = 205,
	GamepadDPadRight = 206,
	GamepadMenu = 207,
	GamepadView = 208,
	GamepadLeftStickBtn = 209,
	GamepadRightStickBtn = 210,
	GamepadLeftStickUp = 211,
	GamepadLeftStickDown = 212,
	GamepadLeftStickRight = 213,
	GamepadLeftStickLeft = 214,
	GamepadRightStickUp = 215,
	GamepadRightStickDown = 216,
	GamepadRightStickRight = 217,
	GamepadRightStickLeft = 218,
	OEM4 = 219,
	OEM5 = 220,
	OEM6 = 221,
	OEM7 = 222,
	OEM8 = 223,
	OEMAX = 225,
	OEM102 = 226,
	ICOHelp = 227,
	ICO00 = 228,
	ProcessKey = 229,
	OEMCLEAR = 230,
	Packet = 231,
	OEMReset = 233,
	OEMJump = 234,
	OEMPA1 = 235,
	OEMPA2 = 236,
	OEMPA3 = 237,
	OEMWSCtrl = 238,
	OEMCusel = 239,
	OEMAttn = 240,
	OEMFinish = 241,
	OEMCopy = 242,
	OEMAuto = 243,
	OEMEnlw = 244,
	OEMBackTab = 245,
	Attn = 246,
	CrSel = 247,
	ExSel = 248,
	EraseEOF = 249,
	Play = 250,
	Zoom = 251,
	NoName = 252,
	PA1 = 253,
	OEMClear = 254
}