using UnityEngine;

public class Sword : MonoBehaviour
{
    private Vector3 target;
    [SerializeField] private float swordSpeed = 5f;
    [SerializeField] private bool canMove;
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
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster != null)
            {
                monster.Kill();
                canMove = false;
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
