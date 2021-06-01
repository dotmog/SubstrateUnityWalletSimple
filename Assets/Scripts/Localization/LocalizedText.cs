using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{

    public string key;

    private Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        text.text = LocalizationController.Instance.GetLocalizedValue(key);
    }

    public void ChangeKey(string key)
    {
        this.key = key;
        
        Reload();
    }

    public void Reload()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
        text.text = LocalizationController.Instance.GetLocalizedValue(key);
    }

}