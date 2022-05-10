using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataCollector : MonoBehaviour
{

    string filePath = "";
    string[] headers;
    void dataCollect() {
        string user = "";
        string level = "";
        int deaths = 0;
        int inputs = 0;
        int hitwall = 0;
        double time = caculateTime();
        string row = user + "," + level + "," +inputs+","+hitwall+","+time;
    }

    double caculateTime() {
        double start = GameManganer.Instance.levelStartTime;
        double end = GameManganer.Instance.levelEndTime;
        double time = Mathf.Abs((float)(end- start));
        GameManganer.Instance.levelStartTime = end;
        return 0;
    }

    void writeCSV(string row) {
    
    }

    void writeCSVLine(string row) {
    
    }
}
