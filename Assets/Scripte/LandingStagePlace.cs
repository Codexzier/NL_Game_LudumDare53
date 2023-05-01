public class LandingStagePlace
{
    public bool HasAnOrder { get; set; }
    public bool HasOrdered { get; set; }
    
    // TODO: ist mehrfach plaziert 
    public PizzaOrders NeedPizzaOrder { get; set; }

    private int points;
    
    public int GetPoints() => this.points;

    public bool OnDelivery(PizzaOrders pizzaOrder)
    {
        if (pizzaOrder == PizzaOrders.None) return false;
        
        this.points = 2;
        if (pizzaOrder == NeedPizzaOrder) this.points = 3;
        
        return true;
    }


   
}