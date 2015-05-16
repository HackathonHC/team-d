using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	private AnimationManager animationManager;

	void Awake ()
	{
		Init();
	}

	private void Init()
	{
		PlayerManager.Instance.Init();
		AnimationManager.Instance.Init();
	}
}
