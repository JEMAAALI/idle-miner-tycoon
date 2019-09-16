using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _verticalLimit;
    [SerializeField] private GameObject _tooltip;
	public GameObject _bottomBar;

    private const float TooltipDelay = 3f;

    private bool _tooltipActive = true;

    private void Update()
    {
        var yInput = Input.GetAxis("Vertical");

		//Add a mobile touch input to the script
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Touch touch = Input.touches[0];
			yInput= touch.deltaPosition.y;
		}
		////////////////////////////////////

	
        if (Mathf.Abs(yInput) > 0)
        {
            if (_tooltipActive)
            {
                StartCoroutine(DisableTooltip());
                _tooltipActive = false;
            }

            var yPosition = transform.position.y;
            yPosition += yInput * _speed * Time.deltaTime;
            yPosition = Mathf.Clamp(yPosition, _verticalLimit.x, _verticalLimit.y);

            transform.position = new Vector2(0, yPosition);
        }
    }

    private IEnumerator DisableTooltip()
    {
        yield return new WaitForSeconds(TooltipDelay);
        _tooltip.SetActive(false);
		_bottomBar.SetActive(true);
    }
}