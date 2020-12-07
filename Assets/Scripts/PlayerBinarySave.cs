using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class PlayerBinarySave : MonoBehaviour
{
    public static void SavePlayerData(Player player)
    {
        //Reference for a binary formatter
        BinaryFormatter formatter = new BinaryFormatter();
        //Location to Save
        string path = Application.dataPath + "/playerSave.sav";
        //Create file at file path
        FileStream stream = new FileStream(path, FileMode.Create);
        //Serialize Player and Save it to a File
        formatter.Serialize(stream, player);
        //Close the File
        stream.Close();

    }
    public static Player LoadPlayerData()
    {
        //Location to Load from
        string path = Application.dataPath+"/playerSave.sav" ;
        //if we have a file at that path
        if (File.Exists(path))
        {
            //get the binary formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //read the data from the path
            FileStream stream = new FileStream(path, FileMode.Open);
            //Deserialize back to a usable variable

            Player data =(Player) formatter.Deserialize(stream);//as player
            //close the file
            stream.Close();
            return data;
        }
        return null;
    }
}
