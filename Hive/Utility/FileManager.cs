using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hive.Utility;

public static class FileManager
{
    private static string path = "";
    static string fileName = "savedata";
    public static string FilePath => path;

    public static string FileString
    {
        get
        {
            return $"{path}/{fileName}.json";
        }
    }

    public static void CreateDirectory()
    {
        string appDataLocal = Directory.GetParent(Path.GetTempPath()).Parent.FullName;
        string folder = "The Hive";

        if (!Directory.Exists($"{appDataLocal}/{folder}"))
        {
            path = Directory.CreateDirectory($"{appDataLocal}/{folder}").FullName;
        }
        else
        {
            path = $"{appDataLocal}/{folder}";
        }

        if (!File.Exists(FileString))
        {
            File.Create(FileString);
        }
    }

    public static async Task WriteToFile(int sempahoreCount, Color antColor)
    {
        if (path != string.Empty && fileName != string.Empty)
        {
            var data = new SaveData(sempahoreCount, antColor.ToString());
            string myDeserializedClass = JsonConvert.SerializeObject(data);

            await File.WriteAllTextAsync(FileString, myDeserializedClass);

            //using (StreamWriter file = File.CreateText($"{path}/{fileName}.json"))
            //{
            //    using (JsonTextWriter writer = new JsonTextWriter(file))
            //    {
            //        JObject.FromObject(new SaveData
            //        {
            //            semaphores = 1,
            //            antcolor = "White"
            //        }).WriteTo(writer);
            //    }
            //}
            
        }

    }

    public static void ReadFromFile(out SaveData data)
    {
        String line;
        string filecontents;
        using (StreamReader sr = File.OpenText(FileString))
        {
            line = sr.ReadLine();
            filecontents = line;
            while (line != null)
            {
                line = sr.ReadLine();
                filecontents += line;
            }
            sr.Close();
        }
        data = JsonConvert.DeserializeObject<SaveData>(filecontents);
    }
}

[Serializable]
public class SaveData
{
    public SaveData(int semaphores, string antcolor)
    {
        this.semaphores = semaphores;
        this.antcolor = antcolor;
    }

    public int semaphores { get; set; }
    public string antcolor { get; set; }
}