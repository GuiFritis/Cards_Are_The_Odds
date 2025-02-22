using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource1;
    [SerializeField] private AudioSource _audioSource2;

    private void Start()
    {
        _audioSource1.loop = false;
        _audioSource1.Play();
    }

    private void Update()
    {
        if(_audioSource1.clip != null && _audioSource1.time >= _audioSource1.clip.length - (Time.deltaTime * 3))
        {
            _audioSource2.loop = true;
            _audioSource2.Play();
            enabled = false;
        }
    }
}
