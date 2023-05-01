using System;
using System.Linq;
using UnityEngine;

public class ShipSupplier : MonoBehaviour
{
    public Vector3 change;
    public float speed = 20.0f;
    public float speedByScreenSize { get; set; } = 1f;
    private static readonly float pixelFrac = 1f / 32f;
    
    private ContactFilter2D _triggerContactFilter2D;
    public PizzaItem[] pizzaItems;

    public LandingStages Customers;
    
    /// <summary>
    /// Animation vom schiff
    /// </summary>
    private Animator anim;

    private void Update()
    {
        var found = this._boxCollider2D.OverlapCollider(this._triggerContactFilter2D, this._collider2Ds);
        if (found == 0)
        {
            //Debug.Log("Landing Stages zurück setzen, für kann wieder beliefiert werden.");
            foreach (var item in this.Customers.LandingStageItems)
            {
                item.DeliverStatus = DeliverStatus.Delivered;
            }
            return;
        }
        
        for (var i = 0; i < found; i++)
        {
            //Debug.Log($"Pizza supplier or customer: {i}");
            
            var foundCollider = this._collider2Ds[i];
            if (!foundCollider.isTrigger)
            {
                continue;
            }

            //Debug.Log("objekt gefunden");
                
            foreach (var landingStage in foundCollider.GetComponents<ReachableLandingStage>())
            {
                switch (landingStage)
                {
                    case PizzaDelivery pizzaDelivery:
                    {
                        //Debug.Log("Pizza Delivery");
                        var prepare = pizzaDelivery.GetOrders();
                        //Debug.Log($"Pizza Delivery -  Order {prepare.Count}");
                        for (int j = 0; j < this.pizzaItems.Length; j++)
                        {
                            if(!prepare.Any()) break;

                            if (this.pizzaItems[j].ActualPizza() != PizzaOrders.None) continue;
                            
                            
                            var getFirstPrepare = prepare.First(f => f.Props.PizzaOrder != PizzaOrders.None);
                            //Debug.Log($"Get Order: {getFirstPrepare.Props.PizzaOrder}, ID:{getFirstPrepare.Props.PizzaId}");
                            this.pizzaItems[j].SetPizzaOrder(getFirstPrepare.Props);
                            //Debug.Log($"Show HUD Pizza Item: {this.pizzaItems[j].name} ");
                            this.pizzaItems[j].Show();
                            prepare.Remove(getFirstPrepare);
                        }
                        
                        break;
                    }
                    case LandingStage landing:
                    {
                        if(!landing.HasOrdered) break;
                        if(landing.DeliverStatus == DeliverStatus.Delivering) return;
                        
                        landing.DeliverStatus = DeliverStatus.Delivering;
                        
                        //Debug.Log("Customer");
                        for (int j = 0; j < this.pizzaItems.Length; j++)
                        {
                            //Debug.Log($"Actual Pizza Order{this.pizzaItems[j].Props.PizzaOrder}");
                            if(this.pizzaItems[j].Props.PizzaOrder == PizzaOrders.None) continue;
                            
                            //Debug.Log($"Pizza ausliefern an {landing.name}, Pizza: {this.pizzaItems[j].Props.PizzaOrder}, {this.pizzaItems[j].Props.PizzaId}");
                            landing.DeliverPizza(this.pizzaItems[j].Props);
                            this.pizzaItems[j].SetOrderNothing();
                            this.pizzaItems[j].Hide();
                            break;
                        }
                        break;
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        this.anim.SetFloat("change_x", this.change.x);
        this.anim.SetFloat("change_y", this.change.y);
        
        // if moving is stop, the ship hold the direction
        if(this.change.y <= -1f) this.anim.SetFloat("lookAt", 0f);
        else if(this.change.x <= -1f) this.anim.SetFloat("lookAt", 1f);
        else if(this.change.y >= 1f) this.anim.SetFloat("lookAt", 2f);
        else if(this.change.x >= 1f) this.anim.SetFloat("lookAt", 3f);
        
        
        var step = roundToPixelGrid(Time.deltaTime);
        var oldPosition = this.transform.position;
        var setSpeed = this.change * step * this.speed * this.speedByScreenSize;
        
        //Debug.Log($"{setSpeed}");
        this.transform.position += setSpeed;
        if (this.isColliding())
        {
            //Debug.Log($"collide: {DateTime.Now}, Count: {this._countColliders}, Change: {this.change}, old:{oldPosition}, new: {this.transform.position}");
            this.transform.position = oldPosition;
        }
        
        //Debug.Log("Change to zero");
        this.change = Vector3.zero;
    }

    private void Start()
    {
        foreach (var item in this.pizzaItems)
        {
            //Debug.Log($"Hide HUD Pizza Item: {item.name}");
            item.Hide();
        }
    }

    private void Awake()
    {
        this._boxCollider2D = this.GetComponent<BoxCollider2D>();
        this._collider2Ds = new Collider2D[10];
        this._obstacleFilter = new ContactFilter2D();
        this.anim = this.GetComponent<Animator>();

        this._triggerContactFilter2D = new ContactFilter2D
        {
            useTriggers = true
        };
    }
    
    private BoxCollider2D _boxCollider2D;
    private Collider2D[] _collider2Ds;
    private ContactFilter2D _obstacleFilter;
    private int _countColliders;

    
    private bool isColliding()
    {
        this._countColliders = this._boxCollider2D.OverlapCollider(this._obstacleFilter, this._collider2Ds);
        return this._countColliders > 0;
    }

    private static float roundToPixelGrid(float f)
    {
        return Mathf.Ceil(f / pixelFrac) * pixelFrac;
    }
}
