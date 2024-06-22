using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoverableButton : Button, IPointerEnterHandler
{
    [SerializeField]
    private UnityEvent m_onHover = new();

    #pragma warning disable IDE1006 // Naming Styles (because Unity uses this style in Button class)
    public UnityEvent onHover => m_onHover;
    #pragma warning restore IDE1006 // Naming Styles

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        m_onHover.Invoke();
    }
}
