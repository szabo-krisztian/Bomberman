using UnityEngine;
using UnityEngine.UI;

public class ZombieSlider : MonoBehaviour
{
    public GameEvent<ZombieType> ZombieTypeSet;
    
    [SerializeField]
    private Slider _slider;

    public void SliderChangedHandler()
    {
        int intValue = Mathf.RoundToInt(_slider.value);
        ZombieTypeSet.Raise(new ZombieType(gameObject.name, intValue));
    }
}