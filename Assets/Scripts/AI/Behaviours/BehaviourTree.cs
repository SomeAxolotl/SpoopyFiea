using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BehaviourTree : MonoBehaviour
{
    // Start is called before the first frame update

    public enum BehaviourTreeState
    {
        Patrol,
        Investigate,
        Attack,
        None
    }

    private BehaviourTreeState currentBehaviour = BehaviourTreeState.None;
    private NavMeshAgent agent;
    private int currentPatrolPointIndex = 0;
    private bool isWaiting = false;
    private bool _canListen = false;
    private SuspicionStateSystem suspicionState;
    private Transform player;
    private LastKnownPosition lastKnownPosition;
    private bool _invokedGameOver = false;
    private Transform[] _patrolPoints;

    public Transform patrolPointObject;
    public float waitTime = 2f;
    public float chaseSpeed = 2.5f;
    public float patrolSpeed = 1f;
    public float investigateSpeed = 1.5f;
    public float changePointRange = 1f;
    public List<AudioClip> audioClips;
    public AudioClip currentClip;
    public AudioSource source;
    [SerializeField] private float _listenRange = 5f;
    [SerializeField] private float _soundSusRate = 0.4f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        suspicionState = SuspicionStateSystem.Instance;
        player = GameObject.FindWithTag("Player").transform;
        lastKnownPosition = GetComponent<LastKnownPosition>();
        _patrolPoints = new Transform[patrolPointObject.childCount];

        for(int i = 0; i < patrolPointObject.childCount; i++)
        {
            _patrolPoints[i] = patrolPointObject.GetChild(i).transform;
        }
        source = GetComponent<AudioSource>();
    }

    void KillPlayer()
    {
        if (!_invokedGameOver)
            UIManager.gameOver?.Invoke();
        _invokedGameOver = true;
        Destroy (gameObject);
    }

    private void OnEnable()
    {
        if (suspicionState == null)
            Start();
        suspicionState.ResetSuspicionLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (suspicionState.GetSuspicionLevel() > 50)
        {
            if (currentBehaviour != BehaviourTreeState.Attack)
            {
                /*agent.isStopped = true;*/
                currentBehaviour = BehaviourTreeState.Attack;
                Cry();
            }
            Debug.Log("In Attack Behaviour sus :" + suspicionState.GetSuspicionLevel());
            MoveToPoint(player.position, chaseSpeed);
            if (Vector3.Distance(transform.position, player.position) < 1f)
            {
                Debug.Log("KILL PLAYER");
                KillPlayer();
                agent.isStopped = true;
                Cry();
            }
        }
        else if (suspicionState.GetSuspicionLevel() > 20)
        {
            if (currentBehaviour != BehaviourTreeState.Investigate)
            {
                currentBehaviour = BehaviourTreeState.Investigate;
                Cry();

            }
            Debug.Log("In Investigate Behaviour  sus :" + suspicionState.GetSuspicionLevel());
            MoveToPoint(lastKnownPosition._position, investigateSpeed);
            Debug.Log("The AI position in investigate is : " + transform.position);
            Debug.Log("Last known Position in Investigate is : " + lastKnownPosition._position);
        }
        else
        {
            if (currentBehaviour != BehaviourTreeState.Patrol)
            {
                currentBehaviour = BehaviourTreeState.Patrol;
            }
            Debug.Log("Not sus enough " + suspicionState.GetSuspicionLevel());
            //Patrol
            MoveToPoint(_patrolPoints[currentPatrolPointIndex].position, patrolSpeed);

        }

    }
    private void MoveToPoint(Vector3 position, float speed)
    {
        if (agent.SetDestination(position))
        {
            //Debug.Log($"The position {position} is valid to move for agent");
            //Debug.Log("The speed of the AI is " + speed);
            agent.speed = speed;
        }

        if(currentBehaviour == BehaviourTreeState.Patrol)
        {
            float waypointDistance = Vector3.Distance(transform.position, position);
            if (waypointDistance < changePointRange)
            {
                currentPatrolPointIndex = (currentPatrolPointIndex + 1) % _patrolPoints.Length;
            }
            else
            {
                Debug.Log("Waypoint distance is " + waypointDistance);
            }
        }


    }
    void Cry()
    {
        currentClip = audioClips[Random.Range(0, audioClips.Count)];
        source.clip = currentClip;
        source.Play();
    }
}
