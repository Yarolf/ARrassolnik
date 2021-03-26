using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class TrackableEvent : DefaultTrackableEventHandler
{

    public VideoPlayer  video;
    public GameObject   videoPlane;
    public GameObject   dLBar;
    public GameObject   kolpak;
    public GameObject   topDl;
    public TextMesh     dLText;
    public GameObject   prepairing;
    public GameObject   connectionError;
    public string[]     dlList;
    private int         i = 0;
    private bool        isCached = false;


    private void MoveDLBar(int percent)
    {
        kolpak.transform.localPosition = new Vector3((percent * 0.08f), kolpak.transform.localPosition.y, kolpak.transform.localPosition.z);
        topDl.transform.localScale = new Vector3((percent * 0.1115f), topDl.transform.localScale.y, topDl.transform.localScale.z);
      //  dLText.text = "Загрузка " + percent.ToString() + "%";
    }

    void BeforeCaching()
    {
        videoPlane.GetComponent<MeshRenderer>().enabled = false;
        videoPlane.SetActive(true);
        video.SetDirectAudioMute(0, true);
        video.playbackSpeed = 2;
        //video.Play();
    }

    void AfterCaching()
    {
        dLBar.SetActive(false);
        videoPlane.GetComponent<MeshRenderer>().enabled = true;
        dLText.gameObject.SetActive(false);
        video.SetDirectAudioMute(0, false);
    }

    IEnumerator CacheVideo()
    {
        int percent = 0;
        BeforeCaching();
        while (percent != 100)
        {
            MoveDLBar(percent);
            if (percent == (100 / dlList.Length) * i)
            {
                dLText.text = dlList[i];
                i++;
            }
            percent = (int)(((float)video.frame / video.frameCount) * 100) + 2;
            yield return null;
        }
        isCached = true;
        video.playbackSpeed = 1;
        dLText.text = "Доставляем блюдо ...";
        prepairing.SetActive(true);
        while ((int)video.frame != 2)
        {
            yield return null;
        }
        prepairing.SetActive(false);
        AfterCaching();
        
    }



    IEnumerator PlayVideo()
    { 
        connectionError.SetActive(false);
        dLText.text = "Повар моет руки ...";
        dLBar.SetActive(true);
        video.Prepare();
        while (!video.isPrepared)
        {
            //dLText.text = "Загрузка 0%";
            dLText.text = "Мойте руки перед едой!";
            yield return null;
        }           
        StartCoroutine(CacheVideo());
    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        if (Application.internetReachability == NetworkReachability.NotReachable && !isCached)
        {
            connectionError.SetActive(true);
            //dLText.text = "Проверьте\n интернет\n соединение!";
            dLBar.SetActive(false);
        }
        else
        {
            if (!isCached)
                StartCoroutine(PlayVideo());
            video.Play();
        }
    }

    protected override void OnTrackingLost()
    {
        connectionError.SetActive(false);
        base.OnTrackingLost();
        if (isCached)
            video.frame = 0;
        video.Pause();
    }
}
