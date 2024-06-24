using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public class AudioSourceConfig : ICloneable
    {
        public bool loop;

        public AudioSourceConfig(AudioSource audioSource)
        {
            loop = audioSource.loop;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        internal void ApplyToAudioSource(AudioSource source)
        {
            source.loop = loop;
        }
    }

    [Serializable]
    public struct AudioClipStruct
    {
        public string name;
        public AudioClip audioClip;
    }

    [Serializable]
    public struct AudioSourceStruct
    {
        public string name;
        public AudioSource audioSource;
    }

    [SerializeField]
    private AudioSourceStruct[] audioSourceStructs;

    [SerializeField]
    private AudioClipStruct[] audioClipStructs;

    private Dictionary<string, AudioClip> audioClipsByName;
    private Dictionary<string, AudioSource> audioSourcesByName;
    private Dictionary<string, AudioSourceConfig> audioSourceConfigsByName;

    private void Awake()
    {
        audioClipsByName = audioClipStructs.ToDictionary(x => x.name, y => y.audioClip);
        audioSourcesByName = audioSourceStructs.ToDictionary(x => x.name, y => y.audioSource);
        audioSourceConfigsByName = audioSourceStructs.ToDictionary(x => x.name, y => new AudioSourceConfig(y.audioSource));
    }

    public AudioClip GetAudioClipByName(string name) => audioClipsByName[name];
    public AudioSource GetAudioSourceByName(string name) => audioSourcesByName[name];
    public AudioSourceConfig GetAudioSourceConfigByName(string name) => audioSourceConfigsByName[name];

    public void Play(string sourceName, string clipName, Action<AudioSourceConfig, AudioClip> configurator = null, bool interrupt = true)
    {
        var source = GetAudioSourceByName(sourceName);
        var clip = GetAudioClipByName(clipName);
        var config = (AudioSourceConfig)GetAudioSourceConfigByName(sourceName).Clone();

        configurator?.Invoke(config, clip);

        Play(source, clip, config, interrupt);
    }

    public void Stop(string sourceName)
    {
        GetAudioSourceByName(sourceName).Stop();
    }

    public void SpawnTempAudioSourceAndPlay(string sourceName, string clipName, Action<AudioSourceConfig, AudioClip> configurator = null, bool destroy = true)
    {
        var source = GetAudioSourceByName(sourceName);
        var clip = GetAudioClipByName(clipName);
        var config = (AudioSourceConfig)GetAudioSourceConfigByName(sourceName).Clone();

        configurator?.Invoke(config, clip);

        SpawnTempAudioSourceAndPlay(source, clip, config, destroy);
    }

    public UniTask WaitAudioSourceToFinish(string sourceName, bool ignoreTimeScale = false)
    {
        var source = GetAudioSourceByName(sourceName);

        return WaitAudioSourceToFinish(source, ignoreTimeScale);
    }

    public async UniTask WaitToFinishAll(bool ignoreTimeScale = false)
    {
        float maxSecondsToWait = -1;

        for (int i = 0; i < audioSourceStructs.Length; i++)
        {
            var audioSource = audioSourceStructs[i].audioSource;

            if (!IsAudioSourceAwaitable(audioSource)) continue;

            var secondsToWait = audioSource.clip.length - audioSource.time;

            if (secondsToWait > maxSecondsToWait)
            {
                maxSecondsToWait = secondsToWait;
            }
        }

        if (maxSecondsToWait > 0)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(maxSecondsToWait), ignoreTimeScale);
        }
    }

    public static void Play(AudioSource source, AudioClip clip, AudioSourceConfig config = null, bool interrupt = true)
    {
        if (!interrupt && source.isPlaying) return;

        source.clip = clip;

        config?.ApplyToAudioSource(source);

        source.Play();
    }

    public static AudioSource SpawnTempAudioSourceAndPlay(AudioSource source, AudioClip clip, AudioSourceConfig config = null, bool destroy = true)
    {
        var newGameObject = new GameObject("AudioTemp");
        newGameObject.transform.position = source.transform.position;

        var newSource = newGameObject.AddComponent<AudioSource>().GetCopyOf(source);

        Play(newSource, clip, config);

        if (destroy && !newSource.loop) Destroy(newGameObject, clip.length);

        return newSource;
    }

    public static async UniTask WaitAudioSourceToFinish(AudioSource source, bool ignoreTimeScale = false)
    {
        if (!IsAudioSourceAwaitable(source)) return;

        var secondsToWait = source.clip.length - source.time;

        await UniTask.Delay(TimeSpan.FromSeconds(secondsToWait), ignoreTimeScale);
    }

    private static bool IsAudioSourceAwaitable(AudioSource audioSource)
    {
        return !audioSource.loop && audioSource.clip != null && audioSource.isPlaying;
    }
}
