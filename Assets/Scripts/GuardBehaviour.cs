using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehaviour : MonoBehaviour
{
    [SerializeField] private Transform[] WayPoints;
    [SerializeField] private NavMeshAgent agent;

    private int currentWay;
    private float currCountdownValue;

    public Transform playerTransform;
    int layerMask = 1 << 8;
    enum StatusGuard { Walk, Guard, Follow };

    [SerializeField] StatusGuard currentStatus;

    void Start()
    {
        layerMask = ~layerMask;
        agent = GetComponent<NavMeshAgent>();
        currentWay = 0;
        SetStatusGuard(StatusGuard.Walk);
    }
    private void LateUpdate()
    {
        switch (GetStatusGuard())
        {
            case StatusGuard.Walk:
                if (Vector3.Distance(transform.position, WayPoints[currentWay].position) <= 0.5f)
                {
                    StartCoroutine(StartCountdown(true));
                    SetStatusGuard(StatusGuard.Guard);
                }

                agent.SetDestination(WayPoints[currentWay].position);
                break;
            case StatusGuard.Guard:
                //idle position
                break;
            case StatusGuard.Follow:
                transform.LookAt(new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,0.62f, GameObject.FindGameObjectWithTag("Player").transform.position.z));

                //RaycastHit hit;
                //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
                //{
                //    Debug.Log(hit.distance + " / " + hit.collider.name);
                //    if (hit.collider.gameObject.tag == "Player")
                //    {
                //        Debug.Log("gameOver");
                //        //GameObject.Find("GameManeger").GetComponent<GameManeger>().GameOver();
                //    }
                //}

                StartCoroutine(StartCountdown(false));
                SetStatusGuard(StatusGuard.Guard);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Player")
        //{

        //    SetStatusGuard(StatusGuard.Follow);
        //    agent.isStopped = true;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, new Vector3(playerTransform.position.x, 0.6f, playerTransform.position.z), out hit, layerMask))
            {
                if (hit.transform.tag == "Player")
                {
                    Debug.Log("gameOver");
                    //GameObject.Find("GameManeger").GetComponent<GameManeger>().GameOver();
                }
                Debug.DrawLine(transform.position, new Vector3(playerTransform.position.x, 0.6f, playerTransform.position.z), Color.red);
            }
            else
            {
                Debug.DrawLine(transform.position, new Vector3(playerTransform.position.x, 0.6f, playerTransform.position.z), Color.yellow);
            }

            //Ray ray = new Ray(transform.position,(other.gameObject.transform.position - transform.position).normalized);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, 5f))
            //{
            //    //Debug.Log(hit.distance + " / " + hit.collider.name);
            //    if (hit.collider.gameObject.tag == "Player")
            //    {
            //        Debug.Log("gameOver");
            //        //GameObject.Find("GameManeger").GetComponent<GameManeger>().GameOver();
            //    }
            //    Debug.DrawRay(transform.position, (other.gameObject.transform.position - transform.position).normalized * hit.distance, Color.red);
            //}

        }
    }

    public IEnumerator StartCountdown(bool teste)
    {
        currCountdownValue = 1;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        if (teste)
        {
            currentWay = NextPath(currentWay);
        }
        else
        {
            agent.isStopped = false;
        }
        SetStatusGuard(StatusGuard.Walk);
    }

    private int NextPath(int way)
    {
        if (way < WayPoints.Length - 1)
        {
            way++;
        }
        else
        {
            way = 0;
        }
        return way;
    }

    private void SetStatusGuard(StatusGuard status)
    {
        currentStatus = status;
    }

    private StatusGuard GetStatusGuard()
    {
        return currentStatus;
    }
}
