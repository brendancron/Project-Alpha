using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPanel : MonoBehaviour {
    
    public RectTransform panelRectTransform;
    public RectTransform containerRectTransform;

    public void init(int num) {
        containerRectTransform = transform.parent.GetComponent<RectTransform>();
    }

}
