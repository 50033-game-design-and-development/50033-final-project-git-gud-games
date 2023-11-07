using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryRenderer : MonoBehaviour {
    // hotbar USS slot class to assign to filled inventory slots
    private static readonly string OCCUPIED_SLOT_CLASS = "filled";

    public UIDocument hotbarUI;
    public int hotbarItemGap = 40;

    private float padding;
    private int numHotbarItems;

    // gap between bar item images
    private readonly List<Button> hotbarItems = new List<Button>();

    // whether or not hotbar item slots have been rendered
    private bool rendered = false;

    // whether or not hotbar item slots are currently being updated
    private bool updating = false;

    public void OnInventoryUpdate() {
        if (GameState.IsInventoryOpened()) {
            Show();
            StartCoroutine(PopulateInventory());
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
        padding = hotbarElement.resolvedStyle.paddingLeft;
        var usableWidth = hotbarWidth - padding * 2;
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
        var numHotbarItems = (int)Math.Floor((usableWidth + hotbarItemGap) / (hotbarItemSize + hotbarItemGap));
        return numHotbarItems;
    }

    private IEnumerator InitializeInventory() {
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

        var rectTransform = hotbarUI.GetComponent<RectTransform>();
        VisualElement hotbarElement = root.Q<VisualElement>("Hotbar");
        padding = (
            hotbarElement.resolvedStyle.paddingLeft + hotbarElement.resolvedStyle.paddingRight
        ) / 2.0f;

        var hotbarWidth = hotbarElement.resolvedStyle.width;
        var hotbarHeight = hotbarElement.resolvedStyle.height;
        Debug.Log("HOTBAR_HEIGHT " + hotbarHeight);
        var hotbarItemHeight = hotbarHeight - padding * 2;
        Debug.Log("HOTBAR_ITEM_HEIGHT " + hotbarItemHeight);
        numHotbarItems = CalculateNumberOfHotbarItems(hotbarWidth, hotbarItemHeight);

        for (int k = 0; k < numHotbarItems; k++) {
            // Create hotbar item slots (each slot is a UIElement Button)
            Button inventoryItem = new Button();
            inventoryItem.style.width = hotbarItemHeight;
            hotbarElement.Add(inventoryItem);
            hotbarItems.Add(inventoryItem);
            yield return null;
        }

        rendered = true;
    }

    private IEnumerator PopulateInventory() {
        // fill up the hotbar items slot UI elements based
        // on the list of collectable items in GameState
        while (!rendered) {
            yield return null;
        }

        while (updating) {
            yield return null;
        }

        updating = true;

        for (int k = 0; k < hotbarItems.Count; k++) {
            var hotbarSlot = hotbarItems[k];
            int buttonNo = k;

            hotbarSlot.clicked += () => {
                // TODO: tie this to an actually useful callback during integration
                Debug.Log($"SLOT {buttonNo} CLICKED");
            };

            if (k < GameState.inventory.Count) {
                var collectable = GameState.inventory[k];
                hotbarSlot.AddToClassList(OCCUPIED_SLOT_CLASS);
                hotbarSlot.style.backgroundImage = (
                                                       new StyleBackground(collectable.itemSprite)
                                                   );
            } else {
                hotbarSlot.RemoveFromClassList(OCCUPIED_SLOT_CLASS);
                hotbarSlot.style.backgroundImage = null;
            }

            // have the image fit within the button
            hotbarSlot.style.unityBackgroundScaleMode = ScaleMode.ScaleToFit;
            yield return null;
        }

        updating = false;
    }

    // Start is called before the first frame update
    void Start() {
        Hide();
        StartCoroutine(InitializeInventory());
        StartCoroutine(PopulateInventory());
    }
}
