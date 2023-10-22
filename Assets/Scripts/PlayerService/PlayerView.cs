using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PlayerView : MonoBehaviour
{

    //x_ускорение = x_0 + v_0 * t + (0.5 * a * t^2)
    [SerializeField] float speed;
    [SerializeField] float acl;
    [SerializeField] float period;
    float curtime;


    public void Update()
    {
        curtime += Time.deltaTime;
        if (curtime < period / 2)
        {
            transform.position = new(transform.position.x, transform.position.y + speed * Time.deltaTime + (acl * Time.deltaTime), 0);
            acl += Time.deltaTime;
        }
        else
        {
            transform.position = new(transform.position.x, transform.position.y + speed * Time.deltaTime - (acl * Time.deltaTime), 0);
            acl -= Time.deltaTime;
        }
    }

}
