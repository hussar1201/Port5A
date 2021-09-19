using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class TrackImage : MonoBehaviour
{
    private float time_limit = 1f;
    private AudioSource audiosource;
    public ARTrackedImageManager manager;
    public List<GameObject> objList = new List<GameObject>();  
    private GameObject obj_current_view;
    private ARTrackedImage img_current;        
    Dictionary<string, GameObject> prefDic = new Dictionary<string, GameObject>();
    public List<AudioClip> list_music = new List<AudioClip>();
    private List<ARTrackedImage> list_trackedImg = new List<ARTrackedImage>();
    private List<float> list_timer = new List<float>();

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        foreach (GameObject obj in objList)
        {
            string name = obj.name;
            Debug.Log(name);
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

    private void Update()
    {
        if (obj_current_view != null)
        {
            obj_current_view.transform.position = img_current.transform.position;
            
        }
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage img in args.added)
        {
            if (!list_trackedImg.Contains(img))
            {
                list_trackedImg.Add(img);
                list_timer.Add(0);
            }
            UpdateImage(img);

        }
    }

    private void ClearImage()
    {
        if (obj_current_view != null)
        {
            obj_current_view.SetActive(false);
            audiosource.Stop();
        }
    }

    private void UpdateImage(ARTrackedImage img)
    {
        img_current = img;
        string name = img.referenceImage.name;
        obj_current_view = prefDic[name];    
        obj_current_view.transform.position = img.transform.position + new Vector3(0f, 0f, 0.05f);        

        for (int i = 0;i<objList.Count;i++)
        {
            objList[i].SetActive(false);
        }

        if (audiosource.isPlaying) audiosource.Stop();

        for (int i = 0; i < list_music.Count; i++)
        {
            if (list_music[i].name.Contains(name))
            {
                audiosource.clip = list_music[i];
                audiosource.Play();
                break;
            }
        }
        obj_current_view.SetActive(true);
    }

}
