using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool canTakeDamage = true;

    public void Kill()
    {
        if (canTakeDamage)
        {
            print("I have been killed");
            Destroy(gameObject);
            SpellManager.Instance.DropSpell(transform.position);
        }
    }
}
