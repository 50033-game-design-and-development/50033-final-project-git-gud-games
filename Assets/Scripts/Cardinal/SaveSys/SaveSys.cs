using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSys {
    private static readonly string SAVE_PATH = Application.persistentDataPath + "/save.data";
    private static readonly BinaryFormatter _bForm = new();

    public static void Save() {
        var fs = new FileStream(SAVE_PATH, FileMode.Create);
        try {
            var save = new SerializableSave(GameState.save);
            _bForm.Serialize(fs, save);
        } catch (SerializationException e) {
            Debug.LogError(e);
        } finally {
            fs.Close();
        }
    }

    public static void Load() {
        if(!File.Exists(SAVE_PATH)) {
            return;
        }

        try {
            var fs = new FileStream(SAVE_PATH, FileMode.Open);
            var save = (_bForm.Deserialize(fs) as SerializableSave)!.ToSave();
            GameState.save.level = save.level;
            GameState.save.inventory = save.inventory;
        } catch (SerializationException e) {
            Debug.LogError(e);
        }
    }
}
