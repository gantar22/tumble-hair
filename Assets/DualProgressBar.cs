using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DualProgressBar : MonoBehaviour
{
    [SerializeField] float hairpercent;
    [SerializeField] float timepercent;
    [SerializeField] float differencepercent;
    [SerializeField] GameObject hairbar;
    [SerializeField] GameObject timebar;
    [SerializeField] GameObject timedummybar;

    void Update()
    {
        //set appropriate filla amounts for each progress bar
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
    }
}
