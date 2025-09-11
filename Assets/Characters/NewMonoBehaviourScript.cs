using UnityEngine;

public class UnitStatManager : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] float moveSpeed;
    [SerializeField] int maxHealth;

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            KillUnit();
        }
    }
    public void KillUnit()
    {
        MonoBehaviour mainScript = GetComponent<MonoBehaviour>();

        switch (mainScript.GetType())
        {
            case (PlayerMovement):
                GetComponent<PlayerMovement>();
                break;
        }
    }
}
