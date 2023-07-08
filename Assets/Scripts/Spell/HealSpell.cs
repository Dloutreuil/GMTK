using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Heal Sword Spell")]
public class HealSpell : Spell
{
    public int healAmount = 10;

    public override void Activate(GameObject parent)
    {
        MageBehaviour mageBehaviour = FindObjectOfType<MageBehaviour>();
        mageBehaviour.health += healAmount;
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}

