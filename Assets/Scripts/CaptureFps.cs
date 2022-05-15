using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CaptureFps : MonoBehaviour
{
    public WebCamTexture webcam;
    public RawImage image;
    public Text ImageCount;
    public int fps = 24;
    public int TotalFrames;
    public float duration = 7f;
    public GameObject CloseText;
    [SerializeField]private float videoTime = 0;
    private bool takeHiResShot = false;
    [SerializeField]int ImageNumber=0;

    private void Start()
    {
        ImageCount.text ="Img Count : " + ImageNumber.ToString();
        CloseText.SetActive(false);
        webcam = new WebCamTexture();
        image.texture = webcam;
       // image.material.mainTexture = webcam;
        webcam.Play();
        Time.captureDeltaTime = 1.0f / fps;
        TotalFrames = fps * (int)duration;
        Debug.Log("ImageWidth" + Screen.width + "X" + "ImageHeight" + Screen.height);
    }

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TakeHiResShot()
    {
        //takeHiResShot = true;
        //videoTime += Time.deltaTime;
        if (takeHiResShot)
        {
            duration -= Time.deltaTime;
            UpdateTimer(duration);
        }
        if (videoTime <= (1f / (float)fps))
        {
           // Debug.LogFormat(videoTime.ToString());
            //videoTime = 0;
            //takeHiResShot = true;

            
        }
        if (takeHiResShot && ImageNumber < 40)
        {
            
            Texture2D screenShot = new Texture2D(image.texture.width, image.texture.height,TextureFormat.RGB24,false);
            screenShot.SetPixels(webcam.GetPixels());
            screenShot.Apply();
            byte[] bytes = screenShot.EncodeToPNG();
            int index = ImageNumber + 1;
            ImageNumber++;

            string fileName = Application.dataPath + "/Screenshots/";
            string imagename= "ScreenShot" + index + ".png";
            string Path = fileName + imagename;
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
            //Debug.Log(Path);
            
            System.IO.File.WriteAllBytes(Path, bytes);
            // takeHiResShot = false;
        }
    }
    void UpdateTimer(float TimetoDisplay)
    {
        TimetoDisplay += 1;
        float secs = Mathf.FloorToInt(TimetoDisplay % 60);
    }
    void Update()
    {
        ImageCount.text = "Img Count : " + ImageNumber.ToString();
        if (ImageNumber>=40)
        {
            takeHiResShot = false;
            CloseText.SetActive(true);
           // duration = 0f;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            takeHiResShot = true;
            
        }
        TakeHiResShot();
        #region OriginalCode
        //videoTime += Time.deltaTime;

        //if (videoTime >= (1f / (float)fps))
        //{
        //    Debug.LogFormat(videoTime.ToString());
        //    videoTime = 0;
        //    takeHiResShot = true;

        //    if (takeHiResShot && ImageNumber < 60)
        //    {
        //        Texture2D screenShot = new Texture2D(image.texture.width, image.texture.height, TextureFormat.RGB24, false);
        //        screenShot.SetPixels(webcam.GetPixels());
        //        screenShot.Apply();
        //        byte[] bytes = screenShot.EncodeToPNG();
        //        int index = ImageNumber + 1;
        //        ImageNumber++;
                
        //        string fileName = Application.persistentDataPath + "/Screenshot" + index + ".png";
        //        Debug.Log(fileName);
        //        System.IO.File.WriteAllBytes(fileName, bytes);
        //       // takeHiResShot = false;
        //    }
        //}
        #endregion
    }
}
