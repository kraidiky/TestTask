using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

// Скрипт навешиваем на камеру. Логично, ей же нужно выстраивать всё относительно камеры.
public class CubesOrderer : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	private List<CubeContainer> containers = new List<CubeContainer> ();

	public void InspectAndDecorateCubes () {
		// В сцене найти кубики, и проверить не были ли они у нас уже раньше.
		var newContainers = GameObject.FindObjectsOfType<BoxCollider>().Cast<BoxCollider>().SkipWhile(
				box => containers.Any (container => container.Box == box)
			).Select(box => new CubeContainer(){Box = box});
		// Всё, новые элементы нашли, теперь пора с ними что-нибудь делать.

			
	}
	public void CreateRandomBox() {
		GameObject = new GameObject();


	}

	void OnGUI ()
	{
			//GUI.Label (new Rect (10, 60, 100, 50), "Scores: ");
			if (GUI.Button (new Rect (10, 10, 250, 24), "Создать новый случайный кубик")) {
					// TODO
			}
			if (GUI.Button (new Rect (10, 40, 200, 24), "Поискать новые кубики")) {
					InspectAndDecorateCubes ();
			}
	}
}

struct CubeContainer
{
	public GameObject Target;
	public BoxCollider Box;
	public Vector3 SelectedPosition;
}