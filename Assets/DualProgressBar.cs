using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DualProgressBar : MonoBehaviour
{
    [SerializeField] float hairpercent;
    [SerializeField] float timepercent;
    float differencepercent;
    [SerializeField] GameObject hairbar;
    [SerializeField] GameObject timebar;
    [SerializeField] GameObject timedummybar;
    [SerializeField] GameObject timemarker;
    [SerializeField] GameObject hairmarker;

    void Update()
    {
        //set appropriate filla amounts for each progress bar.
        //Time percent is % of maximum time.
        hairbar.GetComponent<Image>().fillAmount = hairpercent;
        timebar.GetComponent<Image>().fillAmount = timepercent;
        timedummybar.GetComponent<Image>().fillAmount = timepercent;
        differencepercent = Mathf.Abs(hairpercent - timepercent);

        //set appropriate colors so difference is visible
        if (hairpercent > timepercent)
        {
            timedummybar.SetActive(true);
            timebar.SetActive(false);
            hairbar.GetComponent<Image>().color = Color.green;
            timedummybar.GetComponent<Image>().color = Color.yellow;
        }
        else if (timepercent > hairpercent)
        {
            timedummybar.SetActive(false);
            timebar.SetActive(true);
            hairbar.GetComponent<Image>().color = Color.yellow;
            timebar.GetComponent<Image>().color = Color.red;
        }

        //set markers
        float maxWidth = this.GetComponent<RectTransform>().sizeDelta.x;
        timemarker.transform.localPosition = new Vector3(((maxWidth * timepercent)-(maxWidth / 2)), 0, 0);
        hairmarker.transform.localPosition = new Vector3(((maxWidth * hairpercent) - (maxWidth / 2)), 0, 0);
    }
}
