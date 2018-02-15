using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private Renderer rend;

    public float maxEnemyHealth = 10f;
    public float goldDrop = 1f;
    private float currentEnemyHealth;
    public Color startColour = Color.green;
    public Color endColour = Color.red;

    // Use this for initialization
    void Start () {
        maxEnemyHealth += WaveMaster.instance.waveNumber ;
        currentEnemyHealth = maxEnemyHealth - 2;
        rend = GetComponent<Renderer>();

    }

    // Update is called once per frame
    void Update () {

        float currentHealthStat = currentEnemyHealth / (maxEnemyHealth + 1);

        rend.material.color = new Color(1 - (1f * currentHealthStat), 1 * currentHealthStat, 0f);

    }
    public void RecieveDamage(float damage)
    {
        currentEnemyHealth = currentEnemyHealth - damage;
        if (currentEnemyHealth <= 0)
        {
            ResourceManager.instance.gold += goldDrop;

            Destroy(gameObject);
        }
    }
}
