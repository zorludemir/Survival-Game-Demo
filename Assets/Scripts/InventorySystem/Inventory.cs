using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> itemDatabase;
    public List<Item> items = new List<Item>();
    public Transform inventoryContainer;
    public GameObject inventoryItemPrefab;
    public GameObject useItemPanelPrefab;

    public void AddItem(int id, int quantity)
    {
        Item databaseItem = itemDatabase.Find(i => i.id == id);
        if (databaseItem == null)
        {
            Debug.LogWarning($"Item with ID {id} not found in database.");
            return;
        }

        while (quantity > 0)
        {
            Item inventoryItem = items.Find(i => i.id == id && i.quantity < 10);

            if (inventoryItem != null)
            {
                int remainingSpace = 10 - inventoryItem.quantity;
                int amountToAdd = Mathf.Min(quantity, remainingSpace);
                inventoryItem.quantity += amountToAdd;
                quantity -= amountToAdd;
                UpdateUI(inventoryItem);
            }
            else
            {
                int amountToAdd = Mathf.Min(quantity, 10);
                Item newItem = new Item(id, databaseItem.itemName, databaseItem.icon, amountToAdd);
                items.Add(newItem);
                CreateUIItem(newItem);
                quantity -= amountToAdd;
            }
        }
    }

    public void UpdateUI(Item item)
    {
        foreach (Transform child in inventoryContainer)
        {
            InventorySlot slot = child.GetComponent<InventorySlot>();
            if (slot != null && slot.item == item)
            {
                TextMeshProUGUI quantityText = child.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                quantityText.text = item.quantity.ToString();
                break;
            }
        }

        List<Item> itemsToRemove = new List<Item>();

        foreach (Item item0 in items)
        {
            if (item0.quantity <= 0)
            {
                itemsToRemove.Add(item0);
            }
        }
        foreach (Item itemToRemove in itemsToRemove)
        {
            RemoveItem(itemToRemove);
        }
    }

    void CreateUIItem(Item item)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, inventoryContainer);
        InventorySlot slot = newItem.GetComponent<InventorySlot>();
        slot.item = item;
        slot.useItemPanel = useItemPanelPrefab;

        TextMeshProUGUI itemNameText = newItem.transform.Find("ItemNameText").GetComponent<TextMeshProUGUI>();
        itemNameText.text = item.itemName;

        TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        quantityText.text = item.quantity.ToString();

        Image iconImage = newItem.transform.Find("IconImage").GetComponent<Image>();
        iconImage.sprite = item.icon;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
        UpdateUI(item);
    }
}
