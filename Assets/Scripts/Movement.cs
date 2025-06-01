using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    Rigidbody rb;
    [SerializeField] float thrustStrength = 2100f;
    [SerializeField] float rotationStrength = 200f;
    [SerializeField] ParticleSystem leftSideParticals;
    [SerializeField] ParticleSystem rightSideParticals;
    [SerializeField] ParticleSystem CenterParticals;
    AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        if (thrust.IsPressed())
        {
            CenterParticals.Play();
            rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * thrustStrength);
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            CenterParticals.Stop();
            audioSource.Stop();
        }
        RotateMachine();
    }

    private void RotateMachine()
    {
        float f=rotation.ReadValue<float>();
        if (f > 0)
        {
            leftSideParticals.Stop();
            rightSideParticals.Play();
        }
        else if(f < 0)
        {
            rightSideParticals.Stop();
            leftSideParticals.Play();
        }
        else { 
            rightSideParticals.Stop();
            leftSideParticals.Stop();    
        }
            rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationStrength * f);
        rb.freezeRotation = false;
    }
}
