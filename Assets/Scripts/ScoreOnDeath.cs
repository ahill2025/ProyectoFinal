using UnityEngine;

public class ScoreOnDeath : MonoBehaviour
{
    public int amount;

    void Awake()
    {
        var Life = GetComponent<Life>();        // We did not drag enemy game object, so we declare 'var Life'
        Life.onDeath.AddListener(GivePoints);   // bypass 
    }

    public void GivePoints()
    {
        ScoreManager.instance.amount += amount;     // 'instance' is a global variable, which can be called here.
    }

}
