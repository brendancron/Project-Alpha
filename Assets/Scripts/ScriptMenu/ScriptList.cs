using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class ScriptList : MonoBehaviour {

    public GameObject scriptPanelPrefab;

    public RectTransform listRectTransform;

    public static readonly Regex regex = new Regex(@"[a-zA-Z0-9_]+\.py$");

    void Start() {
        CheckDirectory();
        listRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void UpdateList() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        CheckDirectory();
        string[] filePaths = Directory.GetFiles(ProjectVariables.scriptDirectoryPath, "*.py", SearchOption.TopDirectoryOnly);
        int num = 0;
        float width = listRectTransform.rect.width;
        foreach(string s in filePaths) {
            // this if conditional should check a regex for valid file names!
            if(regex.Matches(s.Substring(ProjectVariables.scriptDirectoryPath.Length)).Count > 0) { 
                //print(s);

                GameObject scriptPanel = Object.Instantiate(scriptPanelPrefab, transform);
                scriptPanel.GetComponent<ScriptPanel>().init(num, s);
                num++;
            }
        }
        float height = listRectTransform.rect.width * ScriptPanel.ratio * num;
        listRectTransform.offsetMin = new Vector2(listRectTransform.offsetMin.x, 0);
        listRectTransform.offsetMax = new Vector2(listRectTransform.offsetMax.x, height);
    }

    private void CheckDirectory() {
        if(!Directory.Exists(ProjectVariables.scriptDirectoryPath)) {
            Directory.CreateDirectory(ProjectVariables.scriptDirectoryPath);
        }
    }
}
