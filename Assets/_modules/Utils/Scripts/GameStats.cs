using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats {
	public static GunsManager _GunsManager { get; set; }
	public static CharacterProperties _CharacterProperties { get; set; }
	public static FPSMovement _FPSMovement { get; set; }
	public static DecalsManager _DecalsManager { get; set; }
	public static GetHurtEffect _GetHurtEffect { get; set; }
	public static MessageNotifier _MessageNotifier { get; set; }
	public static UICharacterProperties _UICharacterProperties { get; set; }
	public static MenuButtonsHandler _MenuButtonsHandler { get; set; }
}
