using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Audio")]
public class SOAudio : ScriptableObject
{
    public List<AudioClip> audioClips = new();
    [Header("Pitch")]
    [Range(-3f, 3f)]
    public float pitch = 1f;
    public bool randomizePith = false;
    [Tooltip("The range to witch the pitch can be randomized")][Range(0f, 1f)]
    public float pitchRange = 0f;
    [Range(0f, 1f)]
    public float volume = .75f;
}
