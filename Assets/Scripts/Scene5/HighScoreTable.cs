using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    [SerializeField]
    private List<HighScoreEntry> highScoreEntries;
    private List<Transform> highScoreEntryTransformList;
    private void Awake()
    {
        TextAsset scoreText = Resources.Load<TextAsset>("scores"); //GET SCORES.CSV and split it in next line
        string[] data = scoreText.text.Split('\n');

        string path = Application.dataPath + "/Resources/current.csv"; //Currentscorefile
        string content = "";

        if (!File.Exists(path)){
            content = "Anil,0";
            File.WriteAllText(path, content);
        }else
            content = File.ReadAllText(path);
        

        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemp");

        entryTemplate.gameObject.SetActive(false);

        highScoreEntries = new List<HighScoreEntry>();
        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(','); //get col

            HighScoreEntry h = new HighScoreEntry();

            h.Name = row[0];
            try{
                h.Score = Convert.ToInt32(row[1]);
            }catch(Exception ex)
            { Console.WriteLine("Exception" + ex); }
            
            highScoreEntries.Add(h);
        }

        string[] cur = content.Split(',');
        highScoreEntryTransformList = new List<Transform>();
        highScoreEntries.Sort((first, second) => first.Score.CompareTo(second.Score));


        HighScoreEntry hSE = highScoreEntries.Where(x => x.Score < Convert.ToInt32(content.Split(',')[1])).LastOrDefault();

        if (hSE != null)
        {
            hSE.Name = cur[0];
            hSE.Score = Convert.ToInt32(cur[1]);

            highScoreEntries.Sort((first, second) => second.Score.CompareTo(first.Score));
            highScoreEntries.RemoveRange(10, highScoreEntries.Count - 10);
        }

        foreach (var highScoreEntry in highScoreEntries)
            CreateHighScoreEntry(highScoreEntry, entryContainer, highScoreEntryTransformList);


        /*Highscores highscores = new Highscores {highscoreEntryList = highScoreEntries };
        string json = JsonUtility.ToJson(highScoreEntries.ToArray()[0].Name);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));*/
        string output = "Name,Score";

        for (int i = 0; i < highScoreEntries.Count; i++)
        {
            output += "\n" + highScoreEntries.ElementAt(i).Name + "," + highScoreEntries.ElementAt(i).Score;
        }
        File.WriteAllText(Application.dataPath + "/Resources/scores.csv", output);
    }

    private void CreateHighScoreEntry(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformlist)
    {
        float tempHeight = 50f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -tempHeight * transformlist.Count);
        entryTransform.gameObject.SetActive(true);

        entryTransform.Find("posText").GetComponent<Text>().text = "" + (transformlist.Count + 1);
        entryTransform.Find("scoreText").GetComponent<Text>().text = "" + highScoreEntry.Score;
        entryTransform.Find("nameText").GetComponent<Text>().text = "" + highScoreEntry.Name;

        transformlist.Add(entryTransform);

    }

    [Serializable]
    private class HighScoreEntry
    {
        public int Score { get; set; }
        public string Name { get; set; }
    }
}
