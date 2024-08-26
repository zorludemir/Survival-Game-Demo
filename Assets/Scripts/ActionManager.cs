using System.Collections;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public int damage = 1;
    public float range = 2f;
    public float attackCooldown = 1.0f;

    private float lastAttackTime;
    private Player player;
    private Toolbar toolbar;
    private Inventory inventory;
    private Animator currentAnimator;

    private void Start()
    {
        player = GetComponent<Player>();
        toolbar = FindObjectOfType<Toolbar>();
        inventory = FindObjectOfType<Inventory>();
        lastAttackTime = -attackCooldown;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && player.currentStamina > 15 && Time.time >= lastAttackTime + attackCooldown)
        {
            
            UseTool(toolbar.tIndex);
            lastAttackTime = Time.time;
        }

        if (currentAnimator != null)
        {
            currentAnimator.SetTrigger("Idle");
        }
    }

    void UseTool(int index)
    {
        currentAnimator = toolbar.tools[index].GetComponent<Animator>();
        if (index == 0 || index == 1 || index == 3)
        {
            player.currentStamina -= 25;
            currentAnimator.SetTrigger("Hit");
        }
        StartCoroutine(Action(index));
    }
    private IEnumerator Action(int index)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range))
        {
            if (hit.transform.gameObject != null)
            {
                if(index == 0)
                {
                    if (hit.collider.transform.parent != null)
                    {
                        if (hit.collider.transform.parent.CompareTag("Tree"))
                        {
                            Tree tree = hit.collider.transform.parent.GetComponent<Tree>();
                            if (tree != null)
                            {
                                yield return new WaitForSeconds(.5f);
                                tree.TakeDamage(damage);
                            }
                        }
                    }
                }
                if(index == 1)
                {
                    if (hit.collider.transform.CompareTag("Rock"))
                    {
                        yield return new WaitForSeconds(.5f);
                        Destroy(hit.transform.gameObject);
                        inventory.AddItem(3, 1);
                    }
                }
                if(index == 3)
                {
                    if (hit.collider.transform.CompareTag("Sheep"))
                    {
                        yield return new WaitForSeconds(.4f);
                        hit.collider.GetComponent<AnimalAI>().TakeDamage(damage);
                    }
                }
            }
        }
    }
}
