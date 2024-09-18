using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSeenStimulus : AbstractStimulus
{
    // Start is called before the first frame update
    public IsSeenStimulus(Vector3 lkp) : base()
    {
        value = 10;
        maxLimit = 100;
        position = lkp;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
