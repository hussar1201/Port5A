using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;


public class TrackImage : MonoBehaviour
{

    public ARTrackedImageManager manager;
    public List<GameObject> objList = new List<GameObject>();

    Dictionary<string, GameObject> prefDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        foreach (GameObject obj in objList)
        {
            string name = obj.name;
            prefDic.Add(name, obj);
        }
    }

    private void OnEnable()
    {
        manager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        manager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach(ARTrackedImage img in args.added)
        {
            UpdateImage(img);
        }
        foreach (ARTrackedImage img in args.updated)
        {

        }      
    }

    private void UpdateImage(ARTrackedImage img)
    {
        string name = img.referenceImage.name;
        GameObject obj = prefDic[name];
        
        obj.transform.position = img.transform.position;
        obj.transform.rotation = img.transform.rotation;
        obj.SetActive(true);
    }

}
