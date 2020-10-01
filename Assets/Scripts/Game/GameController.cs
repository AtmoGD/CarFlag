using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public FlagController flag;
    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }else
        {
            instance = this;
        }
    }

    public List<CarAgent> cars = new List<CarAgent>();

    public void EndGame(CartController winningCar)
    {
        foreach(CartController car in instance.cars)
        {
            if (car != winningCar)
            {
                if (car.GetTeam() == winningCar.GetTeam())
                {
                    car.gameObject.SendMessage("AddReward", 0.3f);

                }
                else
                {
                    car.gameObject.SendMessage("AddReward", -1f);
                }
            }

            car.EndEpisode();
        }
    }
}
