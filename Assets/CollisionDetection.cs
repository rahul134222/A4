using UnityEngine;
using System.Collections;
using UnityEngine.UI; 


public class CollisionDetection : MonoBehaviour
{
    public AudioSource audioPlayer;
    public int initialLives = 3;
    private int lives;
    private bool isInvulnerable = false;
    private float invulnerabilityDuration = 2.0f;
    public AudioSource deathAudioPlayer;
    public Text livesText;
    public AudioSource gameOverSound;
    public AudioSource extraLife;
    public CollectibleManager collectibleManager;
    public SwipeMovement swipeMovement;
    public AudioSource extraSpeed;

    public AudioSource getReady;

    public extraLifeSpawner extraLifeSpawner;


    void Start()
    {
        
        livesText = GameObject.FindGameObjectWithTag("LivesText").GetComponent<Text>();
        

        lives = initialLives;
        UpdateLivesText(); // Update the lives display at the start
        Debug.Log("Lives: " + lives);
        getReady.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Monster" && !isInvulnerable)
        {

            audioPlayer.Play();
            lives--;
            UpdateLivesText(); // Update the lives display whenever lives change
            Debug.Log("Lives left: " + lives);
            

            if (lives <= 0)
            {
                EndGame();
            }
            else
            {
                StartCoroutine(BecomeInvulnerable());
            }
        } else if (collision.gameObject.tag == "extraLife") // Checking for collision with life item
            {
                
                extraLife.Play();
                IncreaseLife(1); // Increase life count by one.
                Destroy(collision.gameObject); // Remove the life item from the scene.
                
                extraLifeSpawner.StartSpawningExtraLife();


            }
            else if (collision.gameObject.tag == "extraSpeed") // Checking for collision with shield/speed item
        {

            extraSpeed.Play();
           if(swipeMovement != null)
                {
                    
                    StartCoroutine(BecomeInvulnerable());
                    Destroy(collision.gameObject); // Remove the power-up from the scene.
                    
                }


            Destroy(collision.gameObject); // Remove the speed boost item from the scene.
        }


    }

    public void IncreaseLife(int amount)
{
    lives += amount;
    UpdateLivesText();
    
}


    IEnumerator BecomeInvulnerable()
    {
        Debug.Log("Becoming Invulnerable");
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
        Debug.Log("Invulnerability Ended");
    }

    void EndGame()
    {

        deathAudioPlayer.Play();
        gameOverSound.Play();
        Debug.Log(gameObject.tag + " Game Over!");
        
        UpdateLivesText();

    }

    void UpdateLivesText()
    {
        if (livesText != null) // Check if the Text component is assigned
        {
            livesText.text = "Lives: " + lives; // Update the text to show the current number of lives
        }
    }
}
