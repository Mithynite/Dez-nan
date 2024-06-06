using UnityEngine;

// TODO Tato třída slouží k možnosti prohození zbraní za pomocí kolečka myši
public class WeaponHolder : MonoBehaviour
{
    private int selectedWeapon; // TODO Uložení "čísla" zbraně

    void Start()
    {
        SelectWeapon();
    }
    void Update()
    {
        int previousWeapon = selectedWeapon; // TODO Uložení předešlé zbraně 
        // TODO Prohození zbraně "nahoru"
       if(Input.GetAxis("Mouse ScrollWheel") > 0f)
       {
        if(selectedWeapon >= transform.childCount - 1) // TODO Kontrola, zda je vybraná zbraň poslední v pořadí
        {
            selectedWeapon = 0; // TODO Nastavení vybrané zbraně na první v pořadí
        }else
        {
            selectedWeapon++;
        }       
       }
       // TODO Prohození zbraně "dolů"
       if(Input.GetAxis("Mouse ScrollWheel") < 0f)
       {
           if(selectedWeapon <= 0) // TODO Kontrola, zda je vybraná zbraně první v pořadí
           {
               selectedWeapon = transform.childCount - 1; // TODO Nastavení vybrané zbraně na poslední v pořadí
           }else
           {
               selectedWeapon--; // TODO Snížení čísla vybrané zbraně
           }       
       }
       // TODO Aktivace/deaktivace zbraní v závislosti na vybrané zbrani
       if(previousWeapon != selectedWeapon)
       {
           SelectWeapon();
       }        
    }
    private void SelectWeapon()
    {
        int x = 0; // TODO Pomocná proměnná pro iteraci přes zbraně
        foreach(Transform weapon in transform) // TODO Procházení všech potomků objektu, na kterém je script (Gun Position)
        {
            if (x == selectedWeapon) // TODO Kontrola, zda se jedná o aktuálně vybranou zbraň
            {
                weapon.gameObject.SetActive(true); // TODO Aktivace objektu zbraně
            }else
            {
                weapon.gameObject.SetActive(false); // TODO Deaktivace objektu zbraně
            }
            x++;
        }
    }
}
