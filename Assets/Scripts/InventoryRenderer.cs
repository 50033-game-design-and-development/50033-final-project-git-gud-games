using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryRenderer : MonoBehaviour {
    // hotbar USS slot class to assign to filled inventory slots
    private const string OCCUPIED_SLOT_CLASS = "filled";

    public UIDocument hotbarUI;
    public int hotbarItemGap = 40;
    public int numHotbarItems = 5;

    private float _padding;

    // gap between bar item images
    private readonly List<Button> _hotbarItems = new();

    public void OnInventoryUpdate() {
        if (GameState.inventoryOpened) {
            Show();
            PopulateHotbar();
        } else {
            Hide();
        }
    }

    private void Show() {
        hotbarUI.rootVisualElement.style.visibility = Visibility.Visible;
    }

    private void Hide() {
        hotbarUI.rootVisualElement.style.visibility = Visibility.Hidden;
    }

    private void InitializeHotbar() {
        // Initialize the hotbar items slot UI elements

        // wait for UI document to render,
        // necessary before reading hotbar padding value
        VisualElement root = hotbarUI.rootVisualElement;
        var buttons = root.Query<Button>().ToList();
        // Remove pre-existing buttons from the inventory
        foreach (var button in buttons) {
            button.RemoveFromHierarchy();
        }

        VisualElement hotbarElement = (
            root.Q<VisualElement>("HotbarWrapper").Q<VisualElement>("Hotbar")
        );
        _padding = (
            hotbarElement.resolvedStyle.paddingTop 
            + hotbarElement.resolvedStyle.paddingBottom
        ) / 2.0f;

        var hotbarWidth = hotbarElement.resolvedStyle.width;
        var hotbarHeight = hotbarElement.resolvedStyle.height;
        var hotbarItemHeight = hotbarHeight - _padding * 2;
        Debug.Log("ITEM_HEIGHT " + hotbarItemHeight);

        for (int k = 0; k < numHotbarItems; k++) {
            // Create hotbar item slots (each slot is a UIElement Button)
            Button inventoryItem = new Button();
            inventoryItem.style.width = hotbarItemHeight;
            hotbarElement.Add(inventoryItem);
            _hotbarItems.Add(inventoryItem);
        }
    }

    /// <summary>
    /// fill up the hotbar items slot UI elements based
    /// on the list of inventory items in GameState
    /// </summary>
    private void PopulateHotbar() {
        for (var k = 0; k < _hotbarItems.Count; k++) {
            var hotbarSlot = _hotbarItems[k];
            var buttonNo = k;

            hotbarSlot.clicked += () => {
                // TODO: tie this to an actually useful callback during integration
                Debug.Log($"SLOT {buttonNo} CLICKED");
            };
            
            Debug.Assert(
                GameState.inventory.Count < _hotbarItems.Count,
                "Theres more inventory items than hotbar slots available!"
            );

            if (k < GameState.inventory.Count) {
                // set hotbar slot background image and make slot active
                var collectable = GameState.inventory[k];
                Debug.Log("SET_ITEM " + k);
                
                hotbarSlot.AddToClassList(OCCUPIED_SLOT_CLASS);
                hotbarSlot.style.backgroundImage = (
                    new StyleBackground(collectable.itemSprite)
                );
            } else {
                // remove hotbar slot background image and make slot inactive
                // if theres no corresponding inventory item for the hotbar slot
                hotbarSlot.RemoveFromClassList(OCCUPIED_SLOT_CLASS);
                hotbarSlot.style.backgroundImage = null;
            }
        }
    }

    private IEnumerator initialize() {
        yield return new WaitForEndOfFrame();
        InitializeHotbar();
        PopulateHotbar();
    }

    // Start is called before the first frame update
    void OnEnable() {
        Hide();
        StartCoroutine(initialize());
    }
}
