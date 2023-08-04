using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class EndVideoAction : MonoBehaviour
{

    public Button SkipVideoButton;

    VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += EndReached;
    }


    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        Debug.Log("Video finished");

        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
    }


    public void GoToFinalFrames()
    {
        // TODO stop music

        // Check if video has started (actually if it has loaded correctly)
        if (videoPlayer.frame > 0)
        {
            videoPlayer.frame = 800;
            SkipVideoButton.gameObject.SetActive(false);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
        }
        
    }



}
