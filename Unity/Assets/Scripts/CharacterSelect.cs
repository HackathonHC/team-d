using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

	private GameObject character1;
	private GameObject character2;
	private GameObject character3;

	private Sprite characterImage1;	
	private Sprite characterImage2;
	private Sprite characterImage3;

	private int selectPlayer = 1;

	private Unit unit;
	private Unit player1Unit;
	private Unit player2Unit;

	void Awake() {
		this.character1 = this.transform.Find("character1").gameObject;
		this.character2 = this.transform.Find("character2").gameObject;
		this.character3 = this.transform.Find("character3").gameObject;

		this.characterImage1 = Resources.Load<Sprite>("Sprites/Characters/chara01");
		this.characterImage2 = Resources.Load<Sprite>("Sprites/Characters/chara02");
		this.characterImage3 = Resources.Load<Sprite>("Sprites/Characters/chara03");

		this.transform.Find("p1/nameLabel").GetComponent<UISprite>().spriteName = "";
		this.transform.Find("p1/attack").GetComponent<UISprite>().spriteName = "";
		this.transform.Find("p1/defense").GetComponent<UISprite>().spriteName = "";
		this.transform.Find("p2/nameLabel").GetComponent<UISprite>().spriteName = "";
		this.transform.Find("p2/attack").GetComponent<UISprite>().spriteName = "";
		this.transform.Find("p2/defense").GetComponent<UISprite>().spriteName = "";

		charaSelected(1);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnButtonClick(GameObject go)
	{
		if (this.selectPlayer == 3) {
			return;
		}

		int selectCharaId = 1;
		switch (go.name) {
		case "character1":
			selectCharaId = 1;
			break;

		case "character2":
			selectCharaId = 2;
			break;

		case "character3":
			selectCharaId = 3;
			break;

		}

		charaSelected(selectCharaId);
	}

	private void charaSelected(int charaId)
	{
		switch (charaId) {
		case 1:
			this.character1.GetComponent<UIButton>().defaultColor = Color.white;
			this.character2.GetComponent<UIButton>().defaultColor = Color.gray;
			this.character3.GetComponent<UIButton>().defaultColor = Color.gray;
			
			this.transform.Find("p" + this.selectPlayer + "/nameLabel").GetComponent<UISprite>().spriteName = "charaName01";
			
			this.transform.Find("p" + this.selectPlayer + "/selectCharacter").GetComponent<UI2DSprite>().sprite2D = characterImage1;

			break;
		case 2:
			this.character1.GetComponent<UIButton>().defaultColor = Color.gray;
			this.character2.GetComponent<UIButton>().defaultColor = Color.white;
			this.character3.GetComponent<UIButton>().defaultColor = Color.gray;
			
			this.transform.Find("p" + this.selectPlayer + "/nameLabel").GetComponent<UISprite>().spriteName = "charaName02";

			this.transform.Find("p" + this.selectPlayer + "/selectCharacter").GetComponent<UI2DSprite>().sprite2D = characterImage2;

			break;
		case 3:
			this.character1.GetComponent<UIButton>().defaultColor = Color.gray;
			this.character2.GetComponent<UIButton>().defaultColor = Color.gray;
			this.character3.GetComponent<UIButton>().defaultColor = Color.white;
			
			this.transform.Find("p" + this.selectPlayer + "/nameLabel").GetComponent<UISprite>().spriteName = "charaName03";

			this.transform.Find("p" + this.selectPlayer + "/selectCharacter").GetComponent<UI2DSprite>().sprite2D = characterImage3;
			break;
		}
		this.unit = new Unit(charaId);
		
		switch (this.unit.master.type) {
		case Unit.Type.Attack:
			this.transform.Find("p" + this.selectPlayer + "/attack").GetComponent<UISprite>().spriteName = "rank_a";
			this.transform.Find("p" + this.selectPlayer + "/defense").GetComponent<UISprite>().spriteName = "rank_c";
			break;
			
		case Unit.Type.Balance:
			this.transform.Find("p" + this.selectPlayer + "/attack").GetComponent<UISprite>().spriteName = "rank_b";
			this.transform.Find("p" + this.selectPlayer + "/defense").GetComponent<UISprite>().spriteName = "rank_b";
			break;
			
		case Unit.Type.Defence:
			this.transform.Find("p" + this.selectPlayer + "/attack").GetComponent<UISprite>().spriteName = "rank_c";
			this.transform.Find("p" + this.selectPlayer + "/defense").GetComponent<UISprite>().spriteName = "rank_a";
			break;
		}
	}

	public void OnSubmitButtonClick()
	{	
		this.transform.Find("character" + this.unit.master.id + "/p" + this.selectPlayer).gameObject.SetActive(true);
		if (this.selectPlayer == 1) {
			this.player1Unit = this.unit;
			this.selectPlayer++;
			charaSelected(3);
		} else if (this.selectPlayer == 2) {
			this.player2Unit = this.unit;
			this.selectPlayer++;
			// Hide Character select
			this.gameObject.SetActive(false);
			// Player Parameter
			GameManager.Instance.playerParameter1.unitId = this.player1Unit.master.id;
			GameManager.Instance.playerParameter2.unitId = this.player2Unit.master.id;
			// Show Stage Select
			GameObject.Find("L5").transform.Find("StageSelect").gameObject.SetActive(true);
		}
	}
}
