using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

    const float GravityPower = 9.8f;

    //目的地に到達したとみなす停止距離
    const float StoppingDistance = 0.6f;

    //現在の移動速度
    Vector3 velocity = Vector3.zero;
    CharacterController characterController;
    //到達済み？
    public bool arrived = false;
    //強制的に向きを変えるか？
    bool forceRotate = false;
    //強制的に向かせたい方向
    Vector3 forceRotateDirection;
    //目的地
    public Vector3 destination;
    //移動速度
    public float walkSpeed = 0.6f;
    //回転速度
    public float rotationSpeed = 360.0f;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
        destination = transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (characterController.isGrounded)
        {
            Vector3 destinationXZ = destination;
            destinationXZ.y = transform.position.y;
            //目的地までの距離と方向
            //方向
            Vector3 direction = (destinationXZ - transform.position).normalized;
            //距離
            float distance = Vector3.Distance(transform.position, destinationXZ);

            Vector3 currentVelocity = velocity;
            //目的地に到着？
            if(arrived || distance < StoppingDistance)
            {
                arrived = true;
            }

            //移動速度ベクトル
            if (arrived)
            {
                velocity = Vector3.zero;
            }
            else
            {
                velocity = direction * walkSpeed;
            }
            //スムーズに移動するよう移動先を補間
            velocity = Vector3.Lerp(currentVelocity, velocity, Mathf.Min(Time.deltaTime * 5.0f, 1.0f));
            velocity.y = 0;

            if (!forceRotate)
            {
                //向きの変更
                if (velocity.magnitude > 0.1f && !arrived)
                {
                    Quaternion characterTargetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, rotationSpeed * Time.deltaTime);

                }
            }else
            {
                Quaternion characterTargetRotation = Quaternion.LookRotation(forceRotateDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        //重力
        velocity += Vector3.down * GravityPower * Time.deltaTime;
        Vector3 snapGround = Vector3.zero;
        if (characterController.isGrounded)
        {
            snapGround = Vector3.down;
        }
        //キャラクターの移動
        characterController.Move(velocity * Time.deltaTime + snapGround);

        if(characterController.velocity.magnitude < 0.1f)
        {
            arrived = true;
        }

        if(forceRotate && Vector3.Dot(transform.forward, forceRotateDirection) > 0.99f)
        {
            forceRotate = false;
        }
    }

    //目的地の設定
    public void SetDestination(Vector3 destination)
    {
        arrived = false;
        this.destination = destination;
    }

    //指定向きへ方向変更
    public void SetDirection(Vector3 direction) {
        forceRotateDirection = direction;
        forceRotateDirection.y = 0;
        forceRotateDirection.Normalize();
        forceRotate = true;
    }

    public void StopMove()
    {
        destination = transform.position;
    }

    public bool Arrived()
    {
        return arrived;
    }

}
