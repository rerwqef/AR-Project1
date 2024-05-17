
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public int score = 0;
    public int chances= 5;
    public int scoreneeded = 1;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI chancesText;
    public bool canCreateBall=true;
    public GameObject LosePannel;
  public GameObject winPannel;
    public GameObject GoalPannel;
    [SerializeField] GameObject misspannel;
    public bool IsGoalTrue=false;
    int losev;
    int winv;
   
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene by its build index
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void GoToHome()
    {   // Reload the current scene by its build index
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Start()
    {
        misspannel.SetActive(false);
        scoreText.text = score.ToString();
        chancesText.text = chances.ToString();
    }
    public void scoreupdater()
    {
        score++;
        scoreText.text=score.ToString();
        StartCoroutine(Goal());
        if (score == scoreneeded)
        {
           Invoke( "ONWin",0.5f);
        }
    }
    IEnumerator Goal()
    {

        Debug.Log("win");
        IsGoalTrue = true;
        GoalPannel.SetActive(true);
        yield return new WaitForSeconds(1);
        IsGoalTrue = false;
        GoalPannel.SetActive(false);
    }
   public   void OnMissedPannel()
    {
       
        StartCoroutine(Miss());
    }
    IEnumerator Miss()
    {
        if (!IsGoalTrue)
        {
            Debug.Log("mISS");
            misspannel.SetActive(true);
            yield return new WaitForSeconds(1);
            misspannel.SetActive(false);
        }

    }
    public  void  ShootBallChanceUpdater()
    {
        chances--;
        chancesText.text = chances.ToString();
        if (chances <= 0&& score != scoreneeded)
        {
           // ONlose();
            canCreateBall=false;
        }
    }

 
 
  
    public void ONWin()
    {
        Debug.Log("Win");
      
        winPannel.SetActive(true);
    }
    public void ONlose()
    {
        Debug.Log("Lose");
        LosePannel.SetActive(true);
    }
}
