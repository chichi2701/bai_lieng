using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickHandler : MonoBehaviour {

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<PlayerController>() != null)
                {
                    hit.collider.GetComponent<PlayerController>().ToggleCanTakeCard();
                }

                if (hit.collider.GetComponent<Card>() != null)
                {
                    hit.collider.GetComponent<Card>().FlipCard_Face();
                }
            }
        }
	}

}
