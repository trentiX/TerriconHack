using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Anim : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private void OnEnable()
    {
        _canvasGroup.DOFade(0.5f, 1);
    }

    private void OnDisable()
    {
        _canvasGroup.DOFade(0, 1);
    }
}
