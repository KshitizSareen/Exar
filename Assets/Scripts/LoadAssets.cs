using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadAssets : MonoBehaviour
{
    public static string AssetName;
    private GameObject Character;
    [SerializeField]
    private Slider slider;
    private Animation animation;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        Debug.Log(AssetName);
        Get(AssetName);
    }

    // Update is called once per frame
    void Update()
    {
        if (animation != null)
        {
            foreach (AnimationState state in animation)
            {
                state.speed = slider.value;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("UIScene");
        }
    }
    private async void Get(string label)
    {
        var locations = await Addressables.LoadResourceLocationsAsync(label).Task;
        foreach( var location in locations)
        {
            Character = await Addressables.InstantiateAsync(location).Task;
            Character.SetActive(false);
        }
        Character.SetActive(true);
        Character.transform.SetPositionAndRotation(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        animation = Character.GetComponent<Animation>();
    }
}
