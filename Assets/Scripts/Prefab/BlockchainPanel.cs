using SubstrateNetApi;
using SubstrateNetWallet;
using System.Collections;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlockchainPanel : MonoBehaviour
{
    public Text NodeInfo;

    public Text BlockNumber;

    public Text ChainName;

    public Text ChainVersion;

    public Text SubstrateVersion;

    public Text EpochValue;

    public Text EraValue;

    public TrackerBar BlockTrackerBar;

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
                _sceneController.CurrentSceneState == SceneController.SceneState.Ready);

            _accountController.Wallet.ChainInfoUpdated += OnChainInfoUpdated;

            // initialise
            OnChainInfoUpdated(null, _accountController.Wallet.ChainInfo);
        }
    }

    private void OnDestroy()
    {
        _accountController.Wallet.ChainInfoUpdated -= OnChainInfoUpdated;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnChainInfoUpdated(object sender, ChainInfo chainInfo)
    {
        if (chainInfo != null)
        {
            var blockNumber = chainInfo.BlockNumber;

            var barFill = (float)(blockNumber % 6) / 5;
            // TODO: this needs to be verified epoche 200 blocks 10 min, era 1200 blocks 60 min
            var blocknumberExt = blockNumber + 616;
            var blocksTillEpoch = blocknumberExt % 200;
            var epochPerc = (double)blocksTillEpoch / 200;
            var blocksTillEra = blocknumberExt % 1200;
            var eraPerc = (double)blocksTillEra / 1200;

            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                NodeInfo.text = _accountController.WebSocketUrl;
                ChainName.text = chainInfo.Chain;
                ChainVersion.text = "SPEC." + chainInfo.RuntimeVersion.SpecVersion.ToString("000");
                SubstrateVersion.text = $"{chainInfo.Name} { chainInfo.Version}";
                BlockNumber.text = blockNumber.ToString("#,#", CultureInfo.InvariantCulture);
                BlockTrackerBar.Fill(barFill, 0);
                EpochValue.text = epochPerc.ToString("P", CultureInfo.InvariantCulture);
                EraValue.text = eraPerc.ToString("P", CultureInfo.InvariantCulture);
            });
        }
        else
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                NodeInfo.text = "";
                ChainName.text = "";
                ChainVersion.text = "";
                SubstrateVersion.text = "";
                BlockNumber.text = "";
                BlockTrackerBar.Fill(0);
                EpochValue.text = "";
                EraValue.text = "";
            });
        }
    }
}
