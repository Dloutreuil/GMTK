using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSwordReset : MonoBehaviour
{

    public PaladinBehaviour paladinBehaviour;

    private void Awake()
    {
        paladinBehaviour = GetComponentInParent<PaladinBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mage"))
        {
            Debug.Log("tezst");
            paladinBehaviour.canThrow = true;
            Sword sword = other.gameObject.GetComponentInChildren<Sword>();
            sword.DestroySword();
        }
    }


}
