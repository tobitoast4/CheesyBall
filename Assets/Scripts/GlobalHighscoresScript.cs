using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;
using UnityEngine.UI;

public class GlobalHighscoresScript : MonoBehaviour{

    private string username;
    public TextMeshProUGUI your_name;
    public TextMeshProUGUI secondHeading;

    public GameObject rowPrefab;
    public Transform rowsParent;
    public RectTransform leaderboardContent;

    private string currentLeaderboard;
    private Vector2 leaderboardContentSize;



    void Start(){
        leaderboardContentSize = leaderboardContent.sizeDelta;
        currentLeaderboard = "CheesyBallHighestJumps";
        username = PlayerPrefs.GetString("username");
        your_name.text = "You: " + username;
        login();
    }


    void Update() {
        if (Input.GetKey(KeyCode.Escape)) {
            back();
        }
    }

    public void back() {
        SceneManager.LoadScene(2);
    }

    public void loadnextLeaderboardRight() {
        if (currentLeaderboard == "CheesyBallHighestJumps") {
            currentLeaderboard = "CheesyBallLongestJumps";
            secondHeading.text = "Longest Jump";
        } else if (currentLeaderboard == "CheesyBallLongestJumps") {
            currentLeaderboard = "CheesyBallTotalDistance";
            secondHeading.text = "Total Distance Travelled";
        } else if (currentLeaderboard == "CheesyBallTotalDistance") {
            currentLeaderboard = "CheesyBallHighestJumps";
            secondHeading.text = "Highest Jump";
        }
        getLeaderboard();
    }

    public void loadnextLeaderboardLeft() {
        if (currentLeaderboard == "CheesyBallHighestJumps") {
            currentLeaderboard = "CheesyBallTotalDistance";
            secondHeading.text = "Total Distance Travelled";
        } else if (currentLeaderboard == "CheesyBallTotalDistance") {
            currentLeaderboard = "CheesyBallLongestJumps";
            secondHeading.text = "Longest Jump";
        }
        else if (currentLeaderboard == "CheesyBallLongestJumps") {
            currentLeaderboard = "CheesyBallHighestJumps";
            secondHeading.text = "Highest Jump";
        }
        getLeaderboard();
    }


    public void login() {
        var request = new LoginWithPlayFabRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.Username = username;
        request.Password = username + "123456";

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult obj) {
        Debug.Log("Login sucessfull" + " (User: " + username + ")");
        getLeaderboard();
        sendLeaderboard("CheesyBallHighestJumps", Mathf.RoundToInt(PlayerPrefs.GetFloat("highestJump_Highscore")));
        sendLeaderboard("CheesyBallLongestJumps", Mathf.RoundToInt(PlayerPrefs.GetFloat("longestJump_Highscore")));
        sendLeaderboard("CheesyBallTotalDistance", Mathf.RoundToInt(PlayerPrefs.GetFloat("totalDistanceTravelled")));
    }

    private void OnLoginFailure(PlayFabError obj) {
        Debug.Log("Login failure" + " (User: " + username + ")");
    }


    public void sendLeaderboard(string leaderboard, int score) {
        var request = new UpdatePlayerStatisticsRequest {
            Statistics = new List<StatisticUpdate> {
                new StatisticUpdate {
                    StatisticName = leaderboard,
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSendLeaderboardSuccess, OnSendLeaderboardFailure);
    }

    public void OnSendLeaderboardSuccess(UpdatePlayerStatisticsResult reult) {
        Debug.Log("Successfully leaderboard sent" + " (User: " + username + ")");
    }

    public void OnSendLeaderboardFailure(PlayFabError error) {
        Debug.Log("Leaderboard error" + " (User: " + username + ")");
    }


    public void getLeaderboard() {
        var request = new GetLeaderboardRequest {
            StatisticName = currentLeaderboard,
            StartPosition = 0
           
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
    }

    private void OnGetLeaderboardSuccess(GetLeaderboardResult obj) {
        Debug.Log("Leaderboard loaded successfully");

        foreach (Transform item in rowsParent) {
            Destroy(item.gameObject);
            
        }
        leaderboardContent.sizeDelta = leaderboardContentSize;

        int n = 0;
        foreach (var item in obj.Leaderboard) {
            //for (int i = 0; i < 4; i++) {
                GameObject newGo = Instantiate(rowPrefab, rowsParent);
                TextMeshProUGUI[] texts = newGo.GetComponentsInChildren<TextMeshProUGUI>();
                Debug.Log("Length " + texts.Length);
                texts[0].text = (item.Position + 1).ToString();
                texts[1].text = item.DisplayName.ToString();
                texts[2].text = item.StatValue.ToString();
                if (n > 5) {
                    leaderboardContent.sizeDelta = new Vector2(leaderboardContent.sizeDelta.x, leaderboardContent.sizeDelta.y + 30);
                }
                n++;
            //}
        }
    }

    private void OnGetLeaderboardFailure(PlayFabError obj) {
        Debug.Log("Leaderboard loaded failed");
    }
}
