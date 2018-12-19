using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int id;

    public int nextId;
    public int prevId;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Actor"))
            return;

        LapController controller = other.GetComponent<LapController>();

        if (!controller)
            return;

        if (controller.currentId == prevId || controller.currentId == nextId)
        {
            if (id == 0 && controller.currentId == prevId)
            {
                controller.currentLap++;

                GameManager.Instance.AddTime();
            }

            controller.currentId = id;
        }
    }
}
