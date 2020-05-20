using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage rawImage;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    void Start() => StartCoroutine(PlayVideo());

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(1);
            break;
        }

        RectTransform transform = rawImage.rectTransform;
        float aspectRatio = (float)videoPlayer.width / videoPlayer.height;
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, transform.rect.width / aspectRatio);

        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();
        audioSource.Play();
    }
}