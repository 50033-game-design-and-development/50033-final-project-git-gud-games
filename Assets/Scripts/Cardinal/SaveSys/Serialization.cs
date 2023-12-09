using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethod {
    public static Texture2D DeCompress(this Texture2D source) {
        var renderTex = RenderTexture.GetTemporary(
            source.width,
            source.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear
        );

        Graphics.Blit(source, renderTex);
        var previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        var readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}

[Serializable]
public class SerializableCollectable {
    [SerializeField] public InventoryItem itemType;
    [SerializeField] public int x;
    [SerializeField] public int y;
    [SerializeField] public byte[] texture;

    public SerializableCollectable(Inv.Collectable item) {
        var tex = item.itemSprite.texture;

        itemType = item.itemType;
        x = tex.width;
        y = tex.height;
        texture = tex.DeCompress().EncodeToPNG();
    }

    public Inv.Collectable ToCollectable() {
        var item = new Inv.Collectable();
        var tex = new Texture2D(x, y);
        tex.LoadImage(texture);

        item.itemType = itemType;
        item.itemSprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);

        return item;
    }
}

[Serializable]
public class SerializableSave {
    [SerializeField] public List<SerializableCollectable> inventory;
    [SerializeField] public int level;

    public SerializableSave(Save save) {
        level = save.level;
        inventory = save.inventory
                        .Select(item => new SerializableCollectable(item))
                        .ToList();
    }

    public Save ToSave() {
        // this is NOT meant to create a new SO instance. Ignore the warning
        var save = new Save();

        save.level = level;
        save.inventory = inventory
                         .Select(item => item.ToCollectable())
                         .ToList();

        return save;
    }
}