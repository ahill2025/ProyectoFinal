using UnityEngine.UI;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
   public Image FillLifeBar;
   private Life playerLife;
   private float maxLife;
   
   void Start()
   {
       playerLife = GameObject.Find("Player").GetComponent<Life>();
       maxLife = playerLife.amount;
   }

   void Update()
    {
         FillLifeBar.fillAmount = playerLife.amount / maxLife;
    }
}