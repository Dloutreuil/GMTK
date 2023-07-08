using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    public new string name;
    public Sprite spellSprite;
    public GameObject vfx;
    public float cooldownTime;
    public float activeTime;
    public virtual void Activate(GameObject parent)
    {
        GameObject vfxGO = Instantiate(vfx, parent.transform);
        Destroy(vfx, 10);
    }

    public virtual void BeginCooldown(GameObject parent)
    {

    }
}
