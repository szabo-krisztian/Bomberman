using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DarkScreenController : MonoBehaviour
{
    [SerializeField]
    private Image _darkScreen;

    private int _target;
    private const float LOAD_TIME = .1f;

    private void Start()
    {
        _darkScreen.color = Color.black;
        _target = 0;
    }

    private void Update()
    {
        Color tempColor = _darkScreen.color;
        tempColor.a = Mathf.MoveTowards(tempColor.a, _target, Time.fixedDeltaTime * 2);
        _darkScreen.color = tempColor;
    }

    public void LoadSceneHandler(string scene)
    {
        StartCoroutine(Load(scene));
    }

    private IEnumerator Load(string scene)
    {
        _target = 255;
        yield return new WaitForSeconds(LOAD_TIME);
        SceneManager.LoadScene(scene);
    }
}
