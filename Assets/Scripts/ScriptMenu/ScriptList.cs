using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScriptList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        CheckDirectory();
        UpdateList();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void UpdateList() {
        CheckDirectory();
        print("getting files");
        string[] filePaths = Directory.GetFiles(ProjectVariables.scriptDirectoryPath, "*.py", SearchOption.TopDirectoryOnly);
        foreach(string s in filePaths) {
            print(s);
        }
    }

    private void CheckDirectory() {
        if(!Directory.Exists(ProjectVariables.scriptDirectoryPath)) {
            Directory.CreateDirectory(ProjectVariables.scriptDirectoryPath);
        }
    }
}
