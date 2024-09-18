using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DetectionNamespace;

[RequireComponent(typeof(Collider))]
public class VisualSignal : AbstractSignal
{
    private void Start()
    {
        detectionType = DetectionType.Visual;
        controller = DetectionController.Instance;
        controller.RegisterSignal(this);
    }
}
