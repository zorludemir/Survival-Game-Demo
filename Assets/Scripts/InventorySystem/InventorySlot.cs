using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public GameObject useItemPanel;
    public Vector3 offset;
    public bool isPanelActive;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !isPanelActive)
        {
            GameObject panel = Instantiate(useItemPanel, transform.position + offset, Quaternion.identity,transform.parent.parent);
            panel.GetComponent<ItemPanel>().Setup(this);
            isPanelActive = true;
        }
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.quantity--;
            Inventory inventory = FindObjectOfType<Inventory>();
            if (item.id == 4)
            {
                Player player = FindAnyObjectByType<Player>();
                player.currentHealth += 20;
            }
            inventory.UpdateUI(item);
            if (item.quantity <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void DropItem()
    {
        item.quantity--;
        Inventory inventory = FindObjectOfType<Inventory>();
        inventory.UpdateUI(item);
        if (item.quantity <= 0)
        {
            Destroy(gameObject);
        }
    }
}
