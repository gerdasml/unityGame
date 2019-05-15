using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CubesHandler : MonoBehaviour
{
    private List<string> answers;
    private List<string> questions;
    public List<GameObject> cubes;
    public List<GameObject> tables;
    public TextMeshPro wordsBoardTask;
    public List<GameObject> lockedObjects;
    public Inventory inventory;

    void Start()
    {
        questions = Config.Instance.Puzzle.Cubes.Questions;
        answers = Config.Instance.Puzzle.Cubes.Answers;
        wordsBoardTask.text = Config.Instance.Puzzle.Cubes.WordsBoardTask;
        cubes.Shuffle();
        tables.Shuffle();

        for (int i = 0; i < 5; i++)
        {
            tables[i].GetComponentInChildren<TableCollider>().ExpectedCube = cubes[i];
            cubes[i].GetComponentsInChildren<TextMeshPro>()
                    .ToList()
                    .ForEach(textMesh => textMesh.text = answers[i]);
            tables[i].GetComponentsInChildren<TextMeshPro>()
                     .ToList()
                     .ForEach(textMesh => textMesh.text = questions[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int cnt = 0;
        for(int i = 0; i < 5; i++)
        {
            var x = tables[i].GetComponentsInChildren<Collider>().ToList().Where(c => c.gameObject.tag == "TableTop").FirstOrDefault();
            var y = cubes[i].GetComponent<Collider>();
            if (tables[i].GetComponentInChildren<TableCollider>().IsExpectedCubeTouching)
            {
                cnt++;
            }
        }
        if(cnt == 5)
        {
            List<InteractableObject> dependencies = cubes[0].GetComponent<InteractableObject>().dependencies;
            foreach (var d in dependencies)
            {
                inventory.RemoveItem(d);
            }
            for (int i = 0; i < lockedObjects.Count; i++)
            {
                lockedObjects[i].SetActive(true);
            }
            for (int i = 0; i < 5; i++)
            {
                Destroy(tables[i].GetComponent<InteractableObject>());
                Destroy(cubes[i].GetComponent<InteractableObject>());
                Destroy(tables[i]);
                Destroy(cubes[i]);
                Destroy(this);
            }
        }
    }
}
