using UnityEngine;
using System.Collections;

public class FireHazard : MonoBehaviour
{
    public int damagePerTick = 5;
    public float tickInterval = 1f;
    private float tickTimer = 0f;

    [Header("Fire Spread")]
    public bool isBurning = true;
    public float spreadCheckInterval = 2f;   // how often this fire "rolls the dice" to spread
    public float spreadRadius = 4f;          // how far it can jump to a neighboring tree
    [Range(0f, 1f)] public float spreadChance = 0.3f; // probability per check
    public LayerMask treeLayer;              // set to whatever layer your (non-burning) trees are on

    void Start()
    {
        if (isBurning)
        {
            StartCoroutine(SpreadRoutine());
        }
    }

    void OnTriggerStay(Collider other)
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;

            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damagePerTick);
            }
        }
    }

    IEnumerator SpreadRoutine()
    {
        while (isBurning)
        {
            yield return new WaitForSeconds(spreadCheckInterval);

            if (Random.value <= spreadChance)
            {
                TrySpreadToNeighbor();
            }
        }
    }

    void TrySpreadToNeighbor()
    {
        Collider[] nearbyTrees = Physics.OverlapSphere(transform.position, spreadRadius, treeLayer);

        foreach (Collider col in nearbyTrees)
        {
            Tree tree = col.GetComponent<Tree>();

            if (tree != null && !tree.IsBurning())
            {
                tree.SetOnFire();
                break; // spread to just one neighbor per successful roll, keeps it manageable
            }
        }
    }
}