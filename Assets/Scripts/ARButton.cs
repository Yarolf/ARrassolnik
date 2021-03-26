using UnityEngine;

public class ARButton : MonoBehaviour
{
    public LayerMask layerMask;
    public Animation infoAnim;
    public GameObject ring;
    public GameObject nameObj;
    

    private bool isShow;

    private void Start()
    {
        nameObj.transform.localScale = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, 4, layerMask))
            {
                ring.SetActive(false);
                isShow = !isShow;
                if (gameObject.tag == "left")
                {
                    if (isShow)
                        infoAnim.Play("InfoOutLeft");
                    else
                        infoAnim.Play("InfoInLeft");
                }
                if (gameObject.tag == "right")
                {
                    if (isShow)
                        infoAnim.Play("InfoOutRight");
                    else
                        infoAnim.Play("InfoInRight");
                }
            }
        }
    }
}
