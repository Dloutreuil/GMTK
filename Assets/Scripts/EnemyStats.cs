using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Enemy/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    public float speed = 5f;
    public int health = 100;
    public int damage = 10;
}
