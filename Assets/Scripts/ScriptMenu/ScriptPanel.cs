﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPanel : MonoBehaviour {
    
    private RectTransform panelRectTransform;

    public static float ratio = 0.1f;

    private string path;

    public void init(int num, string path) {
        this.path = path;
        panelRectTransform = GetComponent<RectTransform>();
        panelRectTransform.anchorMin = new Vector2(0,1);
        panelRectTransform.anchorMax = new Vector2(1,1);
        panelRectTransform.pivot = new Vector2(0.5f,0.5f);
        float height = transform.parent.GetComponent<RectTransform>().rect.width * ratio;
        float bottom = -height*(num+1);
        float top = -height*num;
        panelRectTransform.offsetMin = new Vector2(panelRectTransform.offsetMin.x, bottom);
        panelRectTransform.offsetMax = new Vector2(panelRectTransform.offsetMax.x, top);
    }

}
