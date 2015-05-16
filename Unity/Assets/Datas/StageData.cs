using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageData
{
	public enum StageType
	{
		Balance = 1,
		Attack  = 2,
		Defence = 3,
		Gamble  = 4,
	}

	public static Dictionary<Panel.Type, int> GetData(StageType stageType)
	{
		return STAGE_DATA[stageType];
	}

	public static readonly Dictionary<StageType, Dictionary<Panel.Type, int>> STAGE_DATA = new Dictionary<StageType, Dictionary<Panel.Type, int>>()
	{
		{ 
			StageType.Balance,
			new Dictionary<Panel.Type, int>()
			{ 
				{ Panel.Type.Attack,  7 }, 
				{ Panel.Type.Special, 3 }, 
				{ Panel.Type.Damage,  2 }, 
				{ Panel.Type.Dead,    1 }, 
				{ Panel.Type.Recover, 3 }, 
				{ Panel.Type.Rest,    1 }, 
				{ Panel.Type.Rand,    2 }, 
				{ Panel.Type.Non,     3 }, 
			}
		},
		{ 
			StageType.Attack,
			new Dictionary<Panel.Type, int>()
			{ 
				{ Panel.Type.Attack,  6 }, 
				{ Panel.Type.Special, 5 }, 
				{ Panel.Type.Damage,  3 }, 
				{ Panel.Type.Dead,    1 }, 
				{ Panel.Type.Recover, 1 }, 
				{ Panel.Type.Rest,    1 }, 
                { Panel.Type.Rand,    2 }, 
                { Panel.Type.Non,     3 }, 
            }
        },
		{ 
			StageType.Defence,
			new Dictionary<Panel.Type, int>()
			{ 
				{ Panel.Type.Attack,  6 }, 
				{ Panel.Type.Special, 2 }, 
				{ Panel.Type.Damage,  1 }, 
				{ Panel.Type.Dead,    0 }, 
				{ Panel.Type.Recover, 6 }, 
				{ Panel.Type.Rest,    2 }, 
                { Panel.Type.Rand,    2 }, 
                { Panel.Type.Non,     3 }, 
            }
        },
		{ 
			StageType.Gamble,
			new Dictionary<Panel.Type, int>()
			{ 
				{ Panel.Type.Attack,  0 }, 
				{ Panel.Type.Special, 10 }, 
				{ Panel.Type.Damage,  4 }, 
				{ Panel.Type.Dead,    2 }, 
				{ Panel.Type.Recover, 1 }, 
				{ Panel.Type.Rest,    0 }, 
                { Panel.Type.Rand,    5 }, 
                { Panel.Type.Non,     0 }, 
            }
        }
	};

	public static readonly Dictionary<int, Vector3> PANEL_POSITION_MAP = new Dictionary<int, Vector3>()
	{
		{ 0, new Vector3(-420f, 240f) },
		{ 1, new Vector3(-300f, 240f) },
		{ 2, new Vector3(-180f, 240f) },
		{ 3, new Vector3(-60f, 240f) },
		{ 4, new Vector3(60f, 240f) },
		{ 5, new Vector3(180f, 240f) },
		{ 6, new Vector3(300f, 240f) },
		{ 7, new Vector3(420f, 240f) },
		{ 8, new Vector3(420f, 120f) },
		{ 9, new Vector3(420f, 0f) },
		{ 10, new Vector3(420f, -120f) },
		{ 11, new Vector3(420f, -240f) },
		{ 12, new Vector3(300f, -240f) },
		{ 13, new Vector3(180f, -240f) },
		{ 14, new Vector3(60f, -240f) },
		{ 15, new Vector3(-60f, -240f) },
		{ 16, new Vector3(-180f, -240f) },
		{ 17, new Vector3(-300f, -240f) },
		{ 18, new Vector3(-420f, -240f) },
		{ 19, new Vector3(-420f, -120f) },
		{ 20, new Vector3(-420f, 0f) },
		{ 21, new Vector3(-420f, 120f) },
	};

	public static readonly Dictionary<Panel.Type, string> PANEL_SPRITE_NAME_MAP = new Dictionary<Panel.Type, string>()
	{
		{ Panel.Type.Attack,  "panel_attack" }, 
		{ Panel.Type.Special, "panel_special" }, 
		{ Panel.Type.Damage,  "panel_damage" }, 
		{ Panel.Type.Dead,    "panel_dead" }, 
		{ Panel.Type.Recover, "panel_recover" }, 
		{ Panel.Type.Rest,    "panel_rest" }, 
		{ Panel.Type.Rand,    "panel_random" }, 
		{ Panel.Type.Non,     "panel_non" }, 
	};
}
