using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Machine", menuName = "new Machine")]
public class Machine : ScriptableObject {

	public Item input;
	public Item output;
	public GameObject preFab;
	public string blockName;
	public Quaternion Rotation;
}
