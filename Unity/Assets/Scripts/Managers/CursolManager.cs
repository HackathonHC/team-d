using UnityEngine;
using System.Collections;

public class CursolManager
{
	private static CursolManager instance;
	public static CursolManager Instance
	{
		get
		{
			return instance ?? (instance = new CursolManager());
		}
	}

	private string CURSOL_PREFAB_PATH = "Prefabs/Cursol";
	private GameObject cursolPrefab;

	public void Init()
	{
		this.cursolPrefab = Resources.Load<GameObject>(CURSOL_PREFAB_PATH);
		foreach (var player in PlayerManager.Instance.PlayerList)
		{
			var playerStage = PanelManager.Instance.FindPlayerStage(player.playerId);
			Debug.Log(playerStage);
			var cursolObject = NGUITools.AddChild(playerStage.panelContainerObject, this.cursolPrefab);
			var cursol = new Cursol();
			cursol.SetUp(player, cursolObject);
			// reference
			player.cursol = cursol;
		}
	}
}
