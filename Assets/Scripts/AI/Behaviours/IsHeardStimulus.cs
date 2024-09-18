using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHeardStimulus : AbstractStimulus
{
    // Start is called before the first frame update

    public IsHeardStimulus(Vector3 lkp) : base()
    {
        value = 10;
        maxLimit = 40;
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
