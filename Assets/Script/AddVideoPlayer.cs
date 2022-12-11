using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVideoPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected UnityEngine.Video.VideoPlayer videoClipObject;
    [SerializeField] protected string videoName;

    void Start()
    {
        videoClipObject.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
