using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryRenderer : MonoBehaviour {
    // hotbar USS slot class to assign to filled inventory slots
    private const string OccupiedSlotClass = "filled";

    public UIDocument hotbarUI;
    public int hotbarItemGap = 40;

    private float _padding;
    private int _numHotbarItems;

    // gap between bar item images
    private readonly List<Button> _hotbarItems = new List<Button>();
    private bool _hotbarRendered = false;
    private bool _hotbarItemsUpdating = false;

    public void OnInventoryUpdate() {
        if (GameState.IsInventoryOpened()) {
            Show();
            StartCoroutine(PopulateHotbar());
        } else {
            Hide();
        }
    }

    public void Show() {
        hotbarUI.rootVisualElement.style.visibility = Visibility.Visible;
    }

    public void Hide() {
        hotbarUI.rootVisualElement.style.visibility = Visibility.Hidden;
    }

    private int CalculateNumberOfHotbarItems(float hotbarWidth, float hotbarItemSize) {
        // returns number of hotbar slots that can fit in the hotbar
        VisualElement root = hotbarUI.rootVisualElement;
        VisualElement hotbarElement = root.Q("Hotbar");
        _padding = hotbarElement.resolvedStyle.paddingLeft;
        var usableWidth = hotbarWidth - _padding * 2;
        /*
        n - number of hotbar items
        H - hotbar item size
        u - usable width (width - left and right  padding)
        g - gap size

        (n-1)*g + n*H = u
        n*g - g + n*H = u
        n*(g+H) - g = u
        n = (u+g) / (g+H)
        */
        var numHotbarItems = (int) Math.Floor(
            (usableWidth + hotbarItemGap) / (hotbarItemSize + hotbarItemGap)
        );
        return numHotbarItems;
    }

    private IEnumerator InitializeHotbar() {
        // Initialize the hotbar items slot UI elements

        // wait for UI document to render,
        // necessary before reading hotbar padding value
        yield return null;

        VisualElement root = hotbarUI.rootVisualElement;
        var buttons = root.Query<Button>().ToList();
        // Remove pre-existing buttons from the inventory
        foreach (var button in buttons) {
            button.RemoveFromHierarchy();
        }

        VisualElement hotbarElement = root.Q<VisualElement>("Hotbar");
        _padding = (
            hotbarElement.resolvedStyle.paddingLeft + hotbarElement.resolvedStyle.paddingRight
        ) / 2.0f;

        var hotbarWidth = hotbarElement.resolvedStyle.width;
        var hotbarHeight = hotbarElement.resolvedStyle.height;
        Debug.Log("HOTBAR_HEIGHT " + hotbarHeight);
        var hotbarItemHeight = hotbarHeight - _padding * 2;
        Debug.Log("HOTBAR_ITEM_HEIGHT " + hotbarItemHeight);
        _numHotbarItems = CalculateNumberOfHotbarItems(hotbarWidth, hotbarItemHeight);

        for (int k = 0; k < _numHotbarItems; k++) {
            // Create hotbar item slots (each slot is a UIElement Button)
            Button inventoryItem = new Button();
            inventoryItem.style.width = hotbarItemHeight;
            hotbarElement.Add(inventoryItem);
            _hotbarItems.Add(inventoryItem);
            yield return null;
        }

        _hotbarRendered = true;
        
        // update the displayed hotbar items based on what's in the inventory
        yield return StartCoroutine(PopulateHotbar());
    }

    private IEnumerator PopulateHotbar() {
        // fill up the hotbar items slot UI elements based
        // on the list of inventory items in GameState
        while (_hotbarItemsUpdating || !_hotbarRendered) {
            yield return null;
        }

        _hotbarItemsUpdating = true;

        // go through each slot in the hotbar and set its background image
        // based on the items on the GameState inventory
        for (int k = 0; k < _hotbarItems.Count; k++) {
            var hotbarSlot = _hotbarItems[k];
            int buttonNo = k;

            hotbarSlot.clicked += () => {
                // TODO: tie this to an actually useful callback during integration
                Debug.Log($"SLOT {buttonNo} CLICKED");
            };
            
            Debug.Assert(
                GameState.Inventory.Count < _hotbarItems.Count,
                "Theres more inventory items than hotbar slots available!"
            );

            if (k < GameState.Inventory.Count) {
                // set hotbar slot background image and make slot active
                var collectable = GameState.Inventory[k];
                hotbarSlot.AddToClassList(OccupiedSlotClass);
                hotbarSlot.style.backgroundImage = (
                    new StyleBackground(collectable.itemSprite)
                );
            } else {
                // remove hotbar slot background image and make slot inactive
                hotbarSlot.RemoveFromClassList(OccupiedSlotClass);
                hotbarSlot.style.backgroundImage = null;
            }

            // have the image fit within the button
            hotbarSlot.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
            yield return null;
        }

        _hotbarItemsUpdating = false;
    }

    // Start is called before the first frame update
    void Start() {
        Hide();
        StartCoroutine(InitializeHotbar());
    }
}
