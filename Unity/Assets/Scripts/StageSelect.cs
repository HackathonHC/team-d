using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour {
	
	private int selectPlayer = 1;
	
	private int boardId;
	private int player1Board;
	private int player2Board;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStageClick(GameObject go)
	{
		if (this.selectPlayer == 3) {
			return;
		}
		
		switch (go.name) {
		case "board1":
			this.boardId = 1;
			break;
		case "board2":
			this.boardId = 2;
			break;
		case "board3":
			this.boardId = 3;
			break;
		case "board4":
			this.boardId = 4;
			break;
		}		
	
		this.transform.Find("board" + this.boardId + "/p" + this.selectPlayer).gameObject.SetActive(true);

		if (this.selectPlayer == 1) {
			this.player1Board = this.boardId;
		} else if (this.selectPlayer == 2) {
			this.player2Board = this.boardId;
			this.transform.Find("submitButton").gameObject.SetActive(true);
		}

		this.selectPlayer++;
	}

	public void OnSubmitClick()
	{
		this.gameObject.SetActive(false);
	}
}
