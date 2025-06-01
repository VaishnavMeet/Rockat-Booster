using UnityEngine;

public class MoveCar : MonoBehaviour
{
    [SerializeField] Rigidbody carRigidbody;
    [SerializeField] float speed = 5f;

    private bool shouldMove = false;

    void FixedUpdate()
    {
        if (shouldMove && carRigidbody != null)
        {
            Vector3 targetPosition =new Vector3(10.0100002f, -0.175862774f, -15.59000015f);
            Vector3 direction = (targetPosition - carRigidbody.position).normalized;
            Vector3 movement = direction * speed * Time.fixedDeltaTime;

            carRigidbody.MovePosition(carRigidbody.position + movement);

            if (Vector3.Distance(carRigidbody.position, targetPosition) < 0.1f)
            {
                shouldMove = false;
                Debug.Log("Reached target!");
            }
        }
    }

  

    private void OnTriggerEnter(Collider other)
    {
        shouldMove = true;
    }
}
