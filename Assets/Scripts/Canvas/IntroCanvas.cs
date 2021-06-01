using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCanvas : MonoBehaviour
{
    private GameObject _mainController;

    private AccountController _accountController;

    private SceneController _sceneController;

    private LocalizationController _localizationController;

    void Start()
    {
        var rootGameObjectList = SceneManager.GetActiveScene().GetRootGameObjects().ToList();
        _mainController = rootGameObjectList.Find(p => p.name == "MainController");
        _accountController = _mainController.GetComponent<AccountController>();
        _localizationController = _mainController.GetComponent<LocalizationController>();
        _sceneController = _mainController.GetComponent<SceneController>();
        _localizationController.LoadLocalizedText("language_eng.json");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartClicked()
    {
        _sceneController.ChangeSceneState(SceneController.SceneName.LoginScene, 1);
    }
}
