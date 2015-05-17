using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
	private static SoundManager instance;
	public static SoundManager Instance
	{
		get
		{
			return instance ?? (instance = GameObject.Find("SoundManager").GetComponent<SoundManager>());
		}
	}

	private AudioSource bgmSource;
	public AudioSource BgmSource
	{
		get
		{
			if (bgmSource == null)
			{
				bgmSource = Instance.transform.Find("BGM").GetComponent<AudioSource>();
			}
			return bgmSource;
		}
	}
	
	public void PlayBGM(string bgmClipName)
	{
		var audioClip =  Resources.Load<AudioClip>(string.Format("Sounds/BGM/{0}", bgmClipName));
		this.BgmSource.clip = audioClip;
		this.BgmSource.Play();
	}
	
	public void StopBGM()
	{
		this.BgmSource.Stop();
	}

}

