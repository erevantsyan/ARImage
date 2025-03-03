using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> prefabs = new List<GameObject>();

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private ARTrackedImageManager trackedManager;

    void Awake()
    {
        trackedManager = GetComponent<ARTrackedImageManager>();

        foreach(GameObject prefab in prefabs)
        {
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            newPrefab.name = prefab.name;
            newPrefab.SetActive(false);
            spawnedPrefabs.Add(newPrefab.name, newPrefab);
        }
    }

    void OnEnable()
    {
        trackedManager.trackedImagesChanged += OnImagesTrackedChanged;
    }

    void OnDisable()
    {
        trackedManager.trackedImagesChanged -= OnImagesTrackedChanged;
    }

    void OnImagesTrackedChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        /*foreach (var trackedImage in eventArgs.added)
        {
            UpdateTrackedImages(trackedImage);
        }*/
        foreach (var trackedImage in eventArgs.updated)
        {
            UpdateTrackedImages(trackedImage);
        }
        foreach (var trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.referenceImage.name].gameObject.SetActive(false);
        }
    }

    /*void AddTrackedImages(ARTrackedImage trackedImage)
    {
        if (trackedImage == null) return;
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            spawnedPrefabs[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }

        spawnedPrefabs[trackedImage.referenceImage.name].gameObject.SetActive(true);
        spawnedPrefabs[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        spawnedPrefabs[trackedImage.referenceImage.name].transform.rotation = trackedImage.transform.rotation;

        EventBus.Raise(new eCharacterHello());
    }*/

    void UpdateTrackedImages(ARTrackedImage trackedImage)
    {
        if (trackedImage == null) return;
        if (trackedImage.trackingState is TrackingState.Limited or TrackingState.None)
        {
            spawnedPrefabs[trackedImage.referenceImage.name].gameObject.SetActive(false);
            return;
        }

        spawnedPrefabs[trackedImage.referenceImage.name].gameObject.SetActive(true);
        spawnedPrefabs[trackedImage.referenceImage.name].transform.position = trackedImage.transform.position;
        spawnedPrefabs[trackedImage.referenceImage.name].transform.rotation = trackedImage.transform.rotation;
    }
}
