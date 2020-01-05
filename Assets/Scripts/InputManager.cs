using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    Vector2 slideStartPosition;
    Vector2 prevPosition;
    Vector2 delta = Vector2.zero;
    bool moved = false;

	// Use this for initialization
	void Start () {
        print(transform.position);
	}
	
	// Update is called once per frame
	void Update () {

        //開始位置
        if (Input.GetButtonDown("Fire1"))
        {
            slideStartPosition = GetCursorPosition();
        }

        //画面の1割以上ドラッグした？
        if (Input.GetButton("Fire1"))
        {
            if(Vector2.Distance(slideStartPosition, GetCursorPosition()) >= (Screen.width * 0.1f) ) {
                print(Vector2.Distance(slideStartPosition, GetCursorPosition()));
                print(Screen.width * 0.1f);
                moved = true;
            }
        }

        //ドラッグが終了
        if(!Input.GetButtonUp("Fire1") && !Input.GetButton("Fire1"))
        {
            moved = false;
        }

        //ドラッグ量
        if (moved)
        {
            delta = GetCursorPosition() - prevPosition;
        }
        else
        {
            delta = Vector2.zero;
        }
        prevPosition = GetCursorPosition();
	}

    public bool Clicked()
    {
        if(!moved && Input.GetButton("Fire1"))
        {
            return true;
        }
        return false;
    }

    public Vector2 GetDeltaPosition()
    {
        return delta;
    }

    public bool Moved()
    {
        return moved;
    }

    public Vector2 GetCursorPosition()
    {
        return Input.mousePosition;
    }
}
