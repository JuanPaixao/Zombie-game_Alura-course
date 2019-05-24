using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "CharacterStatus")]
public class Status : ScriptableObject
{
    public int initialHP;
    [HideInInspector] public int hp;
    public float speed;

    void Start()
    {
		hp = initialHP;
    }
}
