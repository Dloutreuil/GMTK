using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Dash Spell")]
public class DashSpell : Spell
{
    public float dashVelocity;
    public override void Activate(GameObject parent)
    {
        MageMovement mageMovement = parent.GetComponent<MageMovement>();
        BoxCollider2D collider = parent.GetComponent<BoxCollider2D>();

        collider.enabled = false;
        mageMovement.moveSpeed = mageMovement.moveSpeed + dashVelocity;

        Debug.Log("dashed");
    }
    public override void BeginCooldown(GameObject parent)
    {
        MageMovement mageMovement = parent.GetComponent<MageMovement>();
        BoxCollider2D collider = parent.GetComponent<BoxCollider2D>();

        collider.enabled = true;
        mageMovement.moveSpeed = mageMovement.moveSpeed - dashVelocity;
        Debug.Log("undashed");

    }

}
