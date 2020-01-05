using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    //カメラから注視点の距離
    public float distance = 5.0f;
    //水平角
    public float horizontalAngle = 0.0f;
    //画面横幅分の角度
    public float rotAngle = 180.0f;
    //垂直角
    public float verticalAngle = 10.0f;

    //追従対象の位置
    public Transform lookTarget;
    //追従位置補正
    public Vector3 offset = Vector3.zero;

    InputManager inputManager;

	// Use this for initialization
	void Start () {
        inputManager = FindObjectOfType<InputManager>();
        //lookTarget = GameObject.Find("Player").transform;
        //print(lookTarget.position);
        offset.y = 1.5f;
    }
	
	// Update is called once per frame
	void Update () {
        //ドラッグ入力でのカメラ更新
        if (inputManager.Moved())
        {
            //1pxあたりの角度
            float anglePerPixel = rotAngle / (float)Screen.width;
            //ドラッグ量
            Vector2 delta = inputManager.GetDeltaPosition();
            horizontalAngle += delta.x * anglePerPixel;
            //360度以上で循環
            horizontalAngle = Mathf.Repeat(horizontalAngle, 360.0f);
            //+-60度まで
            verticalAngle -= delta.y * anglePerPixel;
            verticalAngle = Mathf.Clamp(verticalAngle, -60.0f, 60.0f);
        }

        //カメラ追従
        if(lookTarget != null)
        {
            //注視点
            Vector3 lookPosition = lookTarget.position + offset;
            //注視点から見たカメラの相対位置 Vector3(0, 0, -distance)を原点に垂直、水平角に移動した位置
            Vector3 relativePos = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * new Vector3(0, 0, -distance);
            transform.position = lookPosition + relativePos;
            transform.LookAt(lookPosition);

            //カメラ間に障害物がある場合はカメラを障害物位置に移動
            RaycastHit hitInfo;
            if(Physics.Linecast(lookPosition, transform.position, out hitInfo, 1 << LayerMask.NameToLayer("Ground")))
            {
                transform.position = hitInfo.point;
            }
        }
	}

    public void SetTarget(Transform target)
    {
        lookTarget = target;
    }
}
