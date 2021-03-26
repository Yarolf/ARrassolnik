using UnityEngine.UI;
using UnityEngine;

public class scanEventHandling : MonoBehaviour
{
    public Button retBtn;
    public GameObject menuPanel;
    public GameObject scanPanel;

    void ShowMenu()
    {
        menuPanel.SetActive(true);
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToLandscapeLeft = false;
        scanPanel.SetActive(false);
    }


    void Start()
    {
        retBtn.onClick.AddListener(ShowMenu);
    }

}
