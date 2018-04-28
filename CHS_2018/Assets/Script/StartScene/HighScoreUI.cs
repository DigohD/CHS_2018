using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HighScoreUI : MonoBehaviour {

    public Transform T_Content;

    public GameObject P_ScoreEntry;

    void Start()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://farnorthentertainment.com/CHS18_Get.php");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            ScoreList scoreList = JsonUtility.FromJson<ScoreList>(www.downloadHandler.text);

            Debug.LogWarning(scoreList.Top100Highscore.Length);

            ((RectTransform)T_Content.transform).sizeDelta = new Vector2(468, 22 * scoreList.Top100Highscore.Length);

            int i = 0;
            foreach (ScoreList.ScoreEntry entry in scoreList.Top100Highscore)
            {
                GameObject newEntry = Instantiate(P_ScoreEntry);

                if(i % 2 == 1)
                    newEntry.GetComponent<Image>().color = new Color(0.12f, 0.12f, 0.12f, 1);

                newEntry.transform.SetParent(T_Content, false);
                newEntry.transform.localPosition = new Vector3(0, -22 * i, 0);

                newEntry.transform.GetChild(0).GetComponent<Text>().text = "#" + (i + 1);
                newEntry.transform.GetChild(1).GetComponent<Text>().text = entry.Name;
                newEntry.transform.GetChild(2).GetComponent<Text>().text = entry.Score;
                i++;
            }
        }
    }
}
