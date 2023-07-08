using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool canTakeDamage = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Kill()
    {
        if (canTakeDamage)
        {
            print("I have been killed");
            Destroy(gameObject);
        }
    }
}
