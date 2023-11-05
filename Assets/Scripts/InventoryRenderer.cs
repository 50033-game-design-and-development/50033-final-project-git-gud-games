using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRenderer: MonoBehaviour {
    public Image hotbarBackground;
    public GameObject hotbarPrefab;

    public int padding = 20;
    // gap between bar item images
    public int hotbarItemGap = 40;
    private List<GameObject> _hotbarItems = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start() {
        var rectTransform = hotbarBackground.rectTransform;
        var imageRect = rectTransform.rect;
        var width = imageRect.width;
        var height = imageRect.height;
        var hotbarLeftmostX = (
            rectTransform.anchoredPosition.x - 
            rectTransform.rect.width * rectTransform.pivot.x
        );
        Vector2 hotbarScreenLeftmostX = RectTransformUtility.WorldToScreenPoint(
            null, rectTransform.TransformPoint(
                new Vector3(hotbarLeftmostX, 0f, 0f)
            )
        );

        // Log the dimensions
        Debug.Log($"Image Dimensions: Width = {width}, Height = {height}");
        var usableWidth = width - padding * 2;
        var hotbarItemSize = height - padding * 2;
        /*
        n - number of hotbar items
        H - hotbar item size
        g - gap size
    
        (n-1)*g + n*H = u
        n*g - g + n*H = u
        n*(g+H) - g = u
        n = (u+g) / (g+H)
        */
        var numHotbarItems = (int) Math.Floor(
            (usableWidth + hotbarItemGap) / (hotbarItemSize + hotbarItemGap)
        );
        var remainingSpace = (float) Math.Floor(
            usableWidth - numHotbarItems * hotbarItemSize - 
            (numHotbarItems - 1) * hotbarItemGap
        );
        
        Debug.Log("REM_SPACE " + remainingSpace);
        for (var k = 0; k < numHotbarItems; k++) {
            // calculate position of the hotbar item relative
            // to the center of the hotbar background
            var itemPosition = new Vector3(
                (k + 0.5f) * hotbarItemSize + hotbarItemGap * k -
                width / 2.0f + hotbarScreenLeftmostX.x,
                0.0f, 0.0f
            );
            GameObject instance = Instantiate(
                hotbarPrefab, Vector3.zero, Quaternion.identity,
                hotbarBackground.transform
            );
            instance.transform.localPosition = itemPosition;
            instance.GetComponent<RectTransform>().sizeDelta = new Vector2(
                hotbarItemSize, hotbarItemSize
            );
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
