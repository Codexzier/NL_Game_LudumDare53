using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LandingStages : MonoBehaviour
{
    public LandingStage[] LandingStageItems;
    public PizzaDelivery PizzaDelivery;

    public int countMissings = 0;

    public GameOverBoard GameOver;
    
    public int Score { get; set; }
    
    void Start()
    {
        if (this.PizzaDelivery == null) throw new NullReferenceException("Pizza Lieferant nicht zugewiesen");
    }

    void Update()
    {
        this.Score = this.LandingStageItems.Sum(item => item.pointsForPizzaDelivered);
     this.countMissings = this.LandingStageItems.Sum(item => item.countDeliverdOutofTime);
        
        if(this.countMissings < 10) return;
        
        Debug.Log("Spielende");

        this.GameOver.Score = this.Score;

        this.GameOver.Show();
    }

}

public class GameOverBoard : HideableObjectItem
{
    public int Score { get; set; }
}
