using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class loadScreen : MonoBehaviour
{
    public GameObject logo;
    Animation logoAnim;

    private void Start()
    {
        logoAnim = logo.GetComponent<Animation>();
    }
    IEnumerator startSlideAnim()
    {
        logoAnim.Play();
        while (logoAnim.isPlaying)
            yield return null;
        SceneManager.LoadScene(1);
    }
}


