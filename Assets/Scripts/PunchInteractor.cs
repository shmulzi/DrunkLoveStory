using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchInteractor : MonoBehaviour, IInteractor {

    private const float INPUT_TOLERANCE = 0.1f;

    public int chargeSpeed;
    public int maxCharge;
    public float chargeEffectMultiplier;

    private int _charge;
    private Vector2 _invertedDirection;
    private Vector2 _lastInput;
    private bool _shooting;

    private Vector2 _input;
    public Vector2 Input
    {
        set
        {
            _input = value;
        }
    }

	// Use this for initialization
	void Start () {
        _lastInput = Vector2.zero;
        _charge = 0;
	}
	
	// Update is called once per frame
	void Update () {
       
		if(_input.x < INPUT_TOLERANCE && _input.x > INPUT_TOLERANCE*-1 && _input.y < INPUT_TOLERANCE && _input.y > INPUT_TOLERANCE * -1)
        {
            if (_charge != 0)
            {
                _shooting = true;
                RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 0.5f, _lastInput * -1, 2f);
                if(hits != null && hits.Length > 0)
                {
                    foreach(RaycastHit2D hit in hits)
                    {
                        if(hit.transform != this.transform)
                        {
                            InteractionHandler interactionHandler = hit.transform.GetComponent<InteractionHandler>();
                            if (interactionHandler != null)
                            {
                                interactionHandler.GetPunched(_charge*chargeEffectMultiplier);
                            }
                               
                        }
                    }
                }
                _charge = 0;
            }
        }
        else
        {
            _lastInput = _input;
            if (_charge == 0)
            {
                _charge += chargeSpeed;
                StartCoroutine(ChargeShot());
            }
        }
	}

    private IEnumerator ChargeShot()
    {
        yield return new WaitForSeconds(1f);
        if (!_shooting)
        {
            if (_charge < maxCharge)
                _charge += chargeSpeed;
            if (_input != Vector2.zero)
                yield return StartCoroutine(ChargeShot());
        }
        else
        {
            _shooting = false;
        }
    }
}
