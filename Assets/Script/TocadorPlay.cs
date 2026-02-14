using UnityEngine;

public class TocadorPlay : MonoBehaviour
{
    AudioSource audioSource;

    public void PlayPlay()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
