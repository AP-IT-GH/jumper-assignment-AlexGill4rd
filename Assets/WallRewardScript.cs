using UnityEngine;

public class WallRewardScript : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Tester");
        FindObjectOfType<DemoAgent>().GetComponent<DemoAgent>().AddReward(1f);
        FindObjectOfType<DemoAgent>().GetComponent<DemoAgent>().EndEpisode();
    }
}
