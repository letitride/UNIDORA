using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour {

    CharacterStatus status;
    public Collider mCollider;

	// Use this for initialization
	void Start () {
        status = transform.root.GetComponent<CharacterStatus>();
        mCollider = GetComponent<Collider>();
        /*
        if (gameObject.layer ==  LayerMask.NameToLayer("PlayerAttack"))
        {
            mCollider = GetComponent<CapsuleCollider>();
        }
        else
        {
            mCollider = GetComponent<SphereCollider>();
        }
        */
    }

    public class AttackInfo
    {
        public int attackPower;
        public Transform attacker;
    }

    //自身の攻撃情報
    AttackInfo GetAttackInfo()
    {
        AttackInfo attackInfo = new AttackInfo();
        attackInfo.attackPower = status.Power;
        attackInfo.attacker = transform.root;
        return attackInfo;
    }

    //攻撃を当てた
    private void OnTriggerEnter(Collider other)
    {
        //衝突相手otherへメッセージを送る
        other.SendMessage("Damage", GetAttackInfo());
        //攻撃対象を保存
        status.lastAttackTarget = other.transform.root.gameObject;
    }

    void OnAttack()
    {
        mCollider.enabled = true;
    }


    // Update is called once per frame
    void OnAttackTermination () {
        mCollider.enabled = false;
    }
}
