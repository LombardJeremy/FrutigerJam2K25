using UnityEngine;

public class Carousel : MonoBehaviour
{

	[SerializeField] float spacing = 20;
	[SerializeField, Range(0, 1f)] float opacity_strength = 0.4f;
	[SerializeField, Range(0, 1f)] float scale_strength = 0.4f;
	[SerializeField, Range(0, 1f)] float scale_min = 0.1f;

	[SerializeField] bool wraparoudEnabled = false;
	[SerializeField] float wraparoudRadius = 100;
	[SerializeField] float wraparoudHeight = 50f;


	[SerializeField] float smoothing_speed = 20f;
	[SerializeField] int selectedIndex = 0;

	[SerializeField] Vector2 size = Vector2.one;

	[SerializeField] bool invertAxis = false;

	[SerializeField] Transform childsParent;

	public bool turnAround = false;

	void Update()
	{
		if (childsParent == null || childsParent.childCount == 0) return;

		selectedIndex = Mathf.Clamp(selectedIndex, 0, childsParent.childCount-1);

		for (int i = 0; i < childsParent.childCount; i++)
		{
			if (wraparoudEnabled)
			{
				float maxIndexRange = Mathf.Max(1, (childsParent.childCount-1) / 2f);
				float angle = Mathf.Clamp((i - selectedIndex) / maxIndexRange, -1f, 1f) * Mathf.PI;
				float x = 0;
				float y = 0;

				if (invertAxis)
				{
					y = Mathf.Sin(angle) * wraparoudHeight;
					x = Mathf.Cos(angle) * wraparoudRadius;
				}
				else
				{
					x = Mathf.Sin(angle) * wraparoudHeight;
					y = Mathf.Cos(angle) * wraparoudRadius;
				}

				Vector2 targetPos = Vector2.zero;

				if (invertAxis)
                    //targetPos = new Vector2(x, y - wraparoudHeight) - childsParent.GetChild(i).GetComponent<RectTransform>().rect.size / 2f;
                    targetPos = new Vector2(x, y - wraparoudHeight) - size / 2f;
                else
                    //targetPos = new Vector2(y, x - wraparoudHeight) - childsParent.GetChild(i).GetComponent<RectTransform>().rect.size / 2f;
                    targetPos = new Vector2(y, x - wraparoudHeight) - size / 2f;

                childsParent.GetChild(i).localPosition = Vector2.Lerp(childsParent.GetChild(i).localPosition, targetPos, smoothing_speed * Time.deltaTime);
			}
			else
			{
				if (invertAxis)
				{
					float posY = 0;
					if (i > 0)
					{
                        //posY = childsParent.GetChild(i - 1).localPosition.y + childsParent.GetChild(i - 1).GetComponent<RectTransform>().rect.size.y + spacing;
                        posY = childsParent.GetChild(i - 1).localPosition.y + size.y + spacing;
                    }

                    //childsParent.GetChild(i).localPosition = new Vector2(-childsParent.GetChild(i).GetComponent<RectTransform>().rect.size.x, posY);
                    childsParent.GetChild(i).localPosition = new Vector2(-size.y, posY);

                }
				else
				{
					float posX = 0;
					if (i > 0)
					{
						//posX = childsParent.GetChild(i - 1).localPosition.x + childsParent.GetChild(i - 1).GetComponent<RectTransform>().rect.size.x + spacing;
                        posX = childsParent.GetChild(i - 1).localPosition.x + size.x + spacing;
                    }

                    //childsParent.GetChild(i).localPosition = new Vector2(posX, -childsParent.GetChild(i).GetComponent<RectTransform>().rect.size.y);
                    childsParent.GetChild(i).localPosition = new Vector2(posX, -childsParent.GetChild(i).GetComponent<RectTransform>().sizeDelta.y);

                }
			}

            //childsParent.GetChild(i).GetComponent<RectTransform>().pivot = childsParent.GetChild(i).GetComponent<RectTransform>().rect.size / 2f;
            childsParent.GetChild(i).GetComponent<RectTransform>().pivot = size / 2f;
            float targetScale = 1f - (scale_strength * Mathf.Abs(i - selectedIndex));
			targetScale = Mathf.Clamp(targetScale, scale_min, 1f);
			childsParent.GetChild(i).localScale = Vector2.Lerp(childsParent.GetChild(i).localScale, Vector2.one * targetScale, smoothing_speed * Time.deltaTime);

			// Truc pour l'opacity mais voir le component qu'on va ajouter
			/*
			var target_opacity = 1.0 - (opacity_strength * abs(i.get_index()-selected_index))
			target_opacity = clamp(target_opacity, 0.0, 1.0)
			i.modulate.a = lerp(i.modulate.a, target_opacity, smoothing_speed*delta)
			*/

			/*
			 
			if (i.get_index() == selected_index):
				if (change_z_index): i.z_index = base_z_index + 1
				i.mouse_filter = Control.MOUSE_FILTER_STOP
				i.focus_mode = Control.FOCUS_ALL
			else:
				if (change_z_index): i.z_index = base_z_index -abs(i.get_index()-selected_index)
				i.mouse_filter = Control.MOUSE_FILTER_IGNORE
				i.focus_mode = Control.FOCUS_NONE
			 */

			if (wraparoudEnabled)
			{
				if (invertAxis)
					childsParent.localPosition = new Vector3(childsParent.localPosition.x, Mathf.Lerp(childsParent.localPosition.y, 0, smoothing_speed * Time.deltaTime), childsParent.localPosition.z);
				else
					childsParent.localPosition = new Vector3(Mathf.Lerp(childsParent.localPosition.x, 0, smoothing_speed * Time.deltaTime), childsParent.localPosition.y , childsParent.localPosition.z);
			}
			else
			{
				if (invertAxis)
                    //childsParent.localPosition = new Vector3(childsParent.localPosition.x, Mathf.Lerp(childsParent.localPosition.y, -(childsParent.GetChild(selectedIndex).localPosition.y + childsParent.GetChild(selectedIndex).GetComponent<RectTransform>().rect.size.y / 2f), smoothing_speed * Time.deltaTime), childsParent.localPosition.z);
                    childsParent.localPosition = new Vector3(childsParent.localPosition.x, Mathf.Lerp(childsParent.localPosition.y, -(childsParent.GetChild(selectedIndex).localPosition.y + childsParent.GetChild(selectedIndex).GetComponent<RectTransform>().sizeDelta.y / 2f), smoothing_speed * Time.deltaTime), childsParent.localPosition.z);
                else
                    //childsParent.localPosition = new Vector3(Mathf.Lerp(childsParent.localPosition.x, -(childsParent.GetChild(selectedIndex).localPosition.x + childsParent.GetChild(selectedIndex).GetComponent<RectTransform>().rect.size.x/2f), smoothing_speed * Time.deltaTime), childsParent.localPosition.y, childsParent.localPosition.z);
                    childsParent.localPosition = new Vector3(Mathf.Lerp(childsParent.localPosition.x, -(childsParent.GetChild(selectedIndex).localPosition.x + childsParent.GetChild(selectedIndex).GetComponent<RectTransform>().sizeDelta.x / 2f), smoothing_speed * Time.deltaTime), childsParent.localPosition.y, childsParent.localPosition.z);
            }

		}
	}

