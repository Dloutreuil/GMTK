using UnityEngine;
using System.Collections;

public class PaladinBehaviour : MonoBehaviour
{
    public GameObject swordPrefab;
    public Transform target;
    public float instantiationDelay = 3f;
    public float swordSpeed = 5f;
    public bool canThrow = true;

    private float nextInstantiationTime = 0f;
    private void Update()
    {
        if (canThrow)
        {
            nextInstantiationTime += Time.deltaTime;

            if (nextInstantiationTime >= instantiationDelay)
            {
                InstantiateSwordTowardsTarget();
                nextInstantiationTime = 0;
            }
        }
    }

    private IEnumerator InstantiateSwordTowardsTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);

        GameObject sword = Instantiate(swordPrefab, transform.position, rotation);
        Sword swordScript = sword.GetComponent<Sword>();
        if (swordScript != null)
        {
            swordScript.SetTarget(target.position, swordSpeed);
        }
        canThrow = false;

        BoxCollider2D collider = sword.GetComponent<BoxCollider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(1.5f);
        collider.enabled = true;


    }
}
