using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour
{
    public static void SaveFrame(Frame _frame)
    {
        BinaryFormatter formatter = new BinaryFormatter(); //declare new formatter
        string path = Application.persistentDataPath + _frame.shortName + ".funni"; //save data, file name = name of frame
        FileStream stream = new FileStream(path, FileMode.Create); //preparing to write data

        FrameData data = new FrameData(_frame);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static FrameData LoadFrame(Frame _frame)
    {
        string path = Application.persistentDataPath + _frame.shortName + ".funni";
        if(File.Exists(path))
        {
            //frame data exists! loading data...
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            FrameData data = formatter.Deserialize(stream) as FrameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null; //frame data not found, return nothing
        }
    }
}
