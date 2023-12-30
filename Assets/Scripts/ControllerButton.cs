using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnButtonDown;
    public event Action OnButtonUp;

    private Coroutine _whilePressCoroutine;

    public void OnPointerDown(PointerEventData eventData)
    {
        _whilePressCoroutine = StartCoroutine(WhilePressCoroutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(_whilePressCoroutine);
        OnButtonUp?.Invoke();
    }

    private IEnumerator WhilePressCoroutine()
    {
        while(true)
        {
            OnButtonDown?.Invoke();
            yield return new WaitForEndOfFrame();
        }
    }
}
