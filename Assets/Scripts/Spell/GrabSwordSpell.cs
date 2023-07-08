using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Attrack Sword Spell")]

public class GrabSwordSpell : Spell
{
    public GameObject target;
    public float attractionForce = 10;
    public override void Activate(GameObject parent)
    {
        target = FindAnyObjectByType<MageMovement>().gameObject;
        Debug.Log(target.name);
        Debug.Log("grab");
        Sword sword = FindObjectOfType<Sword>();
        Rigidbody2D parentRigidbody = sword.GetComponent<Rigidbody2D>();
        if (parentRigidbody != null && target != null)
        {
            Vector2 attractionDirection = target.transform.position - sword.transform.position;
            parentRigidbody.velocity = attractionDirection.normalized * attractionForce;
        }
        sword.moveTowardsMage = true;
        sword.swordSpeed = attractionForce;

        GameObject vfxGO = Instantiate(vfx, parent.transform);
        Destroy(vfxGO, 10);

        /*Debug.Log("grab");
        Sword sword = FindObjectOfType<Sword>();
        sword.moveTowardsMage = true;*/
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
