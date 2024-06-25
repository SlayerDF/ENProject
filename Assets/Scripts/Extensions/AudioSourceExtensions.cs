using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public static class AudioSourceExtensions
{
    public static async UniTask WaitFinish(this AudioSource source, bool ignoreTimeScale = false)
    {
        var secondsToWait = TimeToFinish(source);

        await UniTask.Delay(TimeSpan.FromSeconds(secondsToWait), ignoreTimeScale);
    }

    public static float TimeToFinish(this AudioSource source)
    {
        if (source.loop || source.clip == null || !source.isPlaying) return 0;

        return source.clip.length - source.time;
    }

    public static void Play(this AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public static void Detach(this AudioSource source)
    {
        source.transform.SetParent(null, true);
        UnityEngine.Object.Destroy(source.gameObject, source.TimeToFinish());
    }
}
