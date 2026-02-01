using UnityEngine;
using static UnityEngine.Random;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    private Dictionary<string, AudioSource> sfxMap;
    private Dictionary<string, AudioSource> musicMap;

    private AudioSource currentMusic;

    protected override void Awake()
    {
        base.Awake();
        CacheAudioSources();
    }

    private void Start()
    {
        PlayMusic("BallroomBGM");
    }

    private void CacheAudioSources()
    {
        sfxMap = new Dictionary<string, AudioSource>();
        musicMap = new Dictionary<string, AudioSource>();

        // --- CACHE SFX ---
        Transform sfxRoot = transform.Find("SFX");
        if (sfxRoot != null)
        {
            foreach (AudioSource source in sfxRoot.GetComponentsInChildren<AudioSource>())
            {
                sfxMap[source.gameObject.name] = source;
            }
        }
        else
        {
            Debug.LogError("AudioManager: SFX root not found!");
        }

        // --- CACHE MUSIC ---
        Transform musicRoot = transform.Find("Music");
        if (musicRoot != null)
        {
            foreach (AudioSource source in musicRoot.GetComponentsInChildren<AudioSource>())
            {
                source.loop = true;
                musicMap[source.gameObject.name] = source;
            }
        }
        else
        {
            Debug.LogError("AudioManager: Music root not found!");
        }
    }

    // ===================== SFX =====================

    public void PlaySFX(string sfxName, bool randomizePitch = false)
    {
        if (sfxMap.TryGetValue(sfxName, out AudioSource source))
        {
            if (randomizePitch)
                source.pitch = Random.Range(0.8f, 1.2f);
            source.Play();
        }
        else
        {
            Debug.LogWarning($"SFX not found: {sfxName}");
        }
    }

    // ===================== MUSIC =====================

    public void PlayMusic(string musicName)
    {
        if (!musicMap.TryGetValue(musicName, out AudioSource source))
        {
            Debug.LogWarning($"Music not found: {musicName}");
            return;
        }

        if (currentMusic == source)
            return;

        if (currentMusic != null)
            currentMusic.Stop();

        currentMusic = source;
        currentMusic.Play();
    }

    public void StopMusic()
    {
        if (currentMusic != null)
        {
            currentMusic.Stop();
            currentMusic = null;
        }
    }
}
