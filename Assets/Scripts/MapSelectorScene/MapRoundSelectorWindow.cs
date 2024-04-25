using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapRoundSelectorWindow : MonoBehaviour
{
    public GameEvent<string> LoadScene;

    [SerializeField]
    private MapRoundNumberSO _mapRounds;

    [SerializeField]
    private TextMeshProUGUI _numberText;

    [SerializeField]
    private Slider _slider;

    public void SliderValueChangedHandler()
    {
        _numberText.text = _slider.value.ToString();
    }

    public void OkButtonHit(Void data)
    {
        _mapRounds.value = Mathf.RoundToInt(_slider.value);
        LoadScene.Raise("Game");
    }
}
