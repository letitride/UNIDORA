using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAreaActivator : MonoBehaviour {

    Collider[] attackAreaColliders;

	// Use this for initialization
	void Start () {
        AttackArea[] attackAreas = GetComponentsInChildren<AttackArea>();
        attackAreaColliders = new Collider[attackAreas.Length];

        for (int attackAreaCnt = 0; attackAreaCnt < attackAreas.Length; attackAreaCnt++) {
            Debug.Log(attackAreas[attackAreaCnt].mCollider);
            attackAreaColliders[attackAreaCnt] = attackAreas[attackAreaCnt].mCollider;
            attackAreaColliders[attackAreaCnt].enabled = false;
        }
	}

    void StartAttackHit()
    {
        foreach(Collider attackAreaCollider in attackAreaColliders)
        {
            attackAreaCollider.enabled = true;
        }
    }

    void EndAttackHit()
    {
        foreach (Collider attackAreaCollider in attackAreaColliders) {
            attackAreaCollider.enabled = false;
        }
    }
}
