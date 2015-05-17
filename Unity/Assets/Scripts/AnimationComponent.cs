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

	public Promises.Deferred Move(float time, Vector3 from, Vector3 to)
	{
		var deferred = new Promises.Deferred();
		this.gameObject.transform.localPosition = from;
		iTween.MoveTo(this.gameObject, iTween.Hash(
			"position", to, 
			"time", time,
			"delay", 0.1f, 
			"easetype", "easeOutSine", 
			"oncomplete", "OnTweenComplete",
			"islocal", true,
			"oncompleteparams", deferred
            )
        );
		return deferred;
	}

	public Promises.Deferred FadeIn()
	{
		var deferred = new Promises.Deferred();
		var time = 1f;
		iTween.ValueTo(this.gameObject, iTween.Hash(
			"from", 0f, 
			"to", 1f, 
			"time", time,
			"easetype", "easeOutSine",
			"onupdate", "OnFadeUpdate",
			"oncomplete", "OnTweenComplete",
			"islocal", true,
			"oncompleteparams", deferred
			)
		              );
		return deferred;
	}

	public Promises.Deferred FadeOut()
	{
		var deferred = new Promises.Deferred();
		var time = 1f;
		iTween.ValueTo(this.gameObject, iTween.Hash(
			"from", 1f, 
			"to", 0f, 
			"time", time,
			"easetype", "easeOutSine",
			"onupdate", "OnFadeUpdate",
			"oncomplete", "OnTweenComplete",
			"islocal", true,
			"oncompleteparams", deferred
			)
		               );
		return deferred;
	}
	
    public void OnFadeUpdate(float value)
	{
		var uiPanel = GetComponent<UIPanel>();
		if (uiPanel != null)
		{
			uiPanel.alpha = value;
		}
	}
    
    public void OnTweenComplete(Promises.Deferred deferred)
	{
		deferred.Resolve();
	}
}
