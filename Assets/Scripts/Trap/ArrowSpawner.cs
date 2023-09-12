using UnityEngine;

namespace Trap
{
	public class ArrowSpawner : MonoBehaviour
	{
		[SerializeField]
		private GameObject arrow_obj;

		[SerializeField]
		private float coolTime;

		private void Start()
		{
			InvokeRepeating("SpawnArrow", 0f, coolTime);
		}

		private void SpawnArrow()
		{
			Instantiate(arrow_obj, transform.position, Quaternion.identity);
		}
	}
}