using System;
using UnityEngine;

public class SoundManager
{    
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    // ----- Private
    private AudioSource[] _audioSources = new AudioSource[(int)Define.ESoundType.MAX];

    // --------------------------------------------------
    // Functions - Constructor & Destructor & Event
    // --------------------------------------------------
    public SoundManager()
    {
        AudioSettings.OnAudioConfigurationChanged += OnAudioConfigurationChanged;
    }
        
    ~SoundManager()
    {
        AudioSettings.OnAudioConfigurationChanged -= OnAudioConfigurationChanged;
    }

    private void OnAudioConfigurationChanged(bool deviceWasChanged)
    {
        if (_audioSources.Length == 0)
            return;
            
        var bgmAudioSource = _audioSources[(int)Define.ESoundType.BGM];
            
        if (bgmAudioSource == null || bgmAudioSource.clip == null)
            return;

        var currentBGMClip = bgmAudioSource.clip;
            
        bgmAudioSource.volume = 1f;
        bgmAudioSource.clip = currentBGMClip;
        bgmAudioSource.Play();
        Debug.Log($"[SoundManager.OnAudioConfigurationChanged] Sound Environment Changed. BGM {currentBGMClip.name} Replayed");
    }
    
    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    // ----- Public
    public void Init(Action doneCallback = null)
    {
        var root = GameObject.Find("@Sound");
        
        if (root == null) 
        {
            root = new GameObject { name = "@Sound" };
            UnityEngine.Object.DontDestroyOnLoad(root);

            var soundNames = Enum.GetNames(typeof(Define.ESoundType));
            for (var i = 0; i < soundNames.Length - 1; i++) 
            {
                if (i == (int)Define.ESoundType.MAX) 
                    break;
                
                var go = new GameObject { name = soundNames[i] }; 
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.ESoundType.BGM].loop = true;
            _audioSources[(int)Define.ESoundType.BGM].volume = 1f;
        }
        
        doneCallback?.Invoke();
    }
    
    public void Play(string path, Define.ESoundType type = Define.ESoundType.EFFECT, float pitch = 1.0f, float volume = 1.0f, bool isLoop = false)
    {
        var audioClip = Resources.Load<AudioClip>($"Sounds/{path}");
        if (audioClip == null)
        {
            Debug.Log($"[SoundManager.Play] AudioClip Missing! {path}");
            return;
        }
        
        var audioSource = _audioSources[(int)type];
        audioSource.pitch = pitch;
        if (type == Define.ESoundType.BGM)
        {
            if (audioSource.clip == audioClip)
                return;
                
            if (audioSource.isPlaying)
                audioSource.Stop();
                
            audioSource.volume = volume;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            audioSource.volume = volume;
            if (isLoop)
            {
                audioSource.clip = audioClip;
                audioSource.loop = true;
                audioSource.Play();
            }
            else
                audioSource.PlayOneShot(audioClip);
        }
    }

    public void Stop(Define.ESoundType type)
    {
        var audioSource = _audioSources[(int)type];
        audioSource.Stop();
        audioSource.clip = null;
    }

    public void StopAll()
    {
        foreach (var audioSource in _audioSources)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}