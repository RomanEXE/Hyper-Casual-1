using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public AudioSource source;

    public string name;

    [Range(0, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
}