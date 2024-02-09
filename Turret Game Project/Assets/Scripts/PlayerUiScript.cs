using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUiScript : MonoBehaviour
{
    [SerializeField] private PlayerBase player;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider gunHeatSlider;

    private void Awake()
    {
        player.GunHeatChangeEvent += OnGunHeatChange;
        player.HealthChangeEvent += OnHealthChange;
    }

    private void OnHealthChange(float difference)
    {
        damageText.text = difference.ToString();
        damageText.GetComponent<Animator>().SetTrigger("isDamage");
        healthSlider.value = player.Health / GameManager.MAX_PLAYER_HEAlTH;
    }

    private void OnGunHeatChange()
    {
        gunHeatSlider.value = player.GunHeat / GameManager.MAX_GUN_HEAT;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(player.transform.position);
    }
}
