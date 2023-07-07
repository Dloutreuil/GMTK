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

}
