using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EntityAI : MonoBehaviour
{
    //SetupAI
    [SerializeField] private GameObject entity;
    private NavMeshAgent entitymeshAgent;
    private Animator entityAnimator;
    [SerializeField] private float entityDistance = 30f; //Distance to trigger the moviment
    public GameObject player;
    private FirstPersonController firstPerson;
    
    //AISettings
    private float walkSpeed = 1.5f;
    private float runSpeed = 4f;

    //RandomWalk
    [SerializeField] private float range; //até onde pode andar
    [SerializeField] private Transform centrePoint; //centro da area(usa se o proprio transform)

    //FieldOfView
    public float radius;
    [SerializeField][Range(0,360)]
    public float angle;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstructMask;
    
    //Bools
    public bool canSeePlayer;
    private bool huntPlayer = false;
    public bool isRuning = false; //Is moving? (Entity)
    private bool isKilledPlayer = false;

    //Sound
    [SerializeField] private AudioSource bacteriaAudioSource;
    [SerializeField] private AudioClip bacteriaChaseAudio;

    [SerializeField] private AudioSource bacteriaAudioSource2;
    [SerializeField] private AudioClip bacteriaWalkAudio;

    [SerializeField] private AudioSource bacteriaAudioSource3;
    [SerializeField] private AudioClip bacteriaStepsAudio;

    private EntitySpawn entitySpawn;

    private DeathSystem death;

    //VideoJumpscare
    private bool isVideoPlayed = false;
    [SerializeField] private GameObject jumpscareVideo;
    [SerializeField] private RawImage jumpscareVideoImage;
    [SerializeField] private GameObject overlay;


    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        death = player.GetComponent<DeathSystem>();
        entitymeshAgent = entity.GetComponent<NavMeshAgent>();
        entityAnimator = entity.GetComponent<Animator>();
        firstPerson = player.GetComponent<FirstPersonController>();
        entitySpawn = GameObject.Find("BacteriaSpawner").GetComponent<EntitySpawn>();
        overlay = GameObject.Find("Overlay");
        jumpscareVideo = GameObject.FindGameObjectWithTag("JumpscareVideo");
        jumpscareVideoImage = jumpscareVideo.GetComponent<RawImage>();

        jumpscareVideoImage.enabled = false;

        bacteriaAudioSource.clip = bacteriaChaseAudio;
        bacteriaAudioSource2.clip = bacteriaWalkAudio;
        bacteriaAudioSource3.clip = bacteriaStepsAudio;

        bacteriaAudioSource3.Play();

        StartCoroutine(FOVRoutine());
    }

    public void Update()
    {
        Invoke("DetectPlayer", 2f); //A small delay to give the player some time to react

        if (!huntPlayer && !canSeePlayer && !isKilledPlayer)
        {
            entityAnimator.Play("Walk");
            EntityRandomWalk();
            if (bacteriaAudioSource2.isPlaying == false)
                bacteriaAudioSource2.Play();
        }
        else if(!isKilledPlayer && (huntPlayer || canSeePlayer))
        {
            entityAnimator.Play("Run");
            bacteriaAudioSource3.pitch = 2f;

            bacteriaAudioSource2.Stop();
            EntityAtack();
            if(bacteriaAudioSource.isPlaying == false)
                bacteriaAudioSource.Play();
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > entityDistance) //if we reach the distance greater that entityDistance than the bacteria will despawn
            entitySpawn.DespawnEntity();
    }

    public void EntityAtack()
    {
        entitymeshAgent.speed = runSpeed;

        float distance = Vector3.Distance(transform.position,  player.transform.position);

        if (distance < entityDistance)
        {         
            entitymeshAgent.SetDestination(player.transform.position);

            isRuning = true;
        }

        if (distance > entityDistance)
        {
            isRuning = false;
        }
    }

    private void EntityRandomWalk()
    {
        entitymeshAgent.speed = walkSpeed;

        if (entitymeshAgent.remainingDistance <= entitymeshAgent.stoppingDistance) 
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) 
            {                       
                entitymeshAgent.SetDestination(point);
            }
        }
    }

    //Calculates a random point
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform; // 0 because there is only one player
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2) 
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    private void DetectPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < entityDistance && !canSeePlayer && !huntPlayer && !firstPerson.isCrouching && firstPerson.isMoving)
        {
            huntPlayer = true;
        }
    }

    //Killing Player
    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            isKilledPlayer = true;          
            bacteriaAudioSource.Stop();
            bacteriaAudioSource2.Stop();
            bacteriaAudioSource3.Stop();

            var videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
            if (isVideoPlayed == false)
            {
                overlay.SetActive(false);
                videoPlayer.Play();
                jumpscareVideoImage.enabled = true;
                isVideoPlayed = true;
            }
            Invoke("Death", 5f);
        }
    }

    private void Death()
    {
        death.Death();     
    }
}







