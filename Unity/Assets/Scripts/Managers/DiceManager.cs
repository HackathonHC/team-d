using UnityEngine;
using System.Collections;

public class DiceManager : MonoBehaviour 
{
	public enum State
	{
		init = 0,
		wait = 1,
		start = 2,
		spin = 3,
		roleStart = 4,
		roleWait = 5,
		roleEnd = 6,
	}

	private Die_d6 die;
	public int value;
	private State state;

//	private Rigidbody rigidbody;

	private static DiceManager instance;
	public static DiceManager Instance
	{
		get
		{
			return instance ?? (instance = GameObject.Find("DiceCamera").transform.Find("d6").GetComponent<DiceManager>());
		}
	}

	public void diceRole()
	{
		if (this.state == State.wait) {
			this.state = State.start;
		}
		if (this.state == State.roleEnd) {
			this.state = State.init;
		}
	}

	void Awake() 
	{
		die = gameObject.GetComponent<Die_d6>();
		//this.GetComponent<Rigidbody>() = GetComponent<Rigidbody>();
		//this.rigidbody.useGravity = false;
		this.state = State.wait; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (this.state) {
		case State.init:
			this.state = State.wait;
			transform.localPosition = new Vector3(0.3f,0f,-10f);
			transform.localRotation = Quaternion.Euler(-75f,0f,0f);
			break;

		case State.wait:
			break;

		case State.start:
			transform.localPosition = new Vector3(0.3f,0f,4.1f);
			StartCoroutine("diceRoleWait");
			this.state = State.spin;
			break;

		case State.spin:
			transform.rotation = Random.rotation;
			break;

		case State.roleStart:
			//this.rigidbody.useGravity = true;
			this.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * 20,
			                                ForceMode.Impulse);
			this.state = State.roleWait;
			break;

		case State.roleWait:
			if(this.GetComponent<Rigidbody>().velocity.magnitude == 0){
				if (die.value > 0) {
					this.state = State.roleEnd;
				} else {
					this.state = State.roleStart;
				}
			}
			break;

		case State.roleEnd:
			value = die.value;
//			Debug.Log(value);
			break;
		}

//		Debug.Log (this.state);
	}

	IEnumerator diceRoleWait()
	{
		yield return new WaitForSeconds(1f);
		this.state = State.roleStart;
	}
}
