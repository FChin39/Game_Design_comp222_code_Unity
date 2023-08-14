using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spaceship : MonoBehaviour
{
    private float speed = 0; // The speed of the spaceship
    private float timerOperationStart; // Timer for users to vaildly operate the spaceship
    private float timerSocreGain; // Timer for users to vaildly get mark to avoid repetitive marking.
    public int score; // The score of user.
    public Text scoreText; // The UI text of score
    public Text nextLevelText; // The UI text of passing the current level
    public Text speedText; // The UI text of current speed
    public Image diedImage; // The UI image of loosing
    public GameObject shootWindow; // The bullet shoot window of spaceship
    public AudioSource eatSource; // The audio sourcce of getting marks
    public AudioClip eat; // The audio clip of getting marks
    private string sceneName; // The name of current scene name
    private bool died = false; // The status of the spaceship: died/alive

    // Start is called before the first frame update
    void Start()
    {
        timerOperationStart = 5.2f;
        score = 0;
        sceneName = SceneManager.GetActiveScene().name;
        eatSource.clip = eat;
    }

    // Update is called once per frame
    void Update()
    {
        // decrease the time of timer
        timerOperationStart -= Time.deltaTime;
        timerSocreGain -= Time.deltaTime;

        // Display the speed on the UI.
        speedText.text = "Speed\n" + (int)(speed*1000) + "\nkm/h";
        if (speed > 0.14 || died)
        {
            speedText.color = Color.red;
        } 
        else
        {
            speedText.color = Color.white;
        }


        // Check players' command of quiting the game.
        if (Input.GetKey(KeyCode.Escape) && Input.GetKey(KeyCode.E))
        {
            Application.Quit();
        }

        // Check players' command of restarting the game.
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(sceneName);
        }

        // // Check players' command of swithing levels.
        if (sceneName == "Scene1" && Input.GetKey(KeyCode.T))
        {
            SceneManager.LoadScene("Scene2");
        }
        if (sceneName == "Scene2" && Input.GetKey(KeyCode.T))
        {
            SceneManager.LoadScene("Scene1");
        }

        // When players can operate the spaceship vaildly, check the command of operation
        if (timerOperationStart <= 0)
        {
            // If the status is died, restart the game
            if (died) {
                diedImage.GetComponent<Image>().color = new Color(255, 255, 255, 0);
                SceneManager.LoadScene(sceneName);
            }

            // Forward speed up
            if (Input.GetKey(KeyCode.W)&&speed<=0.2)
            {
                speed += (float)0.001;
            }

            // Decrease the speed
            if (Input.GetKey(KeyCode.S) && speed > 0)
            {
                speed -= (float)0.005;
            }
            else if (speed <= 0) { 
                speed = 0;
            }

            // Apply the speed on the spaceship
            transform.Translate(speed, 0, 0);

            // Rotation
            if (Input.GetKey(KeyCode.A)) {
                transform.Rotate(0,(float)-3,0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0, (float)3, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // if the spaceship collides with walls or sateroids, the player will loose.
        if ((collision.gameObject.tag == "wall"|| collision.gameObject.tag == "asteroid" || collision.gameObject.tag == "enemyBullet")&& died==false) {
            timerOperationStart = 3f;
            diedImage.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            died = true;
            speed = 0;
            Destroy(shootWindow);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        // If the spaceship get the crystal, the player will get 100 marks.
        if (other.gameObject.tag == "crystal")
        {
            if (timerSocreGain <= 0)
            {
                timerSocreGain = 0.005f;
                score += 100;
                eatSource.Play();
                Destroy(other.gameObject);
            }

            // Update the score text on the UI.
            scoreText.text = "Score: " + score + "\nGoal: 2000";
            if (score >= 2000)
            {
                speed = 0;
                if (sceneName == "Scene1")
                {
                    nextLevelText.text = "Well done!\n(¡«£þ¨Œ£þ)¡«\nJump to the next space: T";
                }
                else
                {
                    nextLevelText.text = "Good Job!!\nThanks for playing my game!!!\nq(¨R¨Œ¨Qq)\n\nReplay: T\nQuit: Esc + E";
                }
                scoreText.text = "Score: " + score + "\nGoal: 2000\nYou Win!!!";
            }
        }
    }
}
