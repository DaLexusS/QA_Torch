using UnityEngine;

public class Torch
{
    public bool IsLit {get; private set;}
    public bool IsPlayerNearby {get; private set;}
    
    public void SetPlayerNearby(bool isNearby)
    {
        IsPlayerNearby = isNearby;
    }

    public bool TryLight()
    {
        if (IsPlayerNearby)
        {
            if (!IsLit)
            {
                IsLit = true;
            }
            else
            {
                IsLit = false;
            }

            return true;
        }
        
        return false;
    }
}
