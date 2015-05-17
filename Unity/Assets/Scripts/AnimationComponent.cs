using UnityEngine;
using System.Collections;
using Promises;

public class AnimationComponent : MonoBehaviour
{
	public Promises.Deferred SlideIn(float time, Vector3 from, Vector3 to)
	{
		var deferred = new Promises.Deferred();
		this.gameObject.transform.localPosition = from;
		iTween.MoveTo(this.gameObject, iTween.Hash(
			"position", to, 
			"time", time,
			"easetype", "easeOutSine", 
			"oncomplete", "OnTweenComplete",
			"islocal", true,
            "oncompleteparams", deferred
            )
        );
		return deferred;
	}

	public Promises.Deferred SlideOut(float time, Vector3 from, Vector3 to)
	{
		var deferred = new Promises.Deferred();
		this.gameObject.transform.localPosition = from;
		iTween.MoveTo(this.gameObject, iTween.Hash(
			"position", to, 
			"time", time,
			"delay", 1f, 
			"easetype", "easeOutSine", 
			"oncomplete", "OnTweenComplete",
			"islocal", true,
			"oncompleteparams", deferred
            )
        );
		return deferred;
    }
    
    public void OnTweenComplete(Promises.Deferred deferred)
	{
		deferred.Resolve();
	}
}
