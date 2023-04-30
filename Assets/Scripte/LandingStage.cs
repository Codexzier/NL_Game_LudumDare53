using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LandingStage : MonoBehaviour
{
    public PizzaItem[] PizzaItems;

    private bool hasOrder;
    
    
    public float stepTime = 0.03f;
    public float countdownForNextOrderMin = 3f;
    public float countdownForNextOrder;
    public float orderTimeStatus;
    public float orderTimeStatusMax = 1f;

    public int pointsForPizzaDelivered;
    
    public LandingStagePlace LandingStagePlace;
    public int countDeliverdOutofTime;
    public PizzaOrderIcon PizzaOrderIcon { get; set; }
    public DeliveryOutOfTimeIcon deliveryOutOfTimeIcon { get; set; }

    void Start()
    {
        foreach (var item in this.PizzaItems)
        {
            item.Hide();
        }

        if (this.PizzaOrderIcon == null) throw new NullReferenceException("Pizza Order Icon muss zugewiesen werden!");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.hasOrder) return;

        var step = Time.deltaTime * this.stepTime;
        this.countdownForNextOrder += step;
        
        if (this.LandingStagePlace.NeedPizzaOrder == PizzaOrders.None) return;

        // TODO: Warum wird hier zurück gesetzt
        this.countdownForNextOrder = 0f;

        if (this.LandingStagePlace.HasAnOrder) this.HasOrdered();

        this.orderTimeStatus += step;
        if (this.orderTimeStatus < orderTimeStatusMax) return;

        this.deliveryOutOfTimeIcon.Show();
    }

    public void StartOrder(PizzaOrders pizzaOrder)
    {
        if(this.countdownForNextOrder < this.countdownForNextOrderMin) return;
        
        this.LandingStagePlace.NeedPizzaOrder = pizzaOrder;
        
        this.PizzaOrderIcon.Show();

        this.PizzaItems
            .First(order => order.PizzaOrder == this.LandingStagePlace.NeedPizzaOrder)
            .Show();
        
        this.LandingStagePlace.NeedPizzaOrder = pizzaOrder;
        
        this.orderTimeStatus = 0;
    }

    private void HasOrdered()
    {
        if(!this.LandingStagePlace.HasOrdered) return;
        
        var points = this.LandingStagePlace.GetPoints();

        // ein Punkt Abzug wenn außerhalb der Zeit
        if (this.countdownForNextOrder > 1f)
        {
            points--;
            this.countDeliverdOutofTime++;
        }

        this.pointsForPizzaDelivered += points;

        this.LandingStagePlace.HasOrdered = false;
        this.LandingStagePlace.HasAnOrder = false;
        this.LandingStagePlace.NeedPizzaOrder = PizzaOrders.None;
        
        this.deliveryOutOfTimeIcon.Hide();
        this.PizzaOrderIcon.Hide();
        
        foreach (var item in this.PizzaItems)
        {
            item.Hide();
        }

        this.countdownForNextOrder = 0f;
        
        if(this.countdownForNextOrderMin < 0.5f) return;
        this.countdownForNextOrderMin -= 0.1f;
    }

}