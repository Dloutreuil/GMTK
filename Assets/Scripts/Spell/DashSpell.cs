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

        GameObject vfxGO = Instantiate(vfx, parent.transform);
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
