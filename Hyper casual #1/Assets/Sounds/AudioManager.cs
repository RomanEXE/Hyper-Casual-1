using System.Collections;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [SerializeField] private string[] _tracks;
    [SerializeField] private int _currentTrack;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    private void Start()
    {
        SelectNextTrack();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
            return;

        s.source.Play();
    }

    private void SelectNextTrack()
    {
        int _nextTrack = UnityEngine.Random.Range(0, _tracks.Length);

        if (_nextTrack != _currentTrack)
        {
            Play(_tracks[_nextTrack]);
            _currentTrack = _nextTrack;

            foreach (Sound s in sounds)
            {
                if (s.name != _tracks[_currentTrack])
                {
                    StartCoroutine(PlayingTrack(s.clip.length));
                }
                else
                {
                    SelectNextTrack();
                }
            }
        }
    }

    IEnumerator PlayingTrack(float trackPlayingTime)
    {
        yield return new WaitForSeconds(trackPlayingTime);
        StopCoroutine(PlayingTrack(0f));
        SelectNextTrack();
    }
}