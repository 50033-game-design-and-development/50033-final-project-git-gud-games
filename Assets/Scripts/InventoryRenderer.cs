using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

public class InventoryRenderer: MonoBehaviour {
    // public Image hotbarBackground;
    public UIDocument hotbarUI;
    public GameObject hotbarPrefab;

    private float _padding;
    // gap between bar item images
    public int hotbarItemGap = 40;
    private List<GameObject> _hotbarItems = new List<GameObject>();

    public int CalculateNumberOfHotbarItems(
        RectTransform rectTransform, float hotbarItemSize
    ) {
        VisualElement root = hotbarUI.rootVisualElement;
        VisualElement hotbarElement = root.Q("Hotbar");
        _padding = hotbarElement.resolvedStyle.paddingLeft;

        var imageRect = rectTransform.rect;
        var hotbarWidth = imageRect.width;
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
    
    private IEnumerator PopulateInventory() {
        // wait for UI document to render
        yield return null;
        var rectTransform = hotbarUI.GetComponent<RectTransform>();
        
        VisualElement root = hotbarUI.rootVisualElement;
        VisualElement hotbarElement = root.Q<VisualElement>("Hotbar");
        _padding = (
            hotbarElement.resolvedStyle.paddingLeft +
            hotbarElement.resolvedStyle.paddingRight
        ) / 2.0f;
        
        var imageRect = rectTransform.rect;
        var hotbarHeight = imageRect.height;
        Debug.Log("HOTBAR_HEIGHT " + hotbarHeight);
        var hotbarItemHeight = hotbarHeight - _padding * 2;
        Debug.Log("HOTBAR_ITEM_HEIGHT " + hotbarItemHeight);
        var numHotbarItems = CalculateNumberOfHotbarItems(
            rectTransform, hotbarItemHeight
        );

        for (int k = 0; k < numHotbarItems; k++) {
            // Create a new button.
            Button inventoryItem = new Button();
            inventoryItem.style.width = hotbarItemHeight;
            hotbarElement.Add(inventoryItem);
        }
        
        yield return null;
    }
    
    // Start is called before the first frame update
    void Start() {
        VisualElement root = hotbarUI.rootVisualElement;
        var buttons = root.Query<Button>().ToList();
        // Remove pre-existing buttons from the inventory 
        foreach (var button in buttons) {
            button.RemoveFromHierarchy();
        }
        
        StartCoroutine(PopulateInventory());
    }

    // Update is called once per frame
    void Update() {
        
    }
}
