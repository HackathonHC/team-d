using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelManager
{
	private static PanelManager instance;
	public static PanelManager Instance
	{
		get
		{
			return instance ?? (instance = new PanelManager());
		}
	}

	public int PANEL_SIZE = 22;
	private string PANEL_PREFAB_PATH = "Prefabs/Panel";
	private UI2DSprite bgSprite;
	private GameObject panelPrefab;
	private Dictionary<int, PlayerStage> playerStageMap;

	public class PlayerStage
	{
		public Player player;
		public List<Panel> panelList;
		public GameObject panelContainerObject;
		public AnimationComponent animationComponent;
	}

	public void Init()
	{
		this.bgSprite = GameObject.Find("UIRoot/L1").transform.Find("Bg").GetComponent<UI2DSprite>();
		this.playerStageMap = new Dictionary<int, PlayerStage>();

		// Create Player's Stage
		foreach (var player in PlayerManager.Instance.PlayerList)
		{
			CreateStage(player);
		}
	}

	public PlayerStage FindPlayerStage(int playerId)
	{
		return this.playerStageMap[playerId];
	}

	private Vector3 GetPositionByIndex(int index)
	{
		return StageData.PANEL_POSITION_MAP[index];
	}

	private void CreateStage(Player player)
	{
		var id = player.stageId;
		var stageType = (StageData.StageType)id;

		// Load Data
		var panelCountMap = StageData.GetData(stageType);

		// Create Player's Stage
		var playerStage = new PlayerStage();
		
		// Create Panel Model
		playerStage.panelList = new List<Panel>(PANEL_SIZE);
		foreach (var panelCountData in panelCountMap)
		{
			var type = panelCountData.Key;
			var count = panelCountData.Value;
			for (var i = 0; i < count; i++)
			{
				var panel = new Panel(type);
				// Reference
				playerStage.panelList.Add(panel);
			}
		}
		
		// Shuffle
		var shuffleList = playerStage.panelList.ToArray().Shuffle();
		playerStage.panelList = new List<Panel>(shuffleList);
		
		// Create Panel Container
		var l1Object = GameObject.Find("L1");
		playerStage.panelContainerObject = new GameObject();
		playerStage.panelContainerObject.transform.SetParent(l1Object.transform);
		playerStage.panelContainerObject.layer = LayerMask.NameToLayer("2DUI");
		playerStage.panelContainerObject.name = string.Format("PanelContainer{0}", player.playerId);
		playerStage.panelContainerObject.transform.localScale = Vector3.one;

		// Add UiPanel for Container
		var uiPanel = playerStage.panelContainerObject.AddComponent<UIPanel>();
		uiPanel.depth = 1; // TODO: Fix

		// Add Animation Component
		playerStage.animationComponent = playerStage.panelContainerObject.AddMissingComponent<AnimationComponent>();

		// Set Scene
		this.panelPrefab = Resources.Load<GameObject>(PANEL_PREFAB_PATH);
		for(var i = 0; i < playerStage.panelList.Count; i++)
		{
			var panel = playerStage.panelList[i];
			var panelObject = NGUITools.AddChild(playerStage.panelContainerObject, this.panelPrefab);
			// Set Reference
			panel.SetUp(panelObject);
			// Set Position
			panel.gameObject.transform.localPosition = GetPositionByIndex(i);
			// Set GameObject Name
			panel.gameObject.name = i.ToString();
		}

		// Set Reference
		if (!this.playerStageMap.ContainsKey(player.playerId))
		{
			this.playerStageMap.Add(player.playerId, playerStage);
		}
		player.playerStage = playerStage;
	}
}
