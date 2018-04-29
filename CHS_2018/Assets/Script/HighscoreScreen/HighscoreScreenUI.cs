using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreScreenUI : MonoBehaviour {

    public GameObject G_PostUI;
    public GameObject G_FailUI;

    public GameObject G_SuccessText;

    public Button postButton;

    private string inputName;

    public Text scoreText;

    private void Start()
    {
        postButton.interactable = false;
        postButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        postButton.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0.3f);

        scoreText.text = "Post Score: " + GameControl.score;
        scoreText.transform.GetChild(0).GetComponent<Text>().text = "Post Score: " + GameControl.score;

        StartCoroutine(GetStatus());
    }

    IEnumerator GetStatus()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://farnorthentertainment.com/CHS18_Check.php?score=" + GameControl.score);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            bool madeIt = bool.Parse(www.downloadHandler.text);

            if (madeIt)
            {
                G_PostUI.SetActive(true);
            }
            else
            {
                G_FailUI.SetActive(true);
            }
        }
    }

    public void onNameValueChange(string name)
    {
        if(name.Length < 1)
        {
            postButton.interactable = false;
            postButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            postButton.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0.3f);
        }
        else
        {
            postButton.interactable = true;
            postButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            postButton.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
        }

        inputName = name;
    }

    public void onPostButtonClick()
    {
        postButton.interactable = false;

        StartCoroutine(PostScore());
    }

    IEnumerator PostScore()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://farnorthentertainment.com/CHS18.php?name=" + inputName + "&score=" + GameControl.score);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            G_SuccessText.SetActive(true);
            G_SuccessText.GetComponent<Text>().text = "Server Error!";
            G_SuccessText.GetComponent<Text>().color = new Color(1, 0, 0, 1);
            Destroy(postButton.gameObject);
            Debug.Log(www.error);
        }
        else
        {
            G_SuccessText.SetActive(true);
            Destroy(postButton.gameObject);
        }
    }

    public void onLeaveClick()
    {
        SceneManager.LoadScene("StartScene");
    }
}
