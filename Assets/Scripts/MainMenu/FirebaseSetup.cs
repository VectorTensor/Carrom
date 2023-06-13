using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.RemoteConfig;
using Firebase;

public class FirebaseSetup : MonoBehaviour
{
    // Callback function for scene change
    public static event Action OnSceneChange;

    public bool firebaseIsReady = false;

    public class DataFromFirebase
    {
        public string GameName;
        public bool isPlayable;
        public bool isShow;
    }

    Dictionary<string, object> defaults = new Dictionary<string, object>();

    public static DataFromFirebase fd = new DataFromFirebase();

    void Awake()
    {
        //Firebase Config
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("Firebase successful");
                firebaseIsReady = true;
            }
            else
            {
                Debug.Log("Firebase failed");
                Debug.LogError(String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                firebaseIsReady = false;
            }
        });
    }

    void Update()
    {
        if (firebaseIsReady)
        {
            Debug.Log("Setting values");

            //in-app default values
            DataFromFirebase defaultDatas = new DataFromFirebase();
            defaultDatas.GameName = "Carrom";
            defaultDatas.isPlayable = true;
            defaultDatas.isShow = true;

            string json = JsonUtility.ToJson(defaultDatas);
            defaults.Add("Carrom", json);

            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWithOnMainThread((task) =>
            {
                FetchDataAsync();
                FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener += ConfigUpdateListenerEventHandler;
            });

            firebaseIsReady = false;
        }
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

                string data = FirebaseRemoteConfig.DefaultInstance.GetValue("CarromJson").StringValue;
                var loadedData = JsonUtility.FromJson<DataFromFirebase>(data);

                Debug.Log(loadedData.GameName);
                Debug.Log(loadedData.isPlayable);
                Debug.Log(loadedData.isShow);

                fd.GameName = loadedData.GameName;
                fd.isPlayable = loadedData.isPlayable;
                fd.isShow = loadedData.isShow;

                // Invoke the callback when scene change is required
                //OnSceneChange?.Invoke();
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
              var realData = FirebaseRemoteConfig.DefaultInstance.GetValue("Carrom").StringValue;

              var loadedRealData = JsonUtility.FromJson<DataFromFirebase>(realData);

              fd.GameName = loadedRealData.GameName;
              fd.isPlayable = loadedRealData.isPlayable;
              fd.isShow = loadedRealData.isShow;
          });
    }

    // Stop the listener.
    void OnDestroy()
    {
        FirebaseRemoteConfig.DefaultInstance.OnConfigUpdateListener -= ConfigUpdateListenerEventHandler;
    }
}
