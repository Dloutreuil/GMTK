using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDataPrefab : MonoBehaviour
{
    public Spell spellToSpawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mage"))
        {
            SpellHolder spellHolder = other.GetComponent<SpellHolder>();
            if (spellHolder.spell == null)
            {
                spellHolder.spell = spellToSpawn;
                other.GetComponent<AudioSource>().Play();
                UiManager.Instance.UpdateSpell(spellToSpawn.spellSprite);
                Destroy(gameObject);
            }
        }
    }

}
