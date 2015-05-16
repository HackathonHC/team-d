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

	private int PANEL_SIZE = 22;
	private string PANEL_PREFAB_PATH = "Prefabs/Panel";
	private UI2DSprite bgSprite;
	private List<Panel> panelList;
	private GameObject panelContainerObject;
	private GameObject panelPrefab;

	public void Init()
	{
		this.bgSprite = GameObject.Find("UIRoot/L1").transform.Find("Bg").GetComponent<UI2DSprite>();
		this.bgSprite.sprite2D = Resources.Load<Sprite>("Sprites/Bg/bg1");
		this.bgSprite.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f); // TODO FIX

		// Load Data
		var panelCountMap = StageData.GetData(StageData.StageType.Balance);

		// Create Panel Model
		this.panelList = new List<Panel>(PANEL_SIZE);
		foreach (var panelCountData in panelCountMap)
		{
			var type = panelCountData.Key;
			var count = panelCountData.Value;
			for (var i = 0; i < count; i++)
			{
				var panel = new Panel(type);
				// Reference
				this.panelList.Add(panel);
			}
		}

		// Shuffle
		var shuffleList = this.panelList.ToArray().Shuffle();
		this.panelList = new List<Panel>(shuffleList);

		// Create Panel Container
		var l1Object = GameObject.Find("L1");
		this.panelContainerObject = new GameObject();
		this.panelContainerObject.transform.SetParent(l1Object.transform);
		this.panelContainerObject.layer = LayerMask.NameToLayer("2DUI");
		this.panelContainerObject.name = "PanelContainer";
		this.panelContainerObject.transform.localScale = Vector3.one;

		// Set Scene
		this.panelPrefab = Resources.Load<GameObject>(PANEL_PREFAB_PATH);
		for(var i = 0; i < this.panelList.Count; i++)
		{
			var panel = this.panelList[i];
			var panelObject = NGUITools.AddChild(this.panelContainerObject, this.panelPrefab);
			// Set Reference
			panel.SetUp(panelObject);
			// Set Position
			panel.gameObject.transform.localPosition = GetPositionByIndex(i);
			// Set GameObject Name
			panel.gameObject.name = i.ToString();
		}
	}

	private Vector3 GetPositionByIndex(int index)
	{
		return StageData.PANEL_POSITION_MAP[index];
	}
}
