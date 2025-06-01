using UnityEngine;
using UnityEngine.SceneManagement;

public class ColllisionScript : MonoBehaviour
{
    [SerializeField] AudioClip Fail;
    [SerializeField] AudioClip Finish;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Movement movement;
    [SerializeField] ParticleSystem finishParticals;
    [SerializeField] ParticleSystem failParticals;
    bool isStopFlag=false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isStopFlag) { return; }
        switch(collision.gameObject.tag)
        {
            case "FinishPos":
                StopMovements();
                audioSource.PlayOneShot(Finish);
                finishParticals.Play();
                Invoke("OnFinishLevel", 2f);
                break;
            case "StartPos":
                break;
            default:
                StopMovements();
                failParticals.Play();
                audioSource.PlayOneShot(Fail);
                Invoke("OnFailLevel", 2f);
                break;
        }
    }

    public void OnFinishLevel()
    {  
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevel == SceneManager.sceneCountInBuildSettings)
            nextLevel = 0;
        SceneManager.LoadScene(nextLevel);
    }

    public void OnFailLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StopMovements()
    {
        isStopFlag = true;
        movement.enabled = false;
    }
}
