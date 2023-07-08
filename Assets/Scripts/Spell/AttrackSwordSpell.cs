using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Attrack Sword Spell")]

public class AttrackSwordSpell : Spell
{
    public float attractionForce = 10f;
    public GameObject target;

    private void OnEnable()
    {
        target = FindAnyObjectByType<MageMovement>().gameObject;
    }
    public override void Activate(GameObject parent)
    {
        Sword sword = FindObjectOfType<Sword>();
        Rigidbody2D parentRigidbody = sword.GetComponent<Rigidbody2D>();
        if (parentRigidbody != null && target != null)
        {
            Vector2 attractionDirection = target.transform.position - sword.transform.position;
            parentRigidbody.AddForce(attractionDirection * attractionForce);
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}