using UnityEngine;
using System.Collections;

public class SettlementController : MonoBehaviour {

	public int minFriendlySettlements;
	public int maxFriendlySettlements;
	public int minEnemySettlements;
	public int maxEnemySettlements;
	public int minDistanceBetweenSettlements;
	public int scale;

	public GameObject terrain;
	public GameObject friendlySettlementPrefab;
//	public  enemySettlementPrefab;

	private float boundsMinX;
	private float boundsMaxX;
	private float boundsMinY;
	private float boundsMaxY;

	private int numFriendlyAdded;

	private bool isEmpty;

	private Vector3[] settlementCentersF;
	private Vector3[] settlementCentersE;

	// Use this for initialization
	void Start ()
	{
		isEmpty = true;
		boundsMaxX = terrain.GetComponent<MeshFilter>().mesh.bounds.size.x * scale;
		boundsMinX = -boundsMaxX;
		boundsMinY = terrain.GetComponent<MeshFilter>().mesh.bounds.size.z * scale;
		boundsMaxY = -boundsMinY;

		int numFriendly = Random.Range (minFriendlySettlements, maxFriendlySettlements);
//		int numEnemy = Random.Range (minEnemySettlements, maxEnemySettlements);
		settlementCentersF = new Vector3[numFriendly];

		for (int i = 0; i < 10; i++) {
			GenerateSettlement(0);
		}

//		transform.Translate(new Vector3(boundsMinX/2,1,boundsMinY/2));
//		print(transform.position.x);
//		print(transform.position.z);

//		for (int j = 0; j < numEnemy; j++) {
//			GenerateSettlement(1);
//		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// If type == 0 --> Friendly
	// If type == 1 --> Enemy
	// If type == 2 --> Abandoned
	// If type == 3 --> Empty
	void GenerateSettlement (int type)
	{

		int minOrMaxX = Random.Range (0, 4);		// If 0, min. If 1, max
		int minOrMaxY = Random.Range (0, 4);		// If 0, min. If 1, max
//			int settlementX = Random.Range(0,1);

		float settlementX = (minOrMaxX % 2 == 0) ? Random.Range ((boundsMinX/2)+18, (boundsMaxX / 2)-18) : Random.Range ((boundsMinX/2)+18, (boundsMaxX/2) - 18);
		float settlementY = (minOrMaxY % 2 == 0) ? Random.Range ((boundsMinY/2)+18, (boundsMaxY / 2)-18) : Random.Range ((boundsMinY / 2)+18, (boundsMaxY)/2 - 18);


//		float settlementX =  Random.Range (0, (boundsMaxX/2) - 18);
//		float settlementY =  Random.Range (0, (boundsMaxY/2) - 18);


		if (type == 0) {
			Vector3 possiblePosition = new Vector3 (settlementX, 3,settlementY);
//			if (!PositionIsValid (possiblePosition)) {
//				settlementX = (minOrMaxX % 2 == 0) ? Random.Range (18, (boundsMaxX / 2)) : Random.Range ((boundsMaxX / 2), boundsMaxX - 18);
//				settlementY = (minOrMaxY % 2 == 0) ? Random.Range (18, (boundsMaxY / 2)) : Random.Range ((boundsMaxY / 2), boundsMaxY - 18);
//				possiblePosition = new Vector3 (settlementX, -100, settlementY);
//				print("Not valid");
//			}
			GameObject s = Instantiate(friendlySettlementPrefab,possiblePosition,Quaternion.identity) as GameObject;
			s.transform.SetParent(gameObject.transform);
		}

	}

	bool PositionIsValid (Vector3 possiblePosition)
	{
		if (isEmpty) {
//			print("OKAY!!");
			numFriendlyAdded++;
			settlementCentersF [0] = possiblePosition;
			isEmpty = false;
			return true;
		}
//		if (settlementCentersF[0] == null) {
//			
//		}
		foreach (Vector3 pos in settlementCentersF) {
//			print(pos - possiblePosition);
			if (Mathf.Abs((pos - possiblePosition).magnitude) <= (minDistanceBetweenSettlements)) {
//				print("OKAY!!");
				settlementCentersF [numFriendlyAdded+1] = possiblePosition;
				numFriendlyAdded++;
				return true;
			}
		}
		return false;
	}
}
