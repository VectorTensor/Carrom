using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;

public class FirebaseSetup : MonoBehaviour
{
    public class firebaseData
    {
        public bool isShow;
        public string GameName;
        public bool isPlayable;
    }

    Dictionary<string, object> defaults = new Dictionary<string, object>();

    public static firebaseData fd = new firebaseData();

    [SerializeField] GameObject mainScene;
    [SerializeField] GameObject networkManager;


    void Start()
    {
        //Firebase Config
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
                Debug.LogError(String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        //in-app default values
        defaults.Add("isShow", true);
        defaults.Add("GameName", "Carrom Chronicles");
        defaults.Add("isPlayable", false);

        FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWithOnMainThread((task) => {
            FetchDataAsync();
            FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener += ConfigUpdateListenerEventHandler;
        });
    }

    //fetch values from remote config backend
    public Task FetchDataAsync()
    {
        Debug.Log("Fetching data...");
        Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        return fetchTask.ContinueWith(FetchComplete);
    }

    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("Retrieval hasn't finished.");
            return;
        }

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            return;
        }

        // Fetch successful. Parameter values must be activated to use.
        remoteConfig.ActivateAsync().ContinueWith(
            task => {

                Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

                fd.isShow= FirebaseRemoteConfig.DefaultInstance.GetValue("isShow").BooleanValue;
                fd.GameName = FirebaseRemoteConfig.DefaultInstance.GetValue("GameName").StringValue;
                fd.isPlayable= FirebaseRemoteConfig.DefaultInstance.GetValue("isPlayable").BooleanValue;

                Debug.Log(fd.GameName);
                Debug.Log(fd.isPlayable);

                mainScene.SetActive(true);
                networkManager.SetActive(true);
            });
    }

    // Handle real-time Remote Config events. (Subscriber)
    void ConfigUpdateListenerEventHandler(object sender, ConfigUpdateEventArgs args)
    {
        if (args.Error != RemoteConfigError.None)
        {
            Debug.Log(String.Format("Error occurred while listening: {0}", args.Error));
            return;
        }

        Debug.Log("Updated keys: " + string.Join(", ", args.UpdatedKeys));

        // Activate all fetched values and then display a welcome message.
        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        remoteConfig.ActivateAsync().ContinueWith(
          task => {
              
              //DisplayWelcomeMessage();
              fd.isShow= FirebaseRemoteConfig.DefaultInstance.GetValue("isShow").BooleanValue;
              fd.GameName = FirebaseRemoteConfig.DefaultInstance.GetValue("GameName").StringValue;
              fd.isPlayable= FirebaseRemoteConfig.DefaultInstance.GetValue("isPlayable").BooleanValue;

              Debug.Log(fd.GameName);
              Debug.Log(fd.isPlayable);
          });
    }

    // Stop the listener.
    void OnDestroy()
    {
        FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener -= ConfigUpdateListenerEventHandler;
    }
}
