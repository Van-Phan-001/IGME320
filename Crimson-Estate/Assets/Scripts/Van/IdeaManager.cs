using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaManager : MonoBehaviour
{
    [SerializeField] private GameObject objPrefab;

    private Dictionary<string, string> combinations;

    #region Singleton definition
    private static IdeaManager _instance;
    public static IdeaManager Instance { get { return _instance; } }
    //Code to reference singleton: SceneChanger.Instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        combinations = new Dictionary<string, string>();
        combinations.Add("Obj" + "Obj2", "Obj3");
    }

    public void CheckCombo(string a_sIdea, string a_sIdea2)
    {
        Debug.Log("Combo checked");
        //checks both order conditions to see if the combination exists
        if(combinations.ContainsKey(a_sIdea + a_sIdea2))
        {
            GameObject newObj = Instantiate(objPrefab, this.transform);
            newObj.name = combinations[a_sIdea + a_sIdea2];
        }
        if (combinations.ContainsKey(a_sIdea2 + a_sIdea))
        {
            GameObject newObj = Instantiate(objPrefab,this.transform);
            newObj.name = combinations[a_sIdea2 + a_sIdea];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
