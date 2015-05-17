using UnityEngine;
using System.Collections;

public class TimerCallback : MonoBehaviour
{
    [SerializeField] public float time;
    [SerializeField] public Types type;
    [SerializeField] public bool oneShot;

    private bool isLocked;
    private float createdAt;
    private System.Action customCallback;

    public enum Types
    {
        None,
        DisableParticleLoop,
        DestroySelf,
        Custom,
    }

    private float CurrentTime
    {
        get { return Time.time; }
    }

    private float Progress
    {
        get { return this.CurrentTime - this.createdAt; }
    }

    public void Start()
    {
        this.createdAt = this.CurrentTime;
        this.isLocked = false;
    }

    public void Reset()
    {
        this.createdAt = this.CurrentTime;
        this.isLocked = false;
    }

    public void SetCustomCallback(System.Action callback)
    {
        this.type = Types.Custom;
        this.customCallback = callback;
    }

    public void Update()
    {
        if (this.isLocked)
        {
            return;
        }
        if (this.Progress >= this.time)
        {
            OnTimerCallback();
        }
    }

    private void OnTimerCallback()
    {
        switch (this.type)
        {
            case Types.None:
                break;
            case Types.DisableParticleLoop:
                DisableParticleLoop();
                break;
            case Types.DestroySelf:
                DestroySelf();
                break;
            case Types.Custom:
                CustomCallback();
                break;
            default:
                break;
        }
        if (this.oneShot)
        {
            Lock();
        }
    }

    private void DisableParticleLoop()
    {
        var attachedParticleSystem = this.GetComponent<ParticleSystem>();
        if (attachedParticleSystem != null)
        {
            attachedParticleSystem.loop = false;
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    private void CustomCallback()
    {
        this.customCallback();
    }

    private void Lock()
    {
        this.isLocked = true;
    }
}
