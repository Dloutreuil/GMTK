using UnityEngine;

public class Sword : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private float swordSpeed = 5f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool canKill = true;

    public void SetTarget(Vector3 target, float swordspeed)
    {
        this.target = target;
        swordSpeed = swordspeed;
    }

    private void Update()
    {
        if (canMove)
        {
            Vector3 direction = target - transform.position;
            transform.Translate(direction.normalized * swordSpeed * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && canKill)
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null) //canKill not working ????
            {
                monster.Kill();
                if (monster.canTakeDamage)
                {
                    canMove = false;
                    Debug.Log("killing enemy");
                }
            }
        }
        if (other.CompareTag("Mage"))
        {
            canMove = false;
            MageMovement mageMovement = other.GetComponent<MageMovement>();
            transform.SetParent(mageMovement.transform);
        }
        if (other.CompareTag("Paladin"))
        {
            PaladinSwordReset paladinSwordReset = other.gameObject.GetComponentInChildren<PaladinSwordReset>();
            paladinSwordReset.paladinBehaviour.canThrow = true;
            Destroy(gameObject);
        }
    }
}
