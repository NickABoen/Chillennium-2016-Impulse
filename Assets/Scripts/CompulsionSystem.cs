using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompulsionSystem : MonoBehaviour {
    private int action_size = 8;
    private int object_size = 7;
    public enum enActions {None, Aligning, Sorting, Counting, Tapping, Touching, Multiples};
    public enum enObjects {None, Blocks, Circles, Switches, Buttons, Locks, Numbers};

    public GameObject block_prefab, circle_prefab, switch_prefab, button_prefab, lock_prefab, number_prefab;

    public enActions current_action;
    public enObjects current_object;
    public int global_number;
    public int max_count;

    private List<enActions> action_set;
    private List<enObjects> object_set;

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
                    float x = Random.Range(0.0f, 1.0f) - 0.5f;
                    float y = i * block_size.y;
                    Spawn_Prefab(block_prefab, new Vector2(x, y));
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
                }
                break;
            case enActions.Counting:
            case enActions.Tapping:
            case enActions.Touching:
                //TODO: Spawn prefab/prompt for number entry
                int effective_max = Random.Range((int)(max_count / 2), max_count);
                for(int i = 0; i < effective_max; i++)
                {
                    float x = Random.Range(0.0f, 1.0f) - 0.5f;
                    float y = i * block_size.y;
                    Spawn_Prefab(block_prefab, new Vector2(x, y));
                }
                break;
        }
    }

    public void Build_Circles()
    {
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
                        Spawn_Prefab(circle_prefab, new Vector2(x, y));
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
                    Spawn_Prefab(circle_prefab, new Vector2(x, y));
                }
                break;
        }
    }

    public void Build_Switches()
    {
        switch (current_action)
        {
            case enActions.Aligning:
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
                    }
                }
                break;
            case enActions.Tapping:
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
            case enActions.Tapping:
                break;
            case enActions.Touching:
                break;
            case enActions.Multiples:
                break;
        }
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

