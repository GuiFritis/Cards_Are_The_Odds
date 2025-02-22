using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    [SerializeField] private SOAudio soAudio;

    public void PlayAudioClip()
    {
        SFX_Pool.Instance.Play(soAudio);
    }
}
