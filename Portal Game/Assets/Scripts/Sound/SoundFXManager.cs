using UnityEngine;

public class SoundFXManager : MonoBehaviour
{

    [SerializeField] private AudioSource soundFXObject;

    public static SoundFXManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLegth = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLegth);
    }
}