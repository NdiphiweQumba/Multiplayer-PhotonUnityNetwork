public interface IDamagable
{
    public float ItemHealth { get; set; }
    public float DamageAmount { get; set; } 
    public float Damage(string itemName, float amount);   
}
