using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayFabRegisterScript : MonoBehaviour {

    private string username;
    public GameObject error_message_go;
    public InputField username_input;
    private string old_username_text;

    void Start() {
        username = "Test9";
        error_message_go.SetActive(false);
        //register("Test2");
    }


    void Update() {
        if (username_input.text.ToString() != old_username_text) {
            error_message_go.SetActive(false);
        }
        old_username_text = username_input.text.ToString();
    }

    public void proceed() {
        username = username_input.text.ToString();
        register();
    }


    public void register() {
        //username = n_username;
        var request = new RegisterPlayFabUserRequest();
        request.TitleId = PlayFabSettings.TitleId;
        request.Email = username + "@gmail.com";
        request.Username = username;
        request.DisplayName = username;
        request.Password = username + "123456";

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult obj) {
        PlayerPrefs.SetString("username", username_input.text.ToString());
        Debug.Log("TestUser register sucessfull" + " (User: " + username + ")");
        SceneManager.LoadScene(4);

    }

    private void OnRegisterFailure(PlayFabError obj) {
        Debug.Log("TestUser register failure" + " (User: " + username + ")");
        error_message_go.SetActive(true);
    }
}
