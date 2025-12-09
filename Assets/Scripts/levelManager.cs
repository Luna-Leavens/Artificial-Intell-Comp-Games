using UnityEngine;

public class levelManager : MonoBehaviour
{
    private static levelManager lvlManagerInstance;
    public bool[] lvlUnlock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (lvlManagerInstance == null) {
		    lvlManagerInstance = this;
            lvlUnlock = new bool[4]; // increase as we add more levels
            lvlUnlock[0] = true;
	    } else {
		    DestroyObject(gameObject);
	    }
    }
}
