using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CaptureMultiple : MonoBehaviour
{
    public WebCamTexture webcam;
    public RawImage image;
    public int ImageNumber=0;
    public int Timer;
    public bool TakeImages;

    // Start is called before the first frame update
    void Start()
    {
        webcam = new WebCamTexture();
        image.texture = webcam;
        image.material.mainTexture = webcam;
        webcam.Play();
        Timer = 0;
        TakeImages = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeImages = true;
        }

        if (Timer == 10)
        {
            TakeImages = false;
        }
        StartCoroutine(TakeScreenshot());
    }
    public void TakePhotos()
    {

            Texture2D screenShot = new Texture2D(image.texture.width, image.texture.height, TextureFormat.RGB24, false);
            screenShot.SetPixels(webcam.GetPixels());
            screenShot.Apply();
            byte[] bytes = screenShot.EncodeToPNG();
            int index = ImageNumber + 1;
            ImageNumber++;
            Timer++;
            string fileName = Application.persistentDataPath + "/Screenshot" + index + ".png";
            Debug.Log(fileName);
            System.IO.File.WriteAllBytes(fileName, bytes);
        
        
    }
    public IEnumerator TestCapture()
    {
        while (TakeImages && Timer < 10)
        {
            yield return new WaitForSeconds(0.5f);
            int index = ImageNumber + 1;
            ImageNumber++;
            Timer++;
            string fileName = Application.persistentDataPath + "/Screenshot" + index + ".png";
            Debug.Log(fileName);
            ScreenCapture.CaptureScreenshot(fileName);
        }
        
    }

    private IEnumerator TakeScreenshot()
    {
        
        while (TakeImages && Timer < 10 * Time.deltaTime)
        {
            yield return new WaitForSeconds(0.5f);
            TakePhotos();
        }
        
    }

}
