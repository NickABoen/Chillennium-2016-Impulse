using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompulsionSystem : MonoBehaviour {
    private int action_size = 8;
    private int object_size = 7;
    public enum enActions {None, Aligning, Sorting, Counting, Checking, Tapping, Touching, Multiples};
    public enum enObjects {None, Blocks, Circles, Switches, Buttons, Locks, Numbers};

    public Object block_prefab, circle_prefab, switch_prefab, button_prefab, lock_prefab, number_prefab;

    public enActions current_action;
    public enObjects current_object;

    private List<enActions> action_set;
    private List<enObjects> object_set;

	// Use this for initialization
	void Awake() {
        current_action = enActions.None;
        current_object = enObjects.None;

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
        Pick_Random_State();

        switch (current_object)
        {
            case enObjects.Blocks:
                break;
            case enObjects.Circles:
                break;
            case enObjects.Switches:
                break;
            case enObjects.Buttons:
                break;
            case enObjects.Locks:
                break;
            case enObjects.Numbers:
                break;
        }
    }

    public void Build_Blocks()
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
