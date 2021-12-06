using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfHits : MonoBehaviour, IHealth
{
    [SerializeField]
    private int healthInHits = 15;
    
    [SerializeField]
    private int poisonDMG = 2;
    
    [SerializeField]
    private float poisonTime = 1f;

    [SerializeField]
    private float invulnTimeAfterHit = 5f;

    private int hitsRemaining;
    private int poisonCount = 3;
    private bool canTakeDmg = true;
    private bool canTakePoisonDmg = true;

    public event Action<float> OnHPPctChanged = delegate (float f) { };
    public event Action<float> OnPoison = delegate (float f) { };
    public event Action OnDied = delegate { };

    public float CurrentHPPct
    {
        get { return (float)hitsRemaining / (float)healthInHits; }
    }

    private void Start()
    {
        hitsRemaining = healthInHits;
    }

    public void TakeDamage(int amt)
    {

        if (canTakeDmg)
        {
            StartCoroutine(InvulnTimer());
            hitsRemaining--;
            OnHPPctChanged(CurrentHPPct);
        }

        if(canTakePoisonDmg)
        {
            StartCoroutine(Poison());
        }
    }

    private IEnumerator InvulnTimer()
    {
        canTakeDmg = false;
        yield return new WaitForSeconds(invulnTimeAfterHit);
        canTakeDmg = true;
    }
    private IEnumerator Poison()
    {
        canTakePoisonDmg = false;
        for (int i = 0; i < poisonCount; i++)
        {
            if (hitsRemaining > 0)
            {
                hitsRemaining -= poisonDMG;
                yield return new WaitForSeconds(poisonTime);
                OnPoison(CurrentHPPct);
            }
        }
        if (hitsRemaining <= 0)
            OnDied();
        OnHPPctChanged(CurrentHPPct);
        canTakePoisonDmg = true;
    }
}
