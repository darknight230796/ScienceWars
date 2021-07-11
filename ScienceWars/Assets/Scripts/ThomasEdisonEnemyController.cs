using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState{
    CHASE,
    ATACK,
    DEFENCE
    }

public class ThomasEdisonEnemyController : MonoBehaviour {

    private ThomasEdisonAnimation enemyAnim;
    private NavMeshAgent nav;

    private Transform playerTarget;

    public float attackDistance = 1f;
    public float movementSpeed = 0.5f;
    public float chasePlayerAfterAttackDistance = 3f;
    public float waitBeforeTimeAttack = 3f;

    private float attackTimer;
    private EnemyState enemyState;


	void Awake () {
        enemyAnim = GetComponent<ThomasEdisonAnimation>();
        nav = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindGameObjectWithTag(Tags.player).transform;

	}

    private void Start()
    {
        enemyState = EnemyState.CHASE;
        attackTimer = waitBeforeTimeAttack;
    }

    // Update is called once per frame
    void Update () {
        if (enemyState == EnemyState.CHASE)
            chasePlayer();
        else if (enemyState == EnemyState.ATACK)
            attackPlayer();
	}

    void chasePlayer()
    {
        nav.SetDestination(playerTarget.position);
        nav.speed = movementSpeed;

        if (nav.velocity.sqrMagnitude == 0)
        { enemyAnim.walk(false); }
        else
        { enemyAnim.walk(true); }

        if (Vector3.Distance(transform.position, playerTarget.position) <= attackDistance)
            enemyState = EnemyState.ATACK;
    }

    void attackPlayer()
    {
        nav.velocity = Vector3.zero;
        nav.isStopped = true;

        enemyAnim.walk(false);
        attackTimer += Time.deltaTime;
        if(attackTimer > waitBeforeTimeAttack)
        {
            if(Vector3.Distance(transform.position, playerTarget.position) < 1f)
            {
                enemyAnim.slam1();
            }
            else
            {
                enemyAnim.oneHand();
            }

            attackTimer = 0;
        }

        if(Vector3.Distance(transform.position, playerTarget.position)> attackDistance + chasePlayerAfterAttackDistance)
        {
            nav.isStopped = false;
            enemyState = EnemyState.CHASE;
        }
    }
}


































