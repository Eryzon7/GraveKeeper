using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpUI : MonoBehaviour
{
    public Image[] icon;
    public TMP_Text[] Options;
    public TMP_Text[] Despription;

    private bool active = false;
    public GameObject selector;
    private PowerUp assignedPowerUp;



    public void Setup(PowerUp powerUp, int counter)
    {
        assignedPowerUp = powerUp;

        if (Options != null && counter < Options.Length)
            Options[counter].text = assignedPowerUp.name;
        else
            Debug.LogWarning("Options array not set or too small!");

        if (icon != null && counter < icon.Length && assignedPowerUp.icon != null)
            icon[counter].sprite = assignedPowerUp.icon;
        else
            Debug.LogWarning("Icon array not set, too small, or missing sprite!");

        if (Despription != null && counter < Despription.Length)
            Despription[counter].text = assignedPowerUp.name;
        else
            Debug.LogWarning("Options array not set or too small!");
    }

    public void ToggleUI()
    {
        if (active)
        {
            active = false;
            selector.SetActive(false);
        }
        else if (!active)
        {
            active = true;
            selector.SetActive(true);
        }

    }
}
