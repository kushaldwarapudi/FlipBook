using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ExtractFrames : MonoBehaviour
{
    void Start()
    {
        var videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.Stop();
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.prepareCompleted += Prepared;
        videoPlayer.sendFrameReadyEvents = true;
        videoPlayer.frameReady += FrameReady;
        videoPlayer.Prepare();
    }

    void Prepared(VideoPlayer vp) => vp.Pause();

    void FrameReady(VideoPlayer vp, long frameIndex)
    {
        Debug.Log("FrameReady " + frameIndex);
        var textureToCopy = vp.texture;
        // Perform texture copy here ...
        vp.frame = frameIndex + 30;
    }
}
