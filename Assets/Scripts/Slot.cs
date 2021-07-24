using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public CraftButton CraftButton;
    public Image ItemImage;
    public Text ItemAmountText;

    public Image RecipeIndicator;
    public Image SourceItemAImage;
    public Image SourceItemBImage;
    public Image DestinationItemImage;

    public Button UpgradeIndicator;
    public Text UpgradeAmountText;
    public Image UpgradeItemImage;

    public Item Item { get; set; }
    public MachineInventory MachineInventory { get; set; }
    public int Amount { get; set; }
    public bool Automated { get; set; }
    public bool hovering { get; set; }

    void Start()
    {
        ItemImage.sprite = Item.Image;
        DestinationItemImage.sprite = Item.Image;

        if (Item.SourceItemA != null)
        {
            SourceItemAImage.sprite = Item.SourceItemA.Image;
        }

        if (Item.SourceItemB != null)
        {
            SourceItemBImage.sprite = Item.SourceItemB.Image;
        }

        if (Item.AutomationItem != null)
        {
            UpgradeItemImage.sprite = Item.AutomationItem.Image;
            UpgradeAmountText.text = Item.AutomationItemAmount + "x";
        }

        if (Item.SourceItemA == null && Item.SourceItemB == null)
        {
            RecipeIndicator.gameObject.SetActive(false);
        }

        if (Item.AutomationItem == null)
        {
            UpgradeIndicator.gameObject.SetActive(false);
        }

        UpgradeIndicator.onClick.AddListener(() =>
        {
            Slot slot = MachineInventory.GetSlot(Item.AutomationItem);
            if (slot != null && slot.Amount >= Item.AutomationItemAmount)
            {
                slot.Amount -= Item.AutomationItemAmount;
                Automated = true;
            }
        });

        InvokeRepeating("Craft", 0f, 0.5f);
    }

    void Craft()
    {
        Debug.Log(CraftButton.pressed);

        if (Automated || CraftButton.pressed)
        {
            Slot SourceItemASlot = MachineInventory.GetSlot(Item.SourceItemA);
            Slot SourceItemBSlot = MachineInventory.GetSlot(Item.SourceItemB);

            int remain = Automated ? 1 : 0;
            if (SourceItemASlot != null)
            {
                if (SourceItemASlot.Amount > remain)
                {
                    SourceItemASlot.Amount -= 1;
                }
                else
                {
                    return;
                }
            }

            if (SourceItemBSlot != null)
            {
                if (SourceItemBSlot.Amount > remain)
                {
                    SourceItemBSlot.Amount -= 1;
                }
                else
                {
                    return;
                }
            }

            Amount += 1;
        }
    }

    void Update()
    {
        ItemAmountText.text = Amount + "x";
    }
}
