using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMenu : MonoBehaviour
{

    public GameObject menu;

    public ScriptList scriptList;

    private bool pressed = false;

    public string menuKey;

    void Start() {
        menu.SetActive(false);
    }

    void Update() {
        if(pressed) {
            if(!Input.GetKey(menuKey)) {
                pressed = false;
            }
        } else if(Input.GetKey(menuKey)) {
            pressed = true;
            if(menu.activeSelf) {
                menu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            } else {
                scriptList.UpdateList();
                menu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
