using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using System.Diagnostics;

public class AnimationBase : MonoBehaviour
{
    private Coroutine coroutine;

    [SerializeField]
    private bool isPause;
    public bool IsPause { get => isPause; set => isPause = value; }

    public WaitWhile WaitForSecond(float second)
    {
        float totalSeconds = 0;

        return new WaitWhile(() =>
        {
            totalSeconds += Time.deltaTime;
            return totalSeconds < second && !isPause;
        });
    }

    public WaitWhile WaitNextFrame() => new WaitWhile(() => !isPause);

    public WaitWhile WaitForFrame(int frame)
    {
        int totalFrames = 0;
        return new WaitWhile(() =>
        {
            totalFrames++;
            return totalFrames < frame && !isPause;
        });
    }

    public void Play(IEnumerator coroutineEnumerator)
    {
        coroutine = StartCoroutine(coroutineEnumerator);
    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }
}
