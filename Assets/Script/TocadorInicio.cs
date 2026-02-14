using System.Collections;
using UnityEngine;

public class TocadorInicio : MonoBehaviour
{
    public static TocadorInicio instance;

    void Awake()
    {
        instance = this;
    }

    public void PlayAudio(AudioClip audioClip, float volume = 0.5f)
    {
        StartCoroutine(PlayAudioCoroutine(audioClip, volume));
    }

    IEnumerator PlayAudioCoroutine(AudioClip audioClip, float volume = 0.5f)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        yield return new WaitForSeconds(audioSource.clip.length);
    }
}
