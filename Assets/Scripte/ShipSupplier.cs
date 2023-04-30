using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipSupplier : MonoBehaviour
{
    public Vector3 change;
    public float speed = 1f;
    public float speedByScreenSize { get; set; } = 1f;
    private static readonly float pixelFrac = 1f / 32f;
    
    public PizzaItem[] orders;
    
    /// <summary>
    /// Animation vom schiff
    /// </summary>
    private Animator anim;

    private void Update()
    {
        var found = this._boxCollider2D.OverlapCollider(this._triggerContactFilter2D, this._collider2Ds);
        for (var i = 0; i < found; i++)
        {
            Debug.Log($"Pizza supplier or customer: {i}");
            
            var foundCollider = this._collider2Ds[i];
            if (!foundCollider.isTrigger)
            {
                continue;
            }

            Debug.Log("objekt gefunden");
                
            foreach (var landingStage in foundCollider.GetComponents<ReachableLandingStage>())
            {
                switch (landingStage)
                {
                    case PizzaDelivery pizzaDelivery:
                    {
                        var prepare = pizzaDelivery.GetOrders();
                        foreach (var pizzaItem in this.PizzaItems)
                        {
                            if(!prepare.Any()) break;

                            if (pizzaItem.PizzaOrder != PizzaOrders.None) continue;
                            
                            var getFirstPrepare = prepare.First();
                            pizzaItem.PizzaOrder = getFirstPrepare.PizzaOrder;
                            pizzaItem.Show();
                        }
                        break;
                    }
                    case LandingStage landing:
                    {
                        var nextPizza = this.orders.FirstOrDefault(f => f.PizzaOrder != PizzaOrders.None);
                        if (nextPizza == null)
                        {
                            continue;
                        }
                        
                        landing.DeliverPizza(nextPizza.PizzaOrder);
                        break;
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {
        if(this.change == null) return;
        
        this.anim.SetFloat("change_x", this.change.x);
        this.anim.SetFloat("change_y", this.change.y);
        
        // if moving is stop, the ship hold the direction
        if(this.change.y <= -1f) this.anim.SetFloat("lookAt", 0f);
        else if(this.change.x <= -1f) this.anim.SetFloat("lookAt", 1f);
        else if(this.change.y >= 1f) this.anim.SetFloat("lookAt", 2f);
        else if(this.change.x >= 1f) this.anim.SetFloat("lookAt", 3f);
        
        
        var step = roundToPixelGrid(Time.deltaTime);
        var oldPosition = this.transform.position;
        this.transform.position += this.change * step * this.speed; // * this.speedByScreenSize;
        if (this.isColliding())
        {
            Debug.Log($"collide: {DateTime.Now}, Count: {this._countColliders}, Change: {this.change}, old:{oldPosition}, new: {this.transform.position}");
            this.transform.position = oldPosition;
        }
        
        Debug.Log("Change to zero");
        this.change = Vector3.zero;
    }
    
    private ContactFilter2D _triggerContactFilter2D;
    public PizzaItem[] PizzaItems;
    
    private void Awake()
    {
        this._boxCollider2D = this.GetComponent<BoxCollider2D>();
        this._collider2Ds = new Collider2D[10];
        this._obstacleFilter = new ContactFilter2D();
        this.anim = this.GetComponent<Animator>();

        foreach (var item in this.PizzaItems)
        {
            item.Hide();
        }

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
