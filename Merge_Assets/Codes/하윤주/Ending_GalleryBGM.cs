using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_GalleryBGM : MonoBehaviour
{
    static Ending_GalleryBGM Instance;
    public static Ending_GalleryBGM instance
    {
        get
        {
            if (Instance == null)
            {
                var obj = FindObjectOfType<Ending_GalleryBGM>();
                if (obj != null)
                {
                    Instance = obj;
                }
                else
                {
                    var newEnding_GalleryBGM = new GameObject("Ending&Gallery").AddComponent<Ending_GalleryBGM>();
                    Instance = newEnding_GalleryBGM;
                }
            }
            return Instance;
        }
        private set
        {
            Instance = value;
        }
    }

    void Awake()
    {
        var objs = FindObjectsOfType<Ending_GalleryBGM>();
        if (objs.Length != 1) { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene") { Destroy(gameObject); }
    }
}
