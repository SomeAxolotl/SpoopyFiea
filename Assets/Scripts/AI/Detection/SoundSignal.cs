using DetectionNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class SoundSignal : AbstractSignal
{
    // Start is called before the first frame update
    void Start()
    {
        detectionType = DetectionType.Sound;
        controller = DetectionController.Instance;
        controller.RegisterSignal(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
