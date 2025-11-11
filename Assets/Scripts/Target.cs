
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject destroyedObject;

    public void TakeDamage(float damage)
    {
        health-=damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        GameObject broken = Instantiate(destroyedObject,transform.position,transform.rotation);
        Destroy(gameObject);
        Destroy(broken, 4);
    }
}
