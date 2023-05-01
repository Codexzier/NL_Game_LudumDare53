using System;
using UnityEngine;

public class LandingStage : ReachableLandingStage
{
    public PizzaItem Item;
    
    public float stepTime = 0.03f;
    public float countdownForNextOrderMin = 3f;
    public float countdownForNextOrder;
    public float orderTimeStatus;
    public float orderTimeStatusSpeed = 1f;
    public float orderTimeStatusMax = 1f;

    public int pointsForPizzaDelivered = 0;

    public int countDeliverdOutofTime;

    public DeliveryOutOfTimeIcon deliveryOutOfTimeIcon;

    public ProgressBar ProgressBar;
    
    public bool HasOrdered { get; set; }

    private int _points;
    
    public DeliverStatus DeliverStatus = DeliverStatus.Delivered;

    public void Start()
    {
        if (this.Item == null) throw new NullReferenceException("Pizza Order Icon muss zugewiesen werden!");
        if (this.deliveryOutOfTimeIcon == null) throw new NullReferenceException("Delivery out of time icon ist nicht zugewiesen!");
        
        this.Item.Hide();
        this.deliveryOutOfTimeIcon.Hide();
        
        this.ProgressBar.SetValue(0f);
    }

    // Update is called once per frame
    void Update()
    {
        var step = Time.deltaTime * this.stepTime;
        
        if (this.HasOrdered)
        {
            this.orderTimeStatus += step * this.orderTimeStatusSpeed;
            this.ProgressBar.SetValue(this.orderTimeStatus);
            if (this.orderTimeStatus < this.orderTimeStatusMax) return;

            if(this.deliveryOutOfTimeIcon.ToLate) return;
            
            this.countDeliverdOutofTime++;

            this.deliveryOutOfTimeIcon.ToLate = true;
            this.deliveryOutOfTimeIcon.Show();
            return;
        }

        this.countdownForNextOrder += step;
        
        if (this.Item.ActualPizza() == PizzaOrders.None) return;
        
        this.countdownForNextOrder = 0f;
    }

    public void StartOrder(PizzaProps pp)
    {
        //if(this.countdownForNextOrder < this.countdownForNextOrderMin) return;
        
        //Debug.Log($"Start Order set pizza Item {pp.PizzaOrder}");
        this.Item.SetPizzaOrder(pp);
        this.Item.Show();

        this.HasOrdered = true;
        this.orderTimeStatus = 0;
        //this.DeliverStatus = DeliverStatus.CanBeDelivered;
    }

    public void DeliverPizza(PizzaProps order)
    {
        //Debug.Log($"Deliver pizza: {order.PizzaOrder}, HasOrdered: {this.HasOrdered}");
        // no order exist
        if(!this.HasOrdered) return;
        if (order.PizzaOrder == PizzaOrders.None) return;
        
        this._points = 2;
        if (order.PizzaOrder == this.Item.ActualPizza()) this._points = 3;
        //Debug.Log($"Get points for deliver pizza: {this._points}");
        
        // ein Punkt Abzug wenn außerhalb der Zeit
        if (this.countdownForNextOrder > 1f)
        {
            this._points--;
            //this.countDeliverdOutofTime++;
        }
        
        this.pointsForPizzaDelivered += this._points;
        this.HasOrdered = false;
        //this.DeliverStatus = DeliverStatus.Delivered;
        
        this.Item.SetOrderNothing();
        this.Item.Hide();
        this.deliveryOutOfTimeIcon.ToLate = false;
        this.deliveryOutOfTimeIcon.Hide();
        this.ProgressBar.SetValue(0f);
        
        this.countdownForNextOrder = 0f;
        
        if(this.countdownForNextOrderMin < 0.5f) return;
        this.countdownForNextOrderMin -= 0.1f;
    }
}