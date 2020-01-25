using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public Animator animator;
    private int SceneNumber;

    private void Start()
    {
       // GameObject building_Pref_Holder = GetComponent<GroundNode>().currentBuilding ;
     //   GetComponent<Building_Logic>().LoadScene(building_Pref_Holder.GetComponent<Building_Logic>().Choosed_BuildingInfo);
        print("boom");
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            FadeToBuilding(3);
        }
	}

    public void FadeToBuilding(int SceneIndex)
    {
        SceneNumber = SceneIndex;
        animator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneNumber);
    }
    
}
