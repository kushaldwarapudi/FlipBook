using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TestFpsCapture : MonoBehaviour
{
    public WebCamTexture webcam;
    public RawImage image;
    int fps = 24;
    float duration = 7f;
    [SerializeField]int Total;
    [SerializeField] int ImageNumber = 0;
    [SerializeField] bool Takeimage;
    // Start is called before the first frame update
    void Start()
    {
        webcam = new WebCamTexture();
        image.texture = webcam;
        image.material.mainTexture = webcam;
        webcam.Play();
        Time.captureFramerate = fps;
        Total = fps * (int)duration;
        Takeimage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Takeimage = true;
        }
        if (Takeimage)
        {
            StartCoroutine(TakePhotos());
        }
    }
    public void TakePicture()
    {
        while (ImageNumber < Total)
        {
            Texture2D screenShot = new Texture2D(image.texture.width, image.texture.height, TextureFormat.RGB24, false);
            screenShot.SetPixels(webcam.GetPixels());
            screenShot.Apply();
            byte[] bytes = screenShot.EncodeToPNG();
            int index = ImageNumber + 1;
            ImageNumber++;

            string fileName = Application.persistentDataPath + "/Screenshot" + index + ".png";
           
            Debug.Log(fileName);
           
            
        }
        
    }
    public IEnumerator TakePhotos()
    {
        for(int i = 0; i < 400; i = i+40)
        {
            if (i == 400)
            {
                Takeimage = false;
            }
            yield return new WaitForEndOfFrame();
            Texture2D screenShot = new Texture2D(image.mainTexture.width, image.mainTexture.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(new Rect(0, 0, image.mainTexture.width, Screen.height), 0, 0);
            screenShot.Apply();
            byte[] bytes = screenShot.EncodeToPNG();
            int index = ImageNumber + 1;
            ImageNumber++;

            string fileName = Application.persistentDataPath + "/Screenshot" + index + ".png";
            System.IO.File.WriteAllBytes(fileName, bytes);
            Debug.Log(fileName);
            Destroy(screenShot);
            
        }
        
    }
}
