using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class LifeBar : MonoBehaviour
{
   public Image FillLifeBar;
   private Life playerLife;
   private float maxLife;
   private ScoreManager score;
   private TextMeshProUGUI scoreText;
   
   void Start()
   {
       playerLife = GameObject.Find("Player").GetComponent<Life>();
       maxLife = playerLife.amount;
       scoreText =GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
       score = ScoreManager.instance;
   }

   void Update()
    {
        FillLifeBar.fillAmount = playerLife.amount / maxLife;
        scoreText.text = "Score: " + score.amount;
    }
}