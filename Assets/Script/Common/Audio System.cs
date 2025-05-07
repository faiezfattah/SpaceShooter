using UnityEngine;
using UnityEngine.Pool;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem Instance; 

    [Header("SFX Clips")]
    public AudioClip shootClip;
    public AudioClip hitClip;
    public AudioClip deathClip;

    private ObjectPool<AudioSource> _sfxPool;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        _sfxPool = new ObjectPool<AudioSource>(
            CreateAudioSource,
            OnGet,
            OnRelease,
            OnDestroyAudioSource,
            true, 10, 50
        );
    }

    public void PlaySfx(AudioClip clip)
    {
        AudioSource source = _sfxPool.Get();
        source.clip = clip;
        source.Play();

        StartCoroutine(ReturnToPool(source, clip.length));
    }

    private System.Collections.IEnumerator ReturnToPool(AudioSource source, float delay)
    {
        yield return new WaitForSeconds(delay);
        _sfxPool.Release(source);
    }

    private AudioSource CreateAudioSource()
    {
        GameObject go = new GameObject("PooledSFX");
        go.transform.SetParent(transform);
        AudioSource source = go.AddComponent<AudioSource>();
        return source;
    }

    private void OnGet(AudioSource source)
    {
        source.gameObject.SetActive(true);
    }

    private void OnRelease(AudioSource source)
    {
        source.Stop();
        source.clip = null;
        source.gameObject.SetActive(false);
    }

    private void OnDestroyAudioSource(AudioSource source)
    {
        Destroy(source.gameObject);
    }
}