	public void Left()
	{
		selectedIndex -= 1;
		if (selectedIndex < 0)
		{
            selectedIndex = 0;
			if (turnAround) selectedIndex = childsParent.childCount - 1;
        }
    }

	public void Right()
	{
		selectedIndex += 1;
		if (selectedIndex > childsParent.childCount - 1)
		{
            selectedIndex = childsParent.childCount - 1;
			if (turnAround) selectedIndex = 0;
        }
    }

	public int GetSelectedIndex()
	{
		return selectedIndex;
	}

	public void DoPositions()
	{
        if (childsParent == null || childsParent.childCount == 0) return;

        selectedIndex = Mathf.Clamp(selectedIndex, 0, childsParent.childCount - 1);

        for (int i = 0; i < childsParent.childCount; i++)
        {
            if (wraparoudEnabled)
            {
                float maxIndexRange = Mathf.Max(1, (childsParent.childCount - 1) / 2f);
                float angle = Mathf.Clamp((i - selectedIndex) / maxIndexRange, -1f, 1f) * Mathf.PI;
                float x = 0;
                float y = 0;

                if (invertAxis)
                {
                    y = Mathf.Sin(angle) * wraparoudHeight;
                    x = Mathf.Cos(angle) * wraparoudRadius;
                }
                else
                {
                    x = Mathf.Sin(angle) * wraparoudHeight;
                    y = Mathf.Cos(angle) * wraparoudRadius;
                }

                Vector2 targetPos = Vector2.zero;

                if (invertAxis)
                    //targetPos = new Vector2(x, y - wraparoudHeight) - childsParent.GetChild(i).GetComponent<RectTransform>().rect.size / 2f;
                    targetPos = new Vector2(x, y - wraparoudHeight) - size / 2f;
                else
                    //targetPos = new Vector2(y, x - wraparoudHeight) - childsParent.GetChild(i).GetComponent<RectTransform>().rect.size / 2f;
                    targetPos = new Vector2(y, x - wraparoudHeight) - size / 2f;

                childsParent.GetChild(i).localPosition = targetPos;
            }
            else
            {
                if (invertAxis)
                {
                    float posY = 0;
                    if (i > 0)
                    {
                        //posY = childsParent.GetChild(i - 1).localPosition.y + childsParent.GetChild(i - 1).GetComponent<RectTransform>().rect.size.y + spacing;
                        posY = childsParent.GetChild(i - 1).localPosition.y + size.y + spacing;
                    }

                    //childsParent.GetChild(i).localPosition = new Vector2(-childsParent.GetChild(i).GetComponent<RectTransform>().rect.size.x, posY);
                    childsParent.GetChild(i).localPosition = new Vector2(-size.y, posY);

                }
                else
                {
                    float posX = 0;
                    if (i > 0)
                    {
                        //posX = childsParent.GetChild(i - 1).localPosition.x + childsParent.GetChild(i - 1).GetComponent<RectTransform>().rect.size.x + spacing;
                        posX = childsParent.GetChild(i - 1).localPosition.x + size.x + spacing;
                    }

                    //childsParent.GetChild(i).localPosition = new Vector2(posX, -childsParent.GetChild(i).GetComponent<RectTransform>().rect.size.y);
                    childsParent.GetChild(i).localPosition = new Vector2(posX, -childsParent.GetChild(i).GetComponent<RectTransform>().sizeDelta.y);

                }
            }

            //childsParent.GetChild(i).GetComponent<RectTransform>().pivot = childsParent.GetChild(i).GetComponent<RectTransform>().rect.size / 2f;
            childsParent.GetChild(i).GetComponent<RectTransform>().pivot = size / 2f;
            float targetScale = 1f - (scale_strength * Mathf.Abs(i - selectedIndex));
            targetScale = Mathf.Clamp(targetScale, scale_min, 1f);
            childsParent.GetChild(i).localScale = Vector2.one * targetScale;

            // Truc pour l'opacity mais voir le component qu'on va ajouter
            /*
			var target_opacity = 1.0 - (opacity_strength * abs(i.get_index()-selected_index))
			target_opacity = clamp(target_opacity, 0.0, 1.0)
			i.modulate.a = lerp(i.modulate.a, target_opacity, smoothing_speed*delta)
			*/

            /*
			 
			if (i.get_index() == selected_index):
				if (change_z_index): i.z_index = base_z_index + 1
				i.mouse_filter = Control.MOUSE_FILTER_STOP
				i.focus_mode = Control.FOCUS_ALL
			else:
				if (change_z_index): i.z_index = base_z_index -abs(i.get_index()-selected_index)
				i.mouse_filter = Control.MOUSE_FILTER_IGNORE
				i.focus_mode = Control.FOCUS_NONE
			 */

            if (wraparoudEnabled)
            {
                if (invertAxis)
                    childsParent.localPosition = new Vector3(childsParent.localPosition.x, 0, childsParent.localPosition.z);
                else
                    childsParent.localPosition = new Vector3(0, childsParent.localPosition.y, childsParent.localPosition.z);
            }
            else
            {
                if (invertAxis)
                    //childsParent.localPosition = new Vector3(childsParent.localPosition.x, Mathf.Lerp(childsParent.localPosition.y, -(childsParent.GetChild(selectedIndex).localPosition.y + childsParent.GetChild(selectedIndex).GetComponent<RectTransform>().rect.size.y / 2f), smoothing_speed * Time.deltaTime), childsParent.localPosition.z);
                    childsParent.localPosition = new Vector3(childsParent.localPosition.x, -(childsParent.GetChild(selectedIndex).localPosition.y + childsParent.GetChild(selectedIndex).GetComponent<RectTransform>().sizeDelta.y / 2f), childsParent.localPosition.z);
                else
                    //childsParent.localPosition = new Vector3(Mathf.Lerp(childsParent.localPosition.x, -(childsParent.GetChild(selectedIndex).localPosition.x + childsParent.GetChild(selectedIndex).GetComponent<RectTransform>().rect.size.x/2f), smoothing_speed * Time.deltaTime), childsParent.localPosition.y, childsParent.localPosition.z);
                    childsParent.localPosition = new Vector3(-(childsParent.GetChild(selectedIndex).localPosition.x + childsParent.GetChild(selectedIndex).GetComponent<RectTransform>().sizeDelta.x / 2f), childsParent.localPosition.y, childsParent.localPosition.z);
            }

        }
    }
}
