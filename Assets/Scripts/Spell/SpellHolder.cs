using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellHolder : MonoBehaviour
{

    public Spell spell;
    float cooldownTime;
    float activeTime;

    enum SpellState
    {
        ready,
        active,
        cooldown,
    }

    SpellState state = SpellState.ready;

    public KeyCode key;
    void Update()
    {
        if (spell != null)
        {

            switch (state)
            {
                case SpellState.ready:
                    if (Input.GetKeyDown(key))
                    {
                        spell.Activate(gameObject);
                        state = SpellState.active;
                        activeTime = spell.activeTime;
                    }
                    break;

                case SpellState.active:
                    if (activeTime > 0)
                    {
                        activeTime -= Time.deltaTime;
                    }
                    else
                    {
                        spell.BeginCooldown(gameObject);
                        state = SpellState.cooldown;
                        cooldownTime = spell.cooldownTime;
                        spell = null;
                    }
                    break;

                case SpellState.cooldown:
                    if (cooldownTime > 0)
                    {
                        cooldownTime -= Time.deltaTime;
                    }
                    else
                    {
                        state = SpellState.ready;
                    }
                    break;
            }
        }
    }
}
