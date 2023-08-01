using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EndVideoAction : MonoBehaviour
{
    VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += EndReached;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        //vp.playbackSpeed = vp.playbackSpeed / 10.0F;
        Debug.Log("Video finished");

        UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
    }

}
