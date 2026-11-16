using UnityEngine;

public class TDSPlayerController : MonoBehaviour
{
   
    void Awake()
    {
        
    }

    
    void Update()
    {
    
      HandleShooting();

    }
        private void HandleShooting()
        {
          if(Input.GetMouseButtonDown(0))
          {
            //CurrentWeapon.Shoot();
          }
     
        } 
}

