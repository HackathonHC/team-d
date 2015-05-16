using UnityEngine;
using System.Collections;

public class Panel
{
	public enum Type
	{
		Attack  = 1,
		Special = 2,
		Damage  = 3,
		Dead   	= 4,
		Recover = 5,
		Rest  	= 6,
		Rand    = 7,
		Non   	= 8,
	}

	public Type type;
	public GameObject gameObject;
	public UISprite sprite;

	public Panel(Type type)
	{
		this.type = type;
	}

	public void SetUp(GameObject panelObject)
	{
		this.gameObject = panelObject;
		this.sprite = this.gameObject.GetComponent<UISprite>();
		this.sprite.spriteName = GetSpriteNameByPanelType(this.type);
		this.sprite.MakePixelPerfect();
	}

	private string GetSpriteNameByPanelType(Type panelType)
	{
		return StageData.PANEL_SPRITE_NAME_MAP[panelType];
	}
}
