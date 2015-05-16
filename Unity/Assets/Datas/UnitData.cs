using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitData
{
	public static Dictionary<string, object> GetData(int unitId)
	{
		return UNIT_DATA[unitId];
	}

	public static readonly Dictionary<int, Dictionary<string, object>> UNIT_DATA = new Dictionary<int, Dictionary<string, object>>()
	{
		{ 
			0, // Dummy
			new Dictionary<string, object>()
			{ 
				{ "id",    	 0   }, 
				{ "hp",      600 }, 
				{ "attack",  200 }, 
				{ "type",    Unit.Type.Attack }, 
				{ "name",    "0"}, 
			}
		},
		{ 
			1, // Attack
			new Dictionary<string, object>()
			{ 
				{ "id",    	 1   }, 
				{ "hp",      600 }, 
				{ "attack",  200 }, 
				{ "type",    Unit.Type.Attack }, 
				{ "name",    "1"}, 
			}
		},
		{ 
			2, // Balance
			new Dictionary<string, object>()
			{ 
				{ "id",    	 2   }, 
				{ "hp",      900 }, 
				{ "attack",  150 }, 
				{ "type",    Unit.Type.Balance }, 
				{ "name",    "2"}, 
			}
		},
		{
			3, // Defence
			new Dictionary<string, object>()
			{ 
				{ "id",    	 3   }, 
				{ "hp",      600 }, 
				{ "attack",  100 }, 
				{ "type",    Unit.Type.Defence }, 
				{ "name",    "3"}, 
			}
		},
	};
}
