using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    public Transform target; 
    private GameObject gamemanager;
// Reference to the game over text

    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("gamemanager");
        // Get reference to the NavMeshAgent component attached to the enemy GameObject
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            // If NavMeshAgent component is not found, log an error
            Debug.LogError("NavMeshAgent component not found on the enemy GameObject.");
        }

        // Set the target to the ship's transform by default
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            if (target == null)
            {
                // If ship's transform is not found, log an error
                Debug.LogError("Ship's transform not found.");
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object tagged as "Cannon"
        if (collision.gameObject.CompareTag("cannon"))
        {
            // Destroy the enemy
            gamemanager.GetComponent<gamemanager>().increasescore();
            Destroy(gameObject);
        }

        // Check if the collision is with the player ship

    }

    void Update()
    {
        // Follow the ship by setting the destination to the ship's position
        if (agent != null && target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
