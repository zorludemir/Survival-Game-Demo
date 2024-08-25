using UnityEngine;

public class Tree : MonoBehaviour
{
    public int health = 5;
    public GameObject ragdoll;

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            FellTree();
        }
    }

    void FellTree()
    {
        Inventory inv = FindObjectOfType<Inventory>();
        inv.AddItem(0, 3);
        Instantiate(ragdoll, transform.position, Quaternion.Euler(new Vector3(2,0,0)));
        Destroy(gameObject);
    }
}
