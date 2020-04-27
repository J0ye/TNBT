using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Entity : MonoBehaviour
{
    public int startHealth = 100;
    public Slider slider;
    public UnityEvent OnDamage;
    public UnityEvent OnHeal;
    public UnityEvent OnDeath;
    protected int health;
    // Start is called before the first frame update
    void Start()
    {
        SetUpEvent(OnDeath);
        health = startHealth;
    }

    public int GetHealth()
    {
        return health;
    }
    
    public void Damage(int damage)
    {
        if(health - damage > 0)
        {
            health -= damage;
            OnDamage.Invoke();
        }else
        {
            health = 0;
            OnDeath.Invoke();
        } 
        UpdateSlider();
    }

    public void Heal(int val)
    {
        if(health > 0)
        {
            health += val;
            OnHeal.Invoke();
            UpdateSlider();
        }
    }

    protected void UpdateSlider()
    {
        if(slider != null)
        {
            slider.value = health;
        }
    }
    public static void SetUpEvent(UnityEvent e)
    {
        if (e == null)
            e = new UnityEvent();
    }
}
