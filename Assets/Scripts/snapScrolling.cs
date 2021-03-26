using UnityEngine;
using UnityEngine.UI;

public class snapScrolling : MonoBehaviour
{
    [Header("Controllers")]
    [Range(0, 500)]
    public int whiteSpace;
    [Range(0f, 10f)]
    public float            snapSpeed;
    [Range(0f, 10f)]
    public float            scaleOffset;
    [Range(0f, 10f)]
    public float scaleSpeed;

    [Header("Objects")]
    public ScrollRect scrollRect;
    public GameObject[]     pictPrefs;

    private int panCount;
    private GameObject[]    instantiatedPrefs;
    private Vector2[]       prefsPos;
    private Vector2[]       prefsScale;
    private RectTransform   contentRect;
    private int             selectedID;
    private bool            isScrolling;
    private Vector2         contentVector;


    private void Start()
    {
        Input.multiTouchEnabled = false;
        panCount = pictPrefs.Length;
        contentRect = GetComponent<RectTransform>();
        instantiatedPrefs = new GameObject[panCount];
        prefsPos = new Vector2[panCount];
        prefsScale = new Vector2[panCount];
        for (int i = 0; i < panCount; i++)
        {
            instantiatedPrefs[i] = pictPrefs[i];
            if (i == 0) continue;
            instantiatedPrefs[i].transform.localScale = new Vector3(0, 0, 0);
            float newPosX = instantiatedPrefs[i - 1].transform.localPosition.x + pictPrefs[i].GetComponent<RectTransform>().sizeDelta.x + whiteSpace;
            float newPosY = instantiatedPrefs[i].transform.localPosition.y;
            instantiatedPrefs[i].transform.localPosition = new Vector2(newPosX, newPosY);
            prefsPos[i] = -instantiatedPrefs[i].transform.localPosition;
        }
    }

    private void FixedUpdate()
    {
        float nearestPos = float.MaxValue;
        if ((contentRect.anchoredPosition.x >= prefsPos[0].x && !isScrolling) 
            || (contentRect.anchoredPosition.x <= prefsPos[prefsPos.Length - 1].x && !isScrolling))
            scrollRect.inertia = false;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - prefsPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedID = i;
            }
            float scale = Mathf.Clamp(1 / (distance / whiteSpace) * scaleOffset, 0.5f, 1f); //ограничение (? , min, max)
            prefsScale[i].x = Mathf.SmoothStep(instantiatedPrefs[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            prefsScale[i].y = Mathf.SmoothStep(instantiatedPrefs[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            
            instantiatedPrefs[i].transform.localScale = prefsScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 500 && !isScrolling)
             scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 500) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, prefsPos[selectedID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }


    public void scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll)
            scrollRect.inertia = true;
    }
}
