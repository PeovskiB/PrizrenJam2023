using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    /*
    public Transform hit_points_transform, mana_transform;
    public GameObject heart_prefab, potion_prefab;
    public Stack<GameObject> hearts;
    public List<Slider> potions;
    private Entity player;
    private int hit_points;
    private int mana;
    void Start()
    {
        player = Entity.player;

        player.OnHurt += Hurt;
        player.OnManaChange += ManaChange;

        hit_points_transform = GameObject.FindWithTag("HitPoints").transform;
        mana_transform = GameObject.FindWithTag("Mana").transform;

        hit_points = player.data.start_hit_points; 
        mana = player.data.max_mana; 

        hearts = new Stack<GameObject>(hit_points);
        potions = new List<Slider>(mana);

        for (int i = 0; i < hit_points; i++)
            hearts.Push(Instantiate(heart_prefab, hit_points_transform));
        for (int i = 0; i < mana; i++){
            Slider sl =Instantiate(potion_prefab, mana_transform).GetComponent<Slider>();
            sl.value = 1;
            potions.Add(sl);
        }
    }
    void OnDisable(){
        player.OnHurt -= Hurt;
        player.OnManaChange -= ManaChange;
    }
    private void Hurt(int new_hit_points) {
        if (new_hit_points < 0) return;

        int diff = new_hit_points - hit_points;
        for (int i = 0; i < Mathf.Abs(diff); i++){
            if (diff > 0) 
                hearts.Push(Instantiate(heart_prefab, hit_points_transform));
            else if (diff < 0)
                Destroy(hearts.Pop());
        }
        hit_points = new_hit_points;
    }
    private void ManaChange(float new_mana){
        for (int i = 0; i < mana; i++) 
            potions[i].value = Mathf.Clamp01(new_mana - i);
    }
    */
}