using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class menuScript : MonoBehaviour
{
    public Button alivePhoto;
    public Button instruction;
    public Button info;
    public GameObject midPict;
    public GameObject instructionPanel;
    public GameObject infoText;
    public GameObject scanPanel;

    void AlivePhoto()
    {
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
        scanPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    void ShowInstruction()
    {
        instructionPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    void ShowHideInfo()
    {
        midPict.SetActive(!midPict.activeSelf);
        instruction.gameObject.SetActive(!instruction.gameObject.activeSelf);
        alivePhoto.gameObject.SetActive(!alivePhoto.gameObject.activeSelf);
        infoText.SetActive(!infoText.activeSelf);
    }

    private void Start()
    {
        info.onClick.AddListener(ShowHideInfo);
        alivePhoto.onClick.AddListener(AlivePhoto);
        instruction.onClick.AddListener(ShowInstruction);
    }

}
