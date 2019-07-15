﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Mode[] modes;

    public Mode currentMode;

    public ModeState startingMode;

    [Header("Animation")]
    public float animationInTime = 0.5f;
    public float animationOutTime = 0.25f;

    public AnimationCurve animationCurve;

    public void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }

    private void Start()
    {
        foreach (Mode mode in modes)
        {
            mode.OnModeDisabled();
        }

        SetMode(startingMode);
    }

    public void SetMode(ModeState state)
    {
        if(currentMode != null)
        {
            StartCoroutine(AnimateModeAlpha(currentMode, 0, animationOutTime, 0));
        }

        if (state == ModeState.None)
            return;

        currentMode = GetMode(state);

        StartCoroutine(AnimateModeAlpha(currentMode, 1, animationInTime, animationOutTime));
    }

    public Mode GetMode(ModeState state)
    {
        foreach (Mode mode in modes)
        {
            if (mode.state == state)
                return mode;
        }

        return null;
    }

    public IEnumerator AnimateModeAlpha(Mode mode, float targetAlpha, float time, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0;
        float currentAlpha = mode.canvasGroup.alpha;

        while(elapsedTime < time)
        {
            mode.canvasGroup.alpha = Mathf.Lerp(currentAlpha, targetAlpha, animationCurve.Evaluate(elapsedTime / time));

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (targetAlpha == 0)
            mode.OnModeDisabled();
        else if (targetAlpha == 1)
            mode.OnModeEnabled();
    }

}
