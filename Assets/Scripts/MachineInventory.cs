using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineInventory : MonoBehaviour
{
    public List<Item> items;

    public GameObject SlotPrefab;

    private Dictionary<Item, Slot> Slots;

    void Start()
    {
        Slots = new Dictionary<Item, Slot>();
    }

    void Update()
    {
        var spawedItems = new List<Item>();
        foreach (var item in items)
        {
            Slot SourceItemASlot = GetSlot(item.SourceItemA);
            Slot SourceItemBSlot = GetSlot(item.SourceItemB);
            if ((item.SourceItemA == null || (SourceItemASlot != null && SourceItemASlot.Amount > 0)) && (item.SourceItemB == null || (SourceItemBSlot != null && SourceItemBSlot.Amount > 0)))
            {
                var slotObject = Instantiate(SlotPrefab, transform);
                var slot = slotObject.GetComponent<Slot>();
                slot.Item = item;
                slot.MachineInventory = this;
                Slots[item] = slot;
                spawedItems.Add(item);
            }
        }

        foreach (var item in spawedItems)
        {
            items.Remove(item);
        }
    }

    public Slot GetSlot(Item item)
    {
        Slot slot;
        return item == null ? null : Slots.TryGetValue(item, out slot) ? slot : null;
    }
}
