using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdeaManager : MonoBehaviour
{
    /// <summary>
    /// Prefab of the ideas that float inside the idea cloud
    /// </summary>
    [SerializeField] private GameObject objPrefab;

    /// <summary>
    /// Dictionary used to check valid combos
    /// Combos must be added both directions
    /// </summary>
    private Dictionary<string, string> combinations;

    /// <summary>
    /// Contains all ideas currently in the brain
    /// When adding combos make sure to add the string combined in both directions
    /// </summary>
    private Dictionary<string, GameObject> createdIdeas;

    private DialogueReader dr;

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
        dr = GetComponent<DialogueReader>();
        // ----------Instatiate objects -----------------
        combinations = new Dictionary<string, string>();
        createdIdeas = new Dictionary<string, GameObject>();

        // Read all possible cominations
        List<string> combos = dr.Responses["Combos"];
        int i = 0;
        while (i < combos.Count)
        {
            AddCombo(combos[i], combos[i + 1], combos[i + 2]);
            i += 3;
        }

        // ------ Hardcoded combinations ----------------
        AddCombo("Obj", "Obj2", "Obj3");
    }

    /// <summary>
    /// Adds the combination plus their resulting idea into possible 
    /// combinations dictionary
    /// </summary>
    private void AddCombo(string a_sIdea, string a_sIdea2, string a_sResult)
    {
        combinations.Add(a_sIdea + a_sIdea2, a_sResult);
        combinations.Add(a_sIdea2 + a_sIdea, a_sResult);
    }

    /// <summary>
    /// Returns whether a combination exist that requires the two ideas
    /// </summary>
    public bool CheckCombo(string a_sIdea, string a_sIdea2)
    {
        if (combinations.ContainsKey(a_sIdea + a_sIdea2) || combinations.ContainsKey(a_sIdea2 + a_sIdea)) return true;

        return false;
    }

    /// <summary>
    /// Adds an idea of the given tag into the brain directly
    /// </summary>
    public void CreateIdea(string a_sIdeaName = "Default")
    {
        if (createdIdeas.ContainsKey(a_sIdeaName) == false && dr.Responses.ContainsKey(a_sIdeaName)) // If the object doesn't already exist 
        {
            // NOTE: Set base description Here
            Debug.Log($"Created {a_sIdeaName}");
            GameObject newObj = Instantiate(objPrefab, this.transform);
            createdIdeas.Add(a_sIdeaName, newObj);
            newObj.name = a_sIdeaName;
        }
    }

    /// <summary>
    /// Adds an idea of the given combo of ideas if valid
    /// </summary>
    public void CreateIdea(string a_sIdea, string a_sIdea2)
    {
        //If our combo exist, we retrieve that combo from our combinations dictionary and create it
        if (CheckCombo(a_sIdea, a_sIdea2)) 
        {
            CreateIdea(combinations[a_sIdea + a_sIdea2]);
        }
        else Debug.Log("Combo not valid");
    }

    /// <summary>
    /// Deletes a pre-existing idea in the brain if it has the idea
    /// </summary>
    public void DeleteIdea(string a_sIdeaName)
    {
        if (IdeaInBrain(a_sIdeaName)){
            GameObject.Destroy(createdIdeas[a_sIdeaName]);
            createdIdeas.Remove(a_sIdeaName);
        }
    }

    /// <summary>
    /// Returns whether the passed in idea exist already in the brain
    /// </summary>
    public bool IdeaInBrain(string a_sIdeaName)
    {
        return createdIdeas.ContainsKey(a_sIdeaName);
    }
    /// <summary>
    /// Updates an existing idea
    /// </summary>
    public void UpdateIdea(string a_sIdeaName)
    {
        if (IdeaInBrain(a_sIdeaName)){
            // UPDATE THE IDEA WITH ITS SECOND DESCRIPTION
            // change color of ui sprite
            // dr.Responses[a_sIdeaName][1]
        }
    }
}
