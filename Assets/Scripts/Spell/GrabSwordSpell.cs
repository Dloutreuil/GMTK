using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Attrack Sword Spell")]

public class GrabSwordSpell : Spell
{
    public float attractionForce = 10f;
    public GameObject target;

    private void OnEnable()
    {
        target = FindObjectOfType<MageMovement>().gameObject;
    }
    public override void Activate(GameObject parent)
    {
        Sword sword = FindAnyObjectByType<Sword>();

        Rigidbody2D parentRigidbody = sword.gameObject.GetComponent<Rigidbody2D>();
        if (parentRigidbody != null && target != null)
        {
            Vector2 attractionDirection = target.transform.position - parent.transform.position;
            parentRigidbody.AddForce(attractionDirection * attractionForce);
        }
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
