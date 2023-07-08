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

    [SerializeField] public float amountOfSwordThrown = 0;
    private void Update()
    {
        if (canThrow)
        {
            nextInstantiationTime += Time.deltaTime;

            if (nextInstantiationTime >= instantiationDelay)
            {
                StartCoroutine(InstantiateSwordTowardsTarget());
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

        CapsuleCollider2D collider = sword.GetComponent<CapsuleCollider2D>();
        collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;

        amountOfSwordThrown++;

        Destroy(sword, 30);
    }

    public void DestroyedByWall()
    {
        amountOfSwordThrown = 0;
        canThrow = true;
        if (amountOfSwordThrown <= 0)
        {
            nextInstantiationTime = 0;
        }
    }
}
