using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class VideoOnDetect : MonoBehaviour
{
    private ObserverBehaviour observer;
    private GameObject videoPlane;
    private VideoPlayer videoPlayer;

    void Start()
    {
        observer = GetComponent<ObserverBehaviour>();
        videoPlane = GetComponentInChildren<VideoPlayer>(true).gameObject;
        videoPlayer = videoPlane.GetComponent<VideoPlayer>();

        videoPlane.SetActive(false);   // hide at start
        videoPlayer.Stop();

        observer.OnTargetStatusChanged += OnStatusChanged;
    }

    void OnStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            videoPlane.SetActive(true);
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
            videoPlane.SetActive(false);  // HIDE when target lost
        }
    }
}
