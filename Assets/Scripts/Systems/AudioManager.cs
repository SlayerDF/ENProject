using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
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

    private void Awake()
    {
        audioClipsByName = audioClipStructs.ToDictionary(x => x.name, y => y.audioClip);
        audioSourcesByName = audioSourceStructs.ToDictionary(x => x.name, y => y.audioSource);
    }

    public AudioSource GetAudioSourceByName(string name) => audioSourcesByName[name];
    public AudioClip GetAudioClipByName(string name) => audioClipsByName[name];

    public void Play(string sourceName, string clipName, bool interrupt = true, Action<AudioSource, AudioClip> configurator = null)
    {
        var source = GetAudioSourceByName(sourceName);
        var clip = GetAudioClipByName(clipName);

        Play(source, clip, interrupt, configurator);
    }

    public void Stop(string sourceName)
    {
        GetAudioSourceByName(sourceName).Stop();
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

    public static void Play(AudioSource source, AudioClip clip, bool interrupt = true, Action<AudioSource, AudioClip> configurator = null)
    {
        if (!interrupt && source.isPlaying) return;

        source.clip = clip;

        configurator?.Invoke(source, clip);

        source.Play();
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
