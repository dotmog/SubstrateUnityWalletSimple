using System;
using UnityEngine;
using UnityEngine.UI;

public class TrackerBar : MonoBehaviour
{
    [SerializeField]

    private Color _failed = new Color(174 / 255f, 23 / 255f, 23 / 255f, 1f);

    private Color _warned = new Color(174 / 255f, 174 / 255f, 23 / 255f, 1f);

    private Color _filled = new Color(59 / 255f, 174 / 255f, 121 / 255f, 1f);

    private Color _empty = new Color(23 / 255f, 50 / 255f, 69 / 255f, 1f);

    public GameObject[] Bars;

    // Start is called before the first frame update
    void Start()
    {
        //_bar = Resources.Load("Images/bar") as Texture2D;
        Fill(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill(float value, int colorFlag = 0)
    {
        if (value > 1f)
        {
            value = 1f;
        } 
        else if (value < 0f)
        { 
            value = 0f;
        }

        var filled = Math.Round(value * Bars.Length, 0, MidpointRounding.AwayFromZero);

        for(int i = 0; i < Bars.Length; i++)
        {
            var image = Bars[i].GetComponent<Image>();
            if (i < filled)
            {
                image.color = colorFlag == 0 ? _filled : colorFlag == 1 ?_warned : _failed ;
            } 
            else
            {
                image.color = _empty;
            }
        }


    }
}
