using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitArea : MonoBehaviour {

    void Damage(AttackArea.AttackInfo attackInfo) {
        Debug.Log("hit!");
        transform.root.SendMessage("Damage", attackInfo);
    }
}
