using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventdata)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventdata.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventdata);
    }

    public override void OnPointerUp(PointerEventData eventdata)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventdata);
    }
}