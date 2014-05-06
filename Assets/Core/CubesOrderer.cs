using UnityEngine;
using System;
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
		var newContainers = GameObject.FindObjectsOfType<BoxCollider>().Cast<BoxCollider>().Where(box => !containers.Any (container => container.Box == box))
			.Select(box => new CubeContainer(){
				Box = box,
				Size = box.transform.lossyScale.magnitude
			});
		if (newContainers.Count()>0) {
			iTween.Stop();
			// Посчитать сколько места все эти кубики вместе будут занимать на прямой линии. Каждый кубик имеет по краям ещё по 0.5 от своего диагонального размера
			var newOrder = containers.Concat(newContainers).OrderBy(container=>container.Size); // Отсортируем по размерам
			float total = newOrder.Select(container=>container.Size*1.1f).Sum();
			float distance = Mathf.Max(10, (total/2)/Mathf.Tan(Mathf.PI/6));// Хочу чтобы все кубики помещались в 60 градусов
			Vector3 center = transform.position + transform.forward*distance;  // Точка перед камерой на выбраной дистанции
			var pos = center - transform.right*(total/2); // С этой точки начинаем расставлять.
			foreach (var container in newOrder) {
				pos += 0.55f*container.Size*transform.right;
				container.SelectedPosition = pos; // Поставить кубик ан нужное место.
				iTween.MoveTo(container.Box.gameObject, iTween.Hash("x", pos.x, "y", pos.y, "z", pos.z, "easeType", iTween.EaseType.easeInOutExpo, "time", 1f));  // Сразу после назначения нового места кубик начинет лететь.
				pos += 0.55f*container.Size*transform.right;
			}
			containers = newOrder.ToList(); // И закончили управжнения
		}
	}

	public GameObject PrefabToCreate;

	public void CreateRandomBox() {
		GameObject item = (GameObject) Instantiate(PrefabToCreate, UnityEngine.Random.insideUnitSphere * 5, UnityEngine.Random.rotation);
		float size = UnityEngine.Random.Range(0.5f, 2f);
		item.transform.localScale = new Vector3(size, size, size);
	}

	void OnGUI ()
	{
		//GUI.Label (new Rect (10, 60, 100, 50), "Scores: ");
		if (GUI.Button (new Rect (10, 10, 350, 24), "Поискать новые кубики на сцене и расставить")) {
			InspectAndDecorateCubes ();
		}
		if (GUI.Button (new Rect (10, 40, 250, 24), "Создать новый случайный кубик")) {
			CreateRandomBox();
		}
	}
}

class CubeContainer {
	public BoxCollider Box;
	public float Size; // Размер наибольшей диагонали
	public Vector3 SelectedPosition; // Сюда оно должно лететь
	public iTween Tween;
}