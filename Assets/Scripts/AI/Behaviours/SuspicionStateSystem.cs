using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionStateSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public float coolDownDuration = 3f;
    public float suspicionDegradeFactor = 2f;

    private static SuspicionStateSystem _instance;
    public static SuspicionStateSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SuspicionStateSystem>();

                if (_instance == null)
                {
                    GameObject gameManager = new GameObject(typeof(SuspicionStateSystem).Name);
                    _instance = gameManager.AddComponent<SuspicionStateSystem>();
                    DontDestroyOnLoad(gameManager);
                }
            }
            return _instance;
        }
    }

    private float suspicionLevel;
    private Coroutine myCoroutine;
    public float GetSuspicionLevel()
    {
        return suspicionLevel;
    }

    public void ResetSuspicionLevel()
    {
        suspicionLevel = 0;
    }
    void UpdateSuspicionLevel(AbstractStimulus stim)
    {
        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
        }
        if (suspicionLevel < stim.maxLimit)
        {
            suspicionLevel += stim.value * Time.deltaTime;
        }
        myCoroutine = StartCoroutine(WaitAndDecreaseSuspicion());
    }
    private IEnumerator WaitAndDecreaseSuspicion()
    {
        yield return new WaitForSeconds(coolDownDuration);
        while (suspicionLevel > 0)
        {
            suspicionLevel -= suspicionDegradeFactor * Time.deltaTime;
            yield return null;
        }
    }
    void Start()
    {
        DetectionController.ProcessStimulus += UpdateSuspicionLevel;
    }

    // Update is called once per frame
    void Update()
    {
        if (suspicionLevel > 20f && suspicionLevel < 50)
        {
            //INVESTIGATE
        }
        else if (suspicionLevel > 50)
        {
            //Attack
        }
        else if (suspicionLevel < 50)
        {
            //Patrol
        }
    }
}
