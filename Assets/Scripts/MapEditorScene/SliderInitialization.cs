using UnityEngine;
using UnityEngine.UI;

public class SliderInitialization : MonoBehaviour
{
    [SerializeField]
    private Slider _normalZombieSlider;

    [SerializeField]
    private Slider _ghostZombieSlider;

    [SerializeField]
    private Slider _intelligentZombieSlider;

    [SerializeField]
    private Slider _veryIntelligentZombieSlider;

    [SerializeField]
    private TilemapSO _loadedMap;

    private void Start()
    {
        foreach (ZombieType zombie in _loadedMap.TilemapData.Zombies)
        {
            SetZombieCount(zombie.Type, zombie.Count);
        }
    }

    private void SetZombieCount(string zombieType, int count)
    {
        switch (zombieType)
        {
            case "Normal":
                _normalZombieSlider.value = count;
                break;
            case "Ghost":
                _ghostZombieSlider.value = count;
                break;
            case "Intelligent":
                _intelligentZombieSlider.value = count;
                break;
            case "VeryIntelligent":
                _veryIntelligentZombieSlider.value = count;
                break;
        }
    }
}
