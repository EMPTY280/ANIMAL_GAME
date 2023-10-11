using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvReader
{
    public List<Dialogue> Read (string _csvFileName)
    {
        List<Dialogue> dialogues = new List<Dialogue>();
        TextAsset csvData = Resources.Load<TextAsset>(_csvFileName);        

        string fileText = csvData.text;
        string[] data = fileText.Split("\r\n");

        for(int i=1;i<data.Length-1; i++)
        {
            Dialogue dialogue = new Dialogue();

            string[] row = data[i].Split( new char[] { ',' } );

            dialogue.segNum = int.Parse(row[0]);
            dialogue.textTime = float.Parse(row[1]);
            dialogue.questCondition = int.Parse(row[2]);
            row[3] = row[3].Replace("&", "\n");
            dialogue.text = row[3].Replace('#', ',');

            dialogues.Add(dialogue);
        }

        return dialogues;
    }

}
