using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;

public class FirebaseSetup : MonoBehaviour
{
    public class firebaseData {
        public bool isShow;
        public string GameName;
        public bool isPlayable;
    }
    [SerializeField] GameObject mainScene;
    [SerializeField] GameObject networkManager;
    public static firebaseData fd = new firebaseData();
        System.Collections.Generic.Dictionary<string, object> defaults =
  new System.Collections.Generic.Dictionary<string, object>();
    // Start is called before the first frame update
    void Start()
    {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {

                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                var app = Firebase.FirebaseApp.DefaultInstance;
                Debug.Log("Firebase init");
                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
            Debug.Log("Firebase failed");

                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });
  
        defaults.Add("isShow", true);
        defaults.Add("GameName", "Carrom Chronicles");
        defaults.Add("isPlayable",true);
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWithOnMainThread((task)=> {
            FetchDataAsync();
            });

        
    }



    public Task FetchDataAsync() {
        Debug.Log("Fetching data...");
        System.Threading.Tasks.Task fetchTask =
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWith(FetchComplete);
    }


    private void FetchComplete(Task fetchTask) {
        if (!fetchTask.IsCompleted) {
            Debug.LogError("Retrieval hasn't finished.");
            return;
        }

        var remoteConfig = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if(info.LastFetchStatus != Firebase.RemoteConfig.LastFetchStatus.Success) {
            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            return;
        }

        // Fetch successful. Parameter values must be activated to use.
        remoteConfig.ActivateAsync()
            .ContinueWith(
            task => {
                Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

                fd.isShow= FirebaseRemoteConfig.DefaultInstance.GetValue("isShow").BooleanValue;
                fd.GameName = FirebaseRemoteConfig.DefaultInstance.GetValue("GameName").StringValue;
                fd.isPlayable= FirebaseRemoteConfig.DefaultInstance.GetValue("isPlayable").BooleanValue;
                Debug.Log(fd.GameName);
                mainScene.SetActive(true);
                networkManager.SetActive(true);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
