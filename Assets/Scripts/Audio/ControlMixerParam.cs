using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class ControlMixerParam : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _param;
    [SerializeField] private float _value;
    [SerializeField] private float _duration;

    public void ChangeMixerParam()
    {
        StartCoroutine(FadeParam());
    }

    private IEnumerator FadeParam()
    {
        _mixer.GetFloat(_param, out float initialValue);
        float time = 0;
        while(time < _duration)
        {
            _mixer.SetFloat(_param, Mathf.Lerp(initialValue, _value, time/_duration));
            yield return new WaitForSeconds(0.05f);
            time += 0.05f;
        }
        _mixer.SetFloat(_param, _value);

    }
}
