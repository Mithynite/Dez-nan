using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
   
    public int selectedWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousWeapon = selectedWeapon;
       if(Input.GetAxis("Mouse ScrollWheel") > 0f)
       {
        if(selectedWeapon >= transform.childCount - 1) //childCount = kolik má dětí vrchní object
            selectedWeapon = 0;
            else
                selectedWeapon++;
       }
       if(Input.GetAxis("Mouse ScrollWheel") < 0f)
       {
       if(selectedWeapon <= 0)
            selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
       }
       if(previousWeapon != selectedWeapon)
                SelectWeapon();
        
    }
    private void SelectWeapon()
    {
        int x = 0;
        foreach(Transform weapon in transform)
        {
            if(x==selectedWeapon)
                weapon.gameObject.SetActive(true);
                else
                    weapon.gameObject.SetActive(false);
            x++;
        }
    }
}
