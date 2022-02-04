using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Coin", menuName = "Coin")]
public class CoinObject : ScriptableObject
{
    public int score;

    public Texture texture;
}
