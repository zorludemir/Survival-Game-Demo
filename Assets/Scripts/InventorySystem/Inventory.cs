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
    void UpdateUI(Item item)
    {
        foreach (Transform child in inventoryContainer)
        {
            InventoryUIItem uiItem = child.GetComponent<InventoryUIItem>();
            if (uiItem != null && uiItem.Item == item)
            {
                TextMeshProUGUI quantityText = child.Find("QuantityText").GetComponent<TextMeshProUGUI>();
                quantityText.text = item.quantity.ToString();
                break;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddItem(0, 1);
            AddItem(1, 1);
            AddItem(2, 1);
        }
    }
    void CreateUIItem(Item item)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, inventoryContainer);
        InventoryUIItem uiItem = newItem.AddComponent<InventoryUIItem>();
        uiItem.Item = item;

        TextMeshProUGUI itemNameText = newItem.transform.Find("ItemNameText").GetComponent<TextMeshProUGUI>();
        itemNameText.text = item.itemName;

        TextMeshProUGUI quantityText = newItem.transform.Find("QuantityText").GetComponent<TextMeshProUGUI>();
        quantityText.text = item.quantity.ToString();

        Image iconImage = newItem.transform.Find("IconImage").GetComponent<Image>();
        iconImage.sprite = item.icon;
    }
}
public class InventoryUIItem : MonoBehaviour
{
    public Item Item { get; set; }
}
