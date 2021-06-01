using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class LocalizationController : MonoBehaviour
{
    private static LocalizationController _instance;

    private Dictionary<string, string> _localizedText;
    private bool _isReady = false;
    private string _missingTextString = "Localized text not found";

    private void Awake()
    {
        LoadLocalizedText("language_eng.json");
    }

    public static LocalizationController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LocalizationController>();
            }

            return _instance;
        }
    }

    public void LoadLocalizedText2(string fileName)
    {
        Debug.Log($"loading {fileName}.");
        _localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                _localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            Debug.Log("Data loaded, dictionary contains: " + _localizedText.Count + " entries");
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }

        _isReady = true;
    }

    public void LoadLocalizedText(string fileName)
    {
        Debug.Log($"loading {fileName}.");
        _localizedText = new Dictionary<string, string>();

        var loadingRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, fileName));
        loadingRequest.SendWebRequest();

        while (!loadingRequest.isDone)
        {
            if (loadingRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("UnityWebRequest got a ConnectionError!");
                return;
            }
        }

        if (loadingRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError($"UnityWebRequest wasn't success full, {loadingRequest.result}!");
            return;
        }

        string dataAsJson = loadingRequest.downloadHandler.text;
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        for (int i = 0; i < loadedData.items.Length; i++)
        {
            _localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        Debug.Log("Data loaded, dictionary contains: " + _localizedText.Count + " entries");

        // to write
        //File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "your.bytes"), loadingRequest.downloadHandler.data);

    }

    public string GetLocalizedValue(string key)
    {
        string result = _missingTextString;
        if (_localizedText.ContainsKey(key))
        {
            result = _localizedText[key];
        }

        return result;

    }

    public bool GetIsReady()
    {
        return _isReady;
    }

    public void LoadLocalizedTextFromFunction()
    {
        _localizedText = new Dictionary<string, string>();

        // login scene (english)
        _localizedText.Add("LoginState.None", "None");
        _localizedText.Add("LoginState.Load", "Load");
        _localizedText.Add("LoginState.Ready", "Ready");
        _localizedText.Add("LoginState.Unlock", "Unlock");
        _localizedText.Add("LoginState.Create", "Create");
        _localizedText.Add("LoginState.Error", "Error");
        _localizedText.Add("LoginState.Mnemonic", "Mnemonic");
        _localizedText.Add("LoginState.Select", "Select");
        
        Save(_localizedText, "language_eng.json");
    }

    private void Save(Dictionary<string, string> dict, string fileName)
    {
        var loca = new LocalizationData();
        List<LocalizationItem> list = new List<LocalizationItem>();
        foreach(var keyValue in _localizedText)
        {
            list.Add(new LocalizationItem() { key = keyValue.Key, value = keyValue.Value});
        }
        loca.items = list.ToArray();
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        File.WriteAllText(filePath, JsonUtility.ToJson(loca));

    }
}