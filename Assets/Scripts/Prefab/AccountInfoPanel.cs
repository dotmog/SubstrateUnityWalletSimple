using SubstrateNetApi.Model.Types;
using SubstrateNetWallet;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Threading;
using SubstrateNetApi.Model.Types.Struct;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Numerics;
using System;

public class AccountInfoPanel : MonoBehaviour
{
    public Text BalanceValue;

    public Text BalanceLockedValue;

    public Text AvgReturnValue;

    private AccountController _accountController;

    private SceneController _sceneController;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        var rootGameObjectList = SceneManager.GetActiveScene().GetRootGameObjects().ToList();
        var mainController = rootGameObjectList.Find(p => p.name == "MainController");

        // TODO remove for live
        if (mainController != null)
        {
            _sceneController = mainController.GetComponent<SceneController>();
            _accountController = mainController.GetComponent<AccountController>();

            // waiting on scene & wallet beeing initialized!
            yield return new WaitUntil(() => 
                _sceneController.CurrentSceneState == SceneController.SceneState.Ready &&
                _accountController.Wallet.IsOnline // wallet needs to be online to run!
            );

            _accountController.Wallet.AccountInfoUpdated += OnAccountInfoUpdated;

            // initialise
            OnAccountInfoUpdated(null, _accountController.Wallet.AccountInfo);
        }
    }

    private void OnDestroy()
    {
        _accountController.Wallet.AccountInfoUpdated -= OnAccountInfoUpdated;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAccountInfoUpdated(object sender, AccountInfo accountInfo)
    {
        if (accountInfo != null)
        {
            var value = (double)accountInfo.AccountData.Free.Value / 1000000000000000;
            var valueStr = value.ToString("0.000", CultureInfo.InvariantCulture);           
            UnityMainThreadDispatcher.Instance().Enqueue(() => BalanceValue.text = valueStr);
        }
        else
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => BalanceValue.text = "");
        }
    }
}
