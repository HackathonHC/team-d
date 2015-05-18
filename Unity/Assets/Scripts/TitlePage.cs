using UnityEngine;
using System.Collections;

public class TitlePage : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartClick()
	{
		// Hide Title
		this.gameObject.SetActive(false);
		// Show Chara Select
		GameObject.Find("L5").transform.Find("CharacterSelect").gameObject.SetActive(true);
	}
}
