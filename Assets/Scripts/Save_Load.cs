using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Save_Load 
{
    public static void Save(GameManeger scriptManeger)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SaveGame.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveConfig data = new SaveConfig(scriptManeger);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SaveConfig Load()
    {
        string path = Application.persistentDataPath + "/SaveGame.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path,FileMode.Open);
            SaveConfig data = formatter.Deserialize(stream) as SaveConfig;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save filse not found" + path);
            return null;
        }
    }
}
