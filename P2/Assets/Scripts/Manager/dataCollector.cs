using System.IO;
using UnityEngine;

public class dataCollector : MonoBehaviour
{

    public string filePath = "";
    public string[] headers;

    private void Start()
    {
        filePath = Application.persistentDataPath + filePath;
    }
    public void dataCollect()
    {
        string user = GameManganer.Instance.nameUser;
        string level = GameManganer.Instance.sceneManganer.sceneName[GameManganer.Instance.sceneManganer.levelIndex];
        int deaths = GameManganer.Instance.deathCount;
        int inputs = GameManganer.Instance.player.inputCount;
        int hitwall = GameManganer.Instance.player.wallCollisionCount;
        double time = caculateTime();
        string row = user + "," + level + "," + deaths + "," + inputs + "," + hitwall + "," + time;
        GameManganer.Instance.deathCount = 0;
        Debug.Log(row);
        writeCSV(row);
    }

    double caculateTime()
    {
        double start = GameManganer.Instance.levelStartTime;
        double end = GameManganer.Instance.levelEndTime;
        double time = Mathf.Abs((float)(end - start));
        GameManganer.Instance.levelStartTime = end;
        return time;
    }

    void writeCSV(string row)
    {
        
        TextWriter tw; // The TextWriter class is needed
        
        if (!File.Exists(filePath)) // Checks if a file with the same name already exists
        {
            tw = new StreamWriter(filePath, false); // tw is set to StreamWriter and to overwriting
            
            string header = headers[0]; // Writes a line with the two added values
            for (int i = 1; i < headers.Length; i++)
            {
                header += "," + headers[i];
            }

            tw.WriteLine(header);
            tw.Close(); // Afterwards it is closed
        }
        writeCSVLine(row);
    }

    void writeCSVLine(string row)
    {
        TextWriter tw; // TextWriter class is needed
        tw = new StreamWriter(filePath, true); // tw is set to StreamWriter and enables appending
        tw.WriteLine(row); // tw adds a line with input
        tw.Close(); // Lastly its closed again
    }
}
