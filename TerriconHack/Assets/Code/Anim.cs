using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Anim : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Note _note;

    private void Awake()
    {
        _note = FindObjectOfType<Note>();
    }

    private void OnEnable()
    {
        _canvasGroup.DOFade(0.5f, 1);
        Debug.Log(_note.GetScore());
    }

    private void OnDisable()
    {
        _canvasGroup.DOFade(0, 1);
    }
}
