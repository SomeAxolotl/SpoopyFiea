using DetectionNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    public static Action<AbstractStimulus> ProcessStimulus;
    // Start is called before the first frame update
    private static DetectionController _instance ;
    public static DetectionController Instance 
    {
        get 
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DetectionController>();

                if (_instance == null)
                {
                    GameObject gameManager = new GameObject(typeof(DetectionController).Name);
                    _instance = gameManager.AddComponent<DetectionController>();
                    DontDestroyOnLoad(gameManager);
                }
            }
            return _instance;
        }
    }

    [SerializeField]
    public List<AbstractDetection> detectionSystems;
    public Action RegisterDetectionSystems;
    public Action InvokeDetectionLogic;
    public Action<AbstractDetector> AddDetectorToDetectionSystems;
    public Action<AbstractSignal> AddSignalToDetectionSystems;

    public void RegisterDetector(AbstractDetector detector)
    {
        AddDetectorToDetectionSystems(detector);   
    }
    public void RegisterSignal(AbstractSignal signal)
    {
        AddSignalToDetectionSystems(signal);  
    }
    private void Awake()
    {
        VisualDetectionSystem visualDetection = new VisualDetectionSystem();
        SoundDetectionSystem soundDetection = new SoundDetectionSystem();
        detectionSystems = new List<AbstractDetection>();
        detectionSystems.Add(visualDetection);
        detectionSystems.Add(soundDetection);
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        foreach (var detectionSystem in detectionSystems)
        {
            detectionSystem.Detect();
        }
    }
}
