using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script responsible for the slider logic when we select the number of zombies in the edit mode.
/// </summary>
public class ZombieSlider : MonoBehaviour
{
    public GameEvent<ZombieType> ZombieTypeSet;
    
    [SerializeField]
    private Slider _slider;

    /// <summary>
    /// We are raising the event with the corresponding zombie number data.
    /// </summary>
    public void SliderChangedHandler()
    {
        int intValue = Mathf.RoundToInt(_slider.value);
        ZombieTypeSet.Raise(new ZombieType(gameObject.name, intValue));
    }
}