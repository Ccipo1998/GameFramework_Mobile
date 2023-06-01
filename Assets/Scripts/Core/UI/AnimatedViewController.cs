using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ViewController;

public class AnimatedViewController : ViewController
{
    [SerializeField]
    private CanvasGroup _CanvasGroup;

    [SerializeField]
    private float _FadeDuration = 0.3f;

    [SerializeField]
    private float _FadeSteps = 100f;

    [SerializeField]
    private bool _SkipAnimation;

    protected override void OnHiding()
    {
        if (_SkipAnimation)
        {
            State = ViewState.Hidden;
            return;
        }
        StartCoroutine(FadeOut());
    }

    protected override void OnShowing()
    {
        if (_SkipAnimation)
        {
            State = ViewState.Shown;
            return;
        }
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        _CanvasGroup.alpha = 1f;
        float elapsedTime = 0f;
        float theOne = _FadeDuration / _FadeSteps;

        while (elapsedTime < _FadeDuration)
        {
            _CanvasGroup.alpha = 1 - (elapsedTime / _FadeDuration);
            yield return new WaitForSecondsRealtime(theOne);
            elapsedTime += theOne;
        }

        State = ViewState.Hidden;
    }

    private IEnumerator FadeIn()
    {
        _CanvasGroup.alpha = 0f;
        float elapsedTime = 0f;
        float theOne = _FadeDuration / _FadeSteps;

        while (elapsedTime < _FadeDuration)
        {
            _CanvasGroup.alpha = (elapsedTime / _FadeDuration);
            yield return new WaitForSecondsRealtime(theOne);
            elapsedTime += theOne;
        }

        State = ViewState.Shown;
    }
}
