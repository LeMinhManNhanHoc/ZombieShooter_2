using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHelper : UnityEngine.UI.Button
{
    Image[] childImageList;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        Color color;
        switch (state)
        {
            case Selectable.SelectionState.Highlighted:
                color = this.colors.highlightedColor;
                break;
            case Selectable.SelectionState.Pressed:
                color = this.colors.pressedColor;
                break;
            case Selectable.SelectionState.Disabled:
                color = this.colors.disabledColor;
                break;
            default:
                color = this.colors.normalColor;
                break;
        }

        if (base.gameObject.activeInHierarchy)
        {
            SetColor(color);
        }
    }

    private void SetColor(Color color)
    {
        if(childImageList == null)
        {
            childImageList = GetComponentsInChildren<Image>();
        }

        for (int i = 0; i < childImageList.Length; i++)
        {
            childImageList[i].color = color;
        }
    }
}
