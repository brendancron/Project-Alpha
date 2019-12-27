using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScriptList : MonoBehaviour {

    public GameObject scriptPanelPrefab;

    public float ratio = 0.2f;

    void Start() {
        CheckDirectory();
        UpdateList();
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
        float width = GetComponent<RectTransform>().rect.width;
        float height = width*ratio;
        foreach(string s in filePaths) {
            if(true) { // this line should check a regex for valid file names!
                //print(s);

                GameObject scriptPanel = Object.Instantiate(scriptPanelPrefab, transform);
                scriptPanel.GetComponent<ScriptPanel>().init(num);
                num++;
            }
        }
        float scrollheight = num*height;
    }

    private void CheckDirectory() {
        if(!Directory.Exists(ProjectVariables.scriptDirectoryPath)) {
            Directory.CreateDirectory(ProjectVariables.scriptDirectoryPath);
        }
    }
}
