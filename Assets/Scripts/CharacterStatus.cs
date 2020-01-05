using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour {

    public int HP = 100;
    public int MaxHP = 100;

    public int Power = 10;

    public GameObject lastAttackTarget = null;

    public string characterName = "Player";

    public bool attacking = false;
    public bool died = false;

}
