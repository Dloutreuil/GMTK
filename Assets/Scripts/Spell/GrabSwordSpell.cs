using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Spell/Attrack Sword Spell")]

public class GrabSwordSpell : Spell
{
    public float attractionForce = 10;
    public override void Activate(GameObject parent)
    {
        Debug.Log("grab");
        Sword sword = FindObjectOfType<Sword>();
        //Rigidbody2D parentRigidbody = sword.GetComponent<Rigidbody2D>();

        if (!sword.isGrabbed)
        {
            //if (parentRigidbody != null)
            //{
            Vector2 attractionDirection = parent.transform.position - sword.transform.position;
            //parentRigidbody.velocity = attractionDirection.normalized * attractionForce;

            // Calculate the angle based on the parent's direction
            float angle = Mathf.Atan2(attractionDirection.y, attractionDirection.x) * Mathf.Rad2Deg;

            GameObject vfxGO = Instantiate(vfx, sword.transform.position, Quaternion.Euler(0f, 0f, angle));
            Destroy(vfxGO, 10);
            Debug.Log("vfx");
            // }

            sword.moveTowardsMage = true;
            sword.swordSpeed = attractionForce;
        }

        /*Debug.Log("grab");
        Sword sword = FindObjectOfType<Sword>();
        sword.moveTowardsMage = true;*/
    }

    public override void BeginCooldown(GameObject parent)
    {

    }
}
