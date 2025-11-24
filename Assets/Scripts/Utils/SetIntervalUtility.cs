using System;
using System.Collections;
using UnityEngine;

public class SetIntervalUtility
{
    private MonoBehaviour context;
    private Coroutine runningCoroutine;

    public SetIntervalUtility(MonoBehaviour c)
    {
        context = c;
    }

    public void SetInterval(Action fn, float interval)
    {
        StopInterval(); // ensure only 1 interval runs at a time
        runningCoroutine = context.StartCoroutine(_SetInterval(fn, interval));
    }

    private IEnumerator _SetInterval(Action callback, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            callback?.Invoke();
        }
    }

    public void StopInterval()
    {
        if (runningCoroutine != null)
        {
            context.StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
    }
}
