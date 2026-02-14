using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChamaMosaico : MonoBehaviour
{
    [SerializeField] private AudioSource BGM;
    [SerializeField] private AudioSource TransitionSound;
    bool primeira;

    float t;

    void Start()
    {
        primeira = true;
        t = 0;
    }

    void Update()
    {
        Debug.Log(t);

        t += Time.deltaTime;

        if(t > BGM.clip.length && primeira)
        {
            primeira = false;
            StartCoroutine(SceneTransition());
        }
    }

    public void Skip()
    {
        BGM.Stop();
        StartCoroutine(SceneTransition());
    }


    private IEnumerator SceneTransition()
    {
        TransitionSound.Play();

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("MosaicoAudio");
    }
}
