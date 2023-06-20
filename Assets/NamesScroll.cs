using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamesScroll : MonoBehaviour
{
    public GameObject xiao;
    public GameObject arnau;
    public GameObject uri;
    public GameObject xavi;
    public GameObject david;

    public bool Bxiao = true;
    public bool Barnau = true;
    public bool Buri = true;
    public bool Bxavi = true;
    public bool Bdavid = true;

    public GameObject jefe;
    public GameObject final;

    [SerializeField]
    private SlowMotion slowmoScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Mentre no arribi al final
        if (Vector3.Distance(david.transform.position, final.transform.position) < 0.01f)
        {
            this.transform.position = Vector2.MoveTowards(david.transform.position, final.transform.position, Time.deltaTime);

            if (Vector3.Distance(xiao.transform.position, final.transform.position) < 0.01f && Bxiao)
            {
                Bxiao = false;
                slowmoScript.StartDamageSlowMo(1.5f);
            }
            if (Vector3.Distance(arnau.transform.position, final.transform.position) < 0.01f && Barnau)
            {
                Barnau = false;
                slowmoScript.StartDamageSlowMo(1.5f);
            }
            if (Vector3.Distance(uri.transform.position, final.transform.position) < 0.01f && Buri)
            {
                Buri = false;
                slowmoScript.StartDamageSlowMo(1.5f);
            }
            if (Vector3.Distance(xavi.transform.position, final.transform.position) < 0.01f && Bxavi)
            {
                Bxiao = false;
                slowmoScript.StartDamageSlowMo(1.5f);
            }
            if (Vector3.Distance(david.transform.position, final.transform.position) < 0.01f && Bdavid)
            {
                Bdavid = false;
                slowmoScript.StartDamageSlowMo(1.5f);
            }

        }
    }
}
