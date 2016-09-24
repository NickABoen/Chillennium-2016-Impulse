using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompulsionSystem : MonoBehaviour {
    private int action_size = 8;
    private int object_size = 7;
    public enum enActions {None, Aligning, Sorting, Counting, Checking, Tapping, Touching, Multiples};
    public enum enObjects {None, Blocks, Circles, Switches, Buttons, Locks, Numbers};

    public GameObject block_prefab, circle_prefab, switch_prefab, button_prefab, lock_prefab, number_prefab;

    public enActions current_action;
    public enObjects current_object;
    public int global_number;

    private List<enActions> action_set;
    private List<enObjects> object_set;

	// Use this for initialization
	void Awake() {
        global_number = 3;

        action_set = new List<enActions>();
        object_set = new List<enObjects>();
        for (int i = 1; i < action_size; i++)
        {
            action_set.Add((enActions)i);
        }
        for (int i = 1; i < object_size; i++)
        {
            object_set.Add((enObjects)i);
        }
	}

    void Start()
    {
        Build_Compulsion();
    }

	// Update is called once per frame
	void Update () {
	}

    void Pick_Random_State()
    {
        current_action = action_set[Random.Range(0, action_set.Count)];
        current_object = object_set[Random.Range(0, object_set.Count)];

        //Increase random weights
        action_set.Add(current_action);
        object_set.Add(current_object);
    }

    public void Build_Compulsion()
    {
        if(current_action == enActions.None || current_object == enObjects.None)
            Pick_Random_State();

        switch (current_object)
        {
            case enObjects.Blocks:
                Build_Blocks();
                break;
            case enObjects.Circles:
                Build_Circles();
                break;
            case enObjects.Switches:
                Build_Switches();
                break;
            case enObjects.Buttons:
                Build_Buttons();
                break;
            case enObjects.Locks:
                Build_Locks();
                break;
            case enObjects.Numbers:
                Build_Numbers();
                break;
        }
    }

    public void Build_Blocks()
    {
        Vector2 block_size = block_prefab.transform.localScale;
        switch (current_action)
        {
            case enActions.Aligning:
                for(int i = 0; i < global_number; i++)
                {
                    GameObject new_block = GameObject.Instantiate(block_prefab);
                    new_block.transform.position = new Vector3(
                        //Random.Range(0, 2 * global_number) - global_number,
                        Random.Range(0.0f, 1.0f) - 0.5f,
                        new_block.transform.position.y + (i * block_size.y),
                        new_block.transform.position.z
                        );
                }
                break;
            case enActions.Sorting:
                break;
            case enActions.Counting:
                break;
            case enActions.Checking:
                break;
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
    }

    public void Build_Circles()
    {
        switch (current_action)
        {
            case enActions.Aligning:
                break;
            case enActions.Sorting:
                break;
            case enActions.Counting:
                break;
            case enActions.Checking:
                break;
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
    }

    public void Build_Switches()
    {
        switch (current_action)
        {
            case enActions.Aligning:
                break;
            case enActions.Sorting:
                break;
            case enActions.Counting:
                break;
            case enActions.Checking:
                break;
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
    }

    public void Build_Buttons()
    {
        switch (current_action)
        {
            case enActions.Aligning:
                break;
            case enActions.Sorting:
                break;
            case enActions.Counting:
                break;
            case enActions.Checking:
                break;
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
    }

    public void Build_Locks()
    {
        switch (current_action)
        {
            case enActions.Aligning:
                break;
            case enActions.Sorting:
                break;
            case enActions.Counting:
                break;
            case enActions.Checking:
                break;
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
    }

    public void Build_Numbers()
    {
        switch (current_action)
        {
            case enActions.Aligning:
                break;
            case enActions.Sorting:
                break;
            case enActions.Counting:
                break;
            case enActions.Checking:
                break;
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
    }

}
