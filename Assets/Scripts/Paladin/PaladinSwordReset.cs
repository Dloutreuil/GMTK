using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSwordReset : MonoBehaviour
{

    public PaladinBehaviour paladinBehaviour;
    public AudioSource audiosource;

    private void Awake()
    {
        paladinBehaviour = GetComponentInParent<PaladinBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mage"))
        {
            audiosource.PlayDelayed(1);
            Debug.Log("tezst");
            paladinBehaviour.canThrow = true;
            paladinBehaviour.amountOfSwordThrown = 0;
            Sword sword = other.gameObject.GetComponentInChildren<Sword>();
            sword.DestroySword();
        }
    }


}
