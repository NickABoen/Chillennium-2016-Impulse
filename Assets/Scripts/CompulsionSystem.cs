﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CompulsionSystem : MonoBehaviour {
    private int action_size = 8;
    private int object_size = 7;
    public enum enActions {None, Aligning, Sorting, Counting, Tapping, Touching, Multiples};
    public enum enObjects {None, Blocks, Circles, Switches, Buttons, Locks, Numbers};
    public enum enCycleState { Obsession, Anxiety, Compulsion, Relief };

    public GameObject block_prefab, circle_prefab, switch_prefab; //button_prefab, lock_prefab, number_prefab;

    public enActions current_action;
    public enObjects current_object;
    public enCycleState ocd_cycle;
    public int global_number;
    public int max_count;
    public double vertical_column_error_threshold;

    private List<enActions> action_set;
    private List<enObjects> object_set;
    private List<GameObject> object_list;
    private bool isPuzzleComplete = false;

	// Use this for initialization
	void Awake() {
        global_number = (global_number <= 0) ? 3 : global_number;
        max_count = (max_count <= 0) ? 10 : max_count;

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
        ocd_cycle = enCycleState.Obsession;
    }

    void FixedUpdate()
    {
        switch (ocd_cycle)
        {
            case enCycleState.Compulsion:
                isPuzzleComplete = CheckSolution();
                break;
        }
    }

	// Update is called once per frame
	void Update () {
        DraggingSystem input_system = gameObject.GetComponent<DraggingSystem>();
        AnxietySystem anxiety_system = gameObject.GetComponent<AnxietySystem>();
        switch (ocd_cycle)
        {
            case enCycleState.Obsession:
                if(object_list != null)
                {
                    foreach(GameObject gob in object_list)
                    {
                        Destroy(gob);
                    }
                }
                object_list = Build_Compulsion();
                input_system.ResetInteracted();
                ocd_cycle = enCycleState.Anxiety;
                break;
            case enCycleState.Anxiety:
                if (!input_system.PlayerHasInteracted())
                {
                    anxiety_system.ReduceAnxiety();
                }
                else
                {
                    ocd_cycle = enCycleState.Compulsion;
                }
                break;
            case enCycleState.Compulsion:
                anxiety_system.RaiseAnxiety(isPuzzleComplete);
                //TODO: Needs a way of rolling over to relief with a UI element
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ocd_cycle = enCycleState.Relief;
                }
                break;
            case enCycleState.Relief:
                if (anxiety_system.TickRelief())
                {
                    ocd_cycle = enCycleState.Obsession;
                }
                break;
        }
	}

    void Pick_Random_State()
    {
        current_action = action_set[Random.Range(0, action_set.Count)];
        current_object = object_set[Random.Range(0, object_set.Count)];

        //Increase random weights
        action_set.Add(current_action);
        object_set.Add(current_object);
    }

    public List<GameObject> Build_Compulsion()
    {
        if(current_action == enActions.None || current_object == enObjects.None)
            Pick_Random_State();

        switch (current_object)
        {
            case enObjects.Blocks:
                return Build_Blocks();
            case enObjects.Circles:
                return Build_Circles();
            case enObjects.Switches:
                return Build_Switches();
            case enObjects.Buttons:
                return Build_Buttons();
            case enObjects.Locks:
                return Build_Locks();
            case enObjects.Numbers:
                return Build_Numbers();
        }
        return null;
    }

    public List<GameObject> Build_Blocks()
    {
        List<GameObject> objects = new List<GameObject>();
        Vector2 block_size = block_prefab.transform.localScale;
        switch (current_action)
        {
            case enActions.Aligning:
                for(int i = 0; i < global_number; i++)
                {
                    float x = Random.Range(0.0f, 1.0f) - 0.5f;
                    float y = i * block_size.y;
                    objects.Add(Spawn_Prefab(block_prefab, new Vector2(x, y)));
                }
                break;
            case enActions.Sorting:
                List<Color> colors = new List<Color>();
                colors.Add(Color.cyan);
                colors.Add(Color.magenta);
                colors.Add(Color.yellow);
                for(int i = 0; i < global_number; i++)
                {
                    float x = Random.Range(0.0f, 1.0f) - 0.5f;
                    float y = i * block_size.y;
                    GameObject block = Spawn_Prefab(block_prefab, new Vector2(x, y));
                    block.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Count)];
                    objects.Add(block);
                }
                break;
            //case enActions.Counting:
            case enActions.Tapping:
            case enActions.Touching:
                //TODO: Spawn prefab/prompt for number entry
                int effective_max = Random.Range((int)(max_count / 2), max_count);
                for(int i = 0; i < effective_max; i++)
                {
                    float x = Random.Range(0.0f, 1.0f) - 0.5f;
                    float y = i * block_size.y;
                    objects.Add(Spawn_Prefab(block_prefab, new Vector2(x, y)));
                }
                break;
        }

        return objects;
    }

    public List<GameObject> Build_Circles()
    {
        List<GameObject> objects = new List<GameObject>();
        Vector2 circle_size = circle_prefab.transform.localScale;
        int dim_size = Random.Range(2, 5);
        switch (current_action)
        {
            case enActions.Aligning:
                for(int i = 0; i < dim_size; i++)
                {
                    for(int j = 0; j < dim_size; j++)
                    {
                        float x = i * (circle_size.x) + (Random.value - 0.5f);
                        float y = j * (circle_size.y) - 3 + (Random.value - 0.5f);
                        objects.Add(Spawn_Prefab(circle_prefab, new Vector2(x, y)));
                    }
                }
                break;
            case enActions.Sorting:
                List<Color> colors = new List<Color>();
                colors.Add(Color.cyan);
                colors.Add(Color.magenta);
                colors.Add(Color.yellow);
                dim_size = Mathf.Clamp(dim_size, 2, 4);
                for(int i = 0; i < dim_size; i++)
                {
                    for(int j = 0; j < dim_size; j++)
                    {
                        float x = i * (circle_size.x);
                        float y = j * (circle_size.y)-3;
                        GameObject circle = Spawn_Prefab(circle_prefab, new Vector2(x, y));
                        circle.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, 3)];
                        objects.Add(circle);
                    }
                }
                break;
            case enActions.Counting:
            case enActions.Tapping:
            case enActions.Touching:
                int count_amount = Random.Range(15, 30);
                for(int i = 0; i < count_amount; i++)
                {
                    float x = Random.Range(0.0f, 4.0f) - 2.0f;
                    float y = Random.Range(0.0f, 3.0f) - 2.0f;
                    objects.Add(Spawn_Prefab(circle_prefab, new Vector2(x, y)));
                }
                break;
        }
        return objects;
    }

    public List<GameObject> Build_Switches()
    {
        List<GameObject> objects = new List<GameObject>();
        Vector2 switch_size = switch_prefab.transform.localScale;
        switch (current_action)
        {
            case enActions.Sorting:
            case enActions.Counting:
                {
                    current_action = enActions.Counting;
                    current_action = enActions.Counting;
                    int count_amount = Random.Range(15, 30);
                    for (int i = 0; i < count_amount; i++)
                    {
                        float x = Random.Range(0.0f, 4.0f) - 2.0f;
                        float y = Random.Range(0.0f, 3.0f) - 2.0f;
                        GameObject switch_component = Spawn_Prefab(switch_prefab, new Vector2(x, y));
                        switch_component.GetComponent<SwitchComponent>().isOn = Random.value >= 0.5 ? true : false;
                        objects.Add(switch_component);
                    }
                }
                break;
            case enActions.Aligning:
            case enActions.Tapping:
            case enActions.Touching:
                int touch_amount_sq = Random.Range(3, 5);
                for (int i = 0; i < touch_amount_sq; i++) {
                    for(int j = 0; j < touch_amount_sq; j++)
                    {
                        float x = (i * (switch_size.x)) / 2;
                        float y = ((j * (switch_size.y)) / 2) - 3;
                        GameObject switch_component = Spawn_Prefab(switch_prefab, new Vector2(x, y));
                        switch_component.GetComponent<SwitchComponent>().isOn = Random.value >= 0.5 ? true : false;
                        objects.Add(switch_component);
                    }
                }
                break;
        }
        return objects;
    }

    public List<GameObject> Build_Buttons()
    {
        List<GameObject> objects = new List<GameObject>();
        switch (current_action)
        {
            case enActions.Aligning:
                break;
            case enActions.Sorting:
                break;
            case enActions.Counting:
                break;
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
        return objects;
    }

    public List<GameObject> Build_Locks()
    {
        List<GameObject> objects = new List<GameObject>();
        switch (current_action)
        {
            case enActions.Aligning:
                break;
            case enActions.Sorting:
                break;
            case enActions.Counting:
                break;
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
        return objects;
    }

    public List<GameObject> Build_Numbers()
    {
        List<GameObject> objects = new List<GameObject>();
        switch (current_action)
        {
            case enActions.Aligning:
                break;
            case enActions.Sorting:
                break;
            case enActions.Counting:
                break;
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
        return objects;
    }

    public bool CheckSolution()
    {
        //return false;
        return CheckVerticalValidity(1);
    }

    bool CheckVerticalValidity(int max_column_count)
    {
        List<GameObject> x_sorted;
        if (object_list != null && object_list.Count >= 0) {
            x_sorted = object_list.OrderBy(x => x.transform.position.x).ToList();
        }
        else
        {
            return false;
        }
        int column_count = 0;
        float min_y = float.MaxValue;
        GameObject previous = null;
        foreach(GameObject gob in x_sorted)
        {
            if (previous == null)
            {
                column_count++;
                previous = gob;
                continue;
            }
            else {
                float gob_x = gob.transform.position.x;
                float prev_x = previous.transform.position.x;

                if (Mathf.Abs(gob_x - prev_x) > vertical_column_error_threshold)
                {
                    //Is a new column
                    column_count++;
                }
            }

            min_y = min_y < gob.transform.position.y ? min_y : gob.transform.position.y;

            previous = gob;
        }
        //One cube should be on the ground
        if(column_count > max_column_count || min_y > (-4.5 + vertical_column_error_threshold))
        {
            return false; //too high
        }
        Debug.Log("column count = " + column_count);

        return false;
    }

    GameObject Spawn_Prefab(GameObject prefab, Vector2 position)
    {
        GameObject new_block = GameObject.Instantiate(prefab);
        new_block.transform.position = new Vector3(new_block.transform.position.x + position.x, 
                                                   new_block.transform.position.y + position.y, 
                                                   new_block.transform.position.z);
        return new_block;
    }

}

