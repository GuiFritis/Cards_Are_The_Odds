using UnityEngine;
using Utils;

public class SFX_Pool : PoolBase<AudioPlayer, SFX_Pool>
{
    public void Play(AudioClip clip) 
    {
        if(clip != null)
        {
            AudioPlayer audioPlayer = GetPoolItem();
            audioPlayer.pool = this;
            audioPlayer.PlayClip(clip);
        }
    }

    public void Play(SOAudio soAudio)
    {
        AudioPlayer audioPlayer = GetPoolItem();
        audioPlayer.pool = this;
        audioPlayer.PlayAudioSO(soAudio);
        
    }
}