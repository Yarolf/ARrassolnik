using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class instrButton : MonoBehaviour
{
    public GameObject[] pages;
    public Animation[] animations;
    public GameObject menu;
    public GameObject instruction;
    [Header("Button")] 
    public Button button;

    private Text buttonText;
    private int page = 1;
    
    void Start()
    {
        buttonText = button.GetComponentInChildren<Text>();
        button.onClick.AddListener(startCourotine);
    }

    void startCourotine()
    {
        StartCoroutine(NextPage());
    }

    IEnumerator NextPage()
    {
        if (page == 4)
        {
            pages[page - 1].SetActive(false);
            page = 1;
            buttonText.text = "Далее";
            pages[0].SetActive(true);
            menu.SetActive(true);
            instruction.SetActive(false);
        }
        else
        {
            animations[page - 1].Play("Instr" + page.ToString() + "PageExit");
            while (animations[page - 1].isPlaying)
                yield return null;
            if (page == 3)
                buttonText.text = "Меню";
            pages[page - 1].SetActive(false);
            page++;
            pages[page - 1].SetActive(true);
        }


    }

}
