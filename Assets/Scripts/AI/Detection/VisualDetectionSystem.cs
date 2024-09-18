using DetectionNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VisualDetectionSystem : AbstractDetection
{
    public float innerRadius = 20f;
    public float outerRadius = 35f;
    public float perceptionAngleInDegree= 90;
    // Update is called once per frame

    public static Action<IsSeenStimulus> IsSeen;
    public VisualDetectionSystem() 
    {
        detectionType = DetectionNamespace.DetectionType.Visual;
        controller.AddDetectorToDetectionSystems += AddDetector;
        controller.AddSignalToDetectionSystems += AddSignal;
    }

    public override void AddDetector(AbstractDetector detector)
    {
        if (detector.detectionType == DetectionType.Visual)
        {
            if (detectionType == DetectionType.Visual)
            {
                detectors.Add(detector);
            }
        }
    }
    public override void AddSignal(AbstractSignal signal) 
    {
        if (signal.detectionType == DetectionType.Visual)
        {
            if (detectionType == DetectionType.Visual)
            {
                signals.Add(signal);
            }
        }
    }
    public override void Detect() 
    {
        for (int i = 0; i< detectors.Count; i++)
        {
            for (int j = 0; j < signals.Count; j++)
            {
                VisualDetector d = detectors[i] as VisualDetector;
                if (d != null)
                {
                    VisualSignal s = signals[i] as VisualSignal;
                    if (s != null)
                    {
                        //distance check
                        Transform detectorTransform = d.transform;
                        Transform signalTransform = s.transform;

                        //Assuming transforms can not be null. To-do : Enforce transform on the game objects
                        //Can they be? how do we handle that

                        float distance = Vector3.Distance(detectorTransform.position, signalTransform.position);
                        Debug.Log("Distance between transforms : " + distance);

                        if (distance <= innerRadius)
                        { 
                            //Second Check : Is the signal entity in the field of view of the detector entity
                            Vector3 directionDetectorToSignal = signalTransform.position - detectorTransform.position;
                            float angle = Vector3.Angle(detectorTransform.forward, directionDetectorToSignal);
                            if (angle < perceptionAngleInDegree)
                            {
                                Debug.Log("Angle between detector and Signal is : " + angle);
                                //Third check : Line of sight
                                RaycastHit hit;
                                Debug.DrawRay(detectorTransform.position, directionDetectorToSignal.normalized * innerRadius, Color.red);
                                if (Physics.Raycast(detectorTransform.position , directionDetectorToSignal.normalized, out hit, innerRadius))
                                {
                                    if (hit.collider.gameObject == s.gameObject)
                                    {
                                        //succesfull detection
                                        Debug.Log("Successful Sight detection ");
                                        //Send stimulus event to which the alertstatesystem has subscribed
                                        IsSeenStimulus stim = new IsSeenStimulus(s.transform.position);
                                        DetectionController.ProcessStimulus(stim);
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

    }
}
