using System;
using System.Collections;
using UnityEngine;

public class SetTimeoutUtility
{
    private MonoBehaviour context;
    private Coroutine runningCoroutine;

    public SetTimeoutUtility(MonoBehaviour c)
    {
        context = c;
    }

    public void SetTimeout(Action fn, float delay)
    {
        StopTimeout(); // ensure only 1 timeout at a time
        runningCoroutine = context.StartCoroutine(_SetTimeout(fn, delay));
    }

    private IEnumerator _SetTimeout(Action callback, float delay)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
        runningCoroutine = null; // Auto-clear when done
    }

    public void StopTimeout()
    {
        if (runningCoroutine != null)
        {
            context.StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
    }
}
