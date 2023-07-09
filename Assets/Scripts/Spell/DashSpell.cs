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
        PolygonCollider2D collider = parent.GetComponent<PolygonCollider2D>();

        collider.enabled = false;
        mageMovement.moveSpeed = mageMovement.moveSpeed + dashVelocity;

        Debug.Log("dashed");


        // Calculate the angle based on the player's direction
        float angle = Mathf.Atan2(mageMovement.movement.y, mageMovement.movement.x) * Mathf.Rad2Deg;

        GameObject vfxGO = Instantiate(vfx, parent.transform.position, Quaternion.Euler(0f, 0f, angle));
        Destroy(vfxGO, 10);
    }
    public override void BeginCooldown(GameObject parent)
    {
        MageMovement mageMovement = parent.GetComponent<MageMovement>();
        PolygonCollider2D collider = parent.GetComponent<PolygonCollider2D>();

        collider.enabled = true;
        mageMovement.moveSpeed = mageMovement.moveSpeed - dashVelocity;
        Debug.Log("undashed");

    }

}
