using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    private InventorySlot slot;

    public void Setup(InventorySlot slot)
    {
        this.slot = slot;
        Button useButton = transform.Find("UseButton").GetComponent<Button>();
        useButton.onClick.AddListener(OnUseItem);
        Button dropButton = transform.Find("DropButton").GetComponent<Button>();
        dropButton.onClick.AddListener(OnDropItem);
    }

    void OnUseItem()
    {
        slot.UseItem();
        slot.isPanelActive = false;
        ClosePanel();
    }
    void OnDropItem()
    {
        slot.DropItem();
        slot.isPanelActive = false;
        ClosePanel();
    }

    public void ClosePanel()
    {
        Destroy(gameObject);
    }
}
