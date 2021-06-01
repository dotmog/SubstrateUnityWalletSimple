using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    private GameObject _mainController;

    private AccountController _accountController;

    private SceneController _sceneController;

    public Text AccountAddress;

    private Task _extrinsicTask;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        var rootGameObjectList = SceneManager.GetActiveScene().GetRootGameObjects().ToList();
        _mainController = rootGameObjectList.Find(p => p.name == "MainController");

        // TODO remove for live
        if (_mainController != null)
        {
            _accountController = _mainController.GetComponent<AccountController>();
            _sceneController = _mainController.GetComponent<SceneController>();

            AccountAddress.text = _accountController.Wallet.Account.Value; //.Substring(0,22);

            yield return new WaitUntil(() => _sceneController.CurrentSceneState == SceneController.SceneState.Ready);  
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (_extrinsicTask != null && _extrinsicTask.IsCompleted)
        {
            _extrinsicTask = null;
        }
    }

    private void OnDestroy()
    {
    }

    public void OnSettingsClicked()
    {
        Application.Quit();
    }

}
