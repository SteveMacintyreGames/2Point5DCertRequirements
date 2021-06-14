using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("UIManager is null");
            }
            return _instance;
        }
    }

    [SerializeField] private Text _collectiblesText;

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _collectiblesText.text = "Collectibles: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCollectibles(int numberOfCollectibles)
    {
        _collectiblesText.text = "Collectibles: " + numberOfCollectibles;
    }
}
