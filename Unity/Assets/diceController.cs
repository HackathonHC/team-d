using UnityEngine;
using System.Collections;

public class diceController : MonoBehaviour 
{
	private Die_d6 die;
	public int value;
	private int state;

	private Rigidbody rigidbody;

	void Awake() 
	{
		die = gameObject.GetComponent<Die_d6>();
		this.rigidbody = GetComponent<Rigidbody>();
		this.rigidbody.useGravity = false;
		this.state = 0; 

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this.state == 0) {
			this.state = 1;
			StartCoroutine("dice");
		} if (this.state == 1) {
			transform.rotation = Random.rotation;
		} else if (this.state == 2){
			this.rigidbody.useGravity = true;
			this.rigidbody.AddRelativeForce(Vector3.up * 20,
			                        ForceMode.Impulse);
			this.state = 3;
		} else if (this.state == 3) {
			if(this.rigidbody.velocity.magnitude == 0){
				this.state = 4;
			}
		} else if (this.state == 4) {
			value = die.value;
//			Debug.Log(value);
			transform.TransformPoint(0f,0f,0f);
		}
		Debug.Log(transform.localPosition.x);
	}

	IEnumerator dice()
	{
		yield return new WaitForSeconds(1.5f);
		this.state = 2;
	}
}
