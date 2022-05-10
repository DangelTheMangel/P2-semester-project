using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class dataCollector : MonoBehaviour
{

    public string filePath = "";
    public string[] headers;

    private void Start()
    {
        filePath = Application.dataPath + filePath;
    }
    public void dataCollect() {
        string user = GameManganer.Instance.nameUser;
        string level = GameManganer.Instance.sceneManganer.sceneName[GameManganer.Instance.sceneManganer.levelIndex];
        int deaths = GameManganer.Instance.deathCount;
        int inputs = GameManganer.Instance.player.inputCount;
        int hitwall = GameManganer.Instance.player.wallCollisionCount;
        double time = caculateTime();
        string row = user + "," + level + "," +inputs+","+hitwall+","+time;
        Debug.Log(row);
        writeCSV(row);
    }

    double caculateTime() {
        double start = GameManganer.Instance.levelStartTime;
        double end = GameManganer.Instance.levelEndTime;
        double time = Mathf.Abs((float)(end- start));
        GameManganer.Instance.levelStartTime = end;
        return 0;
    }

    void writeCSV(string row) {
        // TextWriter klassen skal bruges
        TextWriter tw;
        // Tjekker at der er en fil med det navn
        if (!File.Exists(filePath))
        {
            // tw indstilles til StreamWriter og til overwriting
            tw = new StreamWriter(filePath, false);
            // Skriver en linje med de to værdier der skal tilføjes
            string header = headers[0];
            for (int i = 1; i < headers.Length; i++)
            {
                header += "," + headers[i];
            }

            tw.WriteLine(header);
            // Derefter lukkes den
            tw.Close();
        }
        writeCSVLine(row);
    }

    void writeCSVLine(string row) {
        // TextWriter klassen skal bruges
        TextWriter tw;
        // tw indstilles til StreamWriter og til appending
        tw = new StreamWriter(filePath, true);
        // tw tilføjer en linje med input
        tw.WriteLine(row);
        // Derefter lukkes den igen
        tw.Close();
    }
}
