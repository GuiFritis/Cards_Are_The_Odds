using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _songs;
    [SerializeField] private bool _playOnAwake = true;
    private float _volume;

    private void Awake()
    {
        _audioSource.clip = _songs.GetRandom();
        _volume = _audioSource.volume;
        if(_playOnAwake)
        {
            StartMusic();
        }
    }

    private void Update()
    {
        if(_audioSource.clip != null && !_audioSource.isPlaying)
        {
            _audioSource.clip = _songs.GetRandom(_audioSource.clip);
            StartMusic();
        }
    }

    public void StartMusic()
    {
        _audioSource.Play(); 
    }

    private void OnDisable()
    {
        _audioSource.DOKill();
        _audioSource.DOFade(0, 1f).OnComplete(() => _audioSource.Pause());
    }

    private void OnEnable()
    {
        _audioSource.UnPause();
        _audioSource.DOKill();
        _audioSource.DOFade(_volume, 1f);
    }
}
