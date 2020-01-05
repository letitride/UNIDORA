using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour {

    const float RayCastMaxDistance = 100.0f;
    InputManager inputManager;
	// Use this for initialization
	void Start () {
        inputManager = FindObjectOfType<InputManager>();
	}
	
	// Update is called once per frame
	void Update () {
        Walking();
	}

    void Walking() {
        if (inputManager.Clicked())
        {
            Vector2 clickpos = inputManager.GetCursorPosition();
            Ray ray = Camera.main.ScreenPointToRay(inputManager.GetCursorPosition());
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, RayCastMaxDistance, 1 << LayerMask.NameToLayer("Ground")))
            {
                SendMessage("SetDestination", hitInfo.point);
            }
        }
    }
}
