using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageSystem : MonoBehaviour {

    public GameObject text_prefab;
    public float random_spawn_range;
    public float minimum_time_between_spawns;

    public enum enAggressiveness { Low, Moderate, High, VeryHigh };

    public enAggressiveness aggressiveness = enAggressiveness.Low;
    public float x_min, x_max, y_min, y_max;
    public float min_speed, max_speed;
    public float percent_to_spawn;
    public float step_moderate, step_high, step_very_high;

    public float percent_of_general_word, percent_of_negative_word, percent_of_helpless_word;
    List<string> general_word_list = new List<string> { "SILLY", "NO", "MOVE ON", "JUST STOP", "WHY" };
    //List<string> instructional_word_list = new List<string> { "STRAIGHTER", "CROOKED", "BY COLOR", "GRID", "TOUCH", "TAP", "STACK" };
    List<string> negative_word_list = new List<string> { "EW", "STOP", "WRONG", "ALWAYS WRONG", "BAD", "DUMB", "STUPID", "WONT STOP", "CANT STOP", "PLEASE STOP", "FILTHY" };
    List<string> helpless_word_list = new List<string> { "please", "...", "just", "so tired", "go away", "leave me alone", "wait", "help" };

	// Use this for initialization
	void Start () {
	}

    float timer = 0;
	// Update is called once per frame
	void FixedUpdate () {
        float anxiety = gameObject.GetComponent<AnxietySystem>().anxiety_buildup;
        aggressiveness = enAggressiveness.Low;
        if (anxiety > step_moderate) aggressiveness = enAggressiveness.Moderate;
        if (anxiety > step_high) aggressiveness = enAggressiveness.High;
        if (anxiety > step_very_high) aggressiveness = enAggressiveness.VeryHigh;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            switch (aggressiveness)
            {
                case enAggressiveness.Low:
                    minimum_time_between_spawns = 6;
                    break;
                case enAggressiveness.Moderate:
                    minimum_time_between_spawns = 3;
                    break;
                case enAggressiveness.High:
                    minimum_time_between_spawns = 0.5f;
                    percent_to_spawn = 7;
                    break;
                case enAggressiveness.VeryHigh:
                    minimum_time_between_spawns = 0;
                    percent_to_spawn = 10;
                    break;
            }
            if (RollDice(percent_to_spawn))
            {
                timer = minimum_time_between_spawns;
                SpawnWord();
            }
        }
	}

    public string toBold(string value)
    {
        return "<b>" + value + "</b>";
    }

    public string toItalics(string value)
    {
        return "<i>" + value + "</i>";
    }

    public void SendWords(List<string> shotgun_list)
    {
        foreach(string word in shotgun_list)
        {
            SendWord(word);
        }
    }

    bool RollDice(float percent_chance)
    {
        return (Random.Range(0,100) < percent_chance);
    }

    string GetRandomWord()
    {
        switch (aggressiveness)
        {
            case enAggressiveness.Low:
                percent_of_helpless_word = 1;
                percent_of_negative_word = 1;
                percent_of_general_word = 10; 
                break;
            case enAggressiveness.Moderate:
                percent_of_helpless_word = 1;
                percent_of_negative_word = 10;
                percent_of_general_word = 20; 
                break;
            case enAggressiveness.High:
                percent_of_helpless_word = 1;
                percent_of_negative_word = 30;
                percent_of_general_word = 20; 
                break;
            case enAggressiveness.VeryHigh:
                percent_of_helpless_word = 40;
                percent_of_negative_word = 40;
                percent_of_general_word = 20; 
                break;
        }
        int roll = Random.Range(0, 100);
        if(roll < percent_of_helpless_word)
        {
            return helpless_word_list[Random.Range(0, helpless_word_list.Count)];
        }
        else if((roll - percent_of_helpless_word) < percent_of_negative_word)
        {
            return negative_word_list[Random.Range(0, negative_word_list.Count)];
        }
        else if((roll - percent_of_helpless_word - percent_of_negative_word) < percent_of_general_word)
        {
            return general_word_list[Random.Range(0, general_word_list.Count)];
        }
        else
        {
            //return instructional_word_list[Random.Range(0, instructional_word_list.Count)];
            CompulsionSystem.enActions action = gameObject.GetComponent<CompulsionSystem>().current_action;
            List<string> custom_action_words = new List<string>();
            switch (action)
            {
                case CompulsionSystem.enActions.Aligning:
                    {
                        CompulsionSystem.enObjects objects = gameObject.GetComponent<CompulsionSystem>().current_object;
                        switch (objects)
                        {
                            case CompulsionSystem.enObjects.Blocks:
                                custom_action_words.Add("STRAIGHTEN");
                                custom_action_words.Add("CROOKED");
                                custom_action_words.Add("CHECK");
                                custom_action_words.Add("STACK");
                                break;
                            case CompulsionSystem.enObjects.Circles:
                                custom_action_words.Add("GRID");
                                custom_action_words.Add("CROOKED");
                                custom_action_words.Add("NEAT ROWS");
                                break;
                            case CompulsionSystem.enObjects.Switches:
                                custom_action_words.Add("ALL THE SAME");
                                custom_action_words.Add("MATCHING");
                                custom_action_words.Add("CHECK");
                                custom_action_words.Add("CLICK");
                                break;

                        }
                    }
                    break;
                case CompulsionSystem.enActions.Sorting:
                    {
                        CompulsionSystem.enObjects objects = gameObject.GetComponent<CompulsionSystem>().current_object;
                        switch (objects)
                        {
                            case CompulsionSystem.enObjects.Blocks:
                                custom_action_words.Add("STRAIGHTEN");
                                custom_action_words.Add("CROOKED");
                                custom_action_words.Add("CHECK");
                                custom_action_words.Add("COLUMNS OF COLOR");
                                custom_action_words.Add("STACK");
                                break;
                            case CompulsionSystem.enObjects.Circles:
                                custom_action_words.Add("CROOKED");
                                custom_action_words.Add("NEAT ROWS");
                                custom_action_words.Add("COLUMNS OF COLOR");
                                break;
                            case CompulsionSystem.enObjects.Switches:
                                custom_action_words.Add("ALL THE SAME");
                                custom_action_words.Add("MATCHING");
                                custom_action_words.Add("CHECK");
                                custom_action_words.Add("CLICK");
                                break;
                        }
                    }
                    break;
                case CompulsionSystem.enActions.Tapping:
                    custom_action_words.Add("TAP");
                    custom_action_words.Add("TAPPING");
                    break;
                case CompulsionSystem.enActions.Touching:
                    custom_action_words.Add("TOUCH");
                    custom_action_words.Add("TOUCHING");
                    break;
            }
            return custom_action_words[Random.Range(0,custom_action_words.Count)];
        }
    }

    void SpawnWord()
    {
        string word = GetRandomWord();

        int percent_to_bold = 0, percent_to_italics = 0;
        switch (aggressiveness)
        {
            case enAggressiveness.High:
                percent_to_bold = 5;
                break;
            case enAggressiveness.VeryHigh:
                percent_to_bold = 15;
                percent_to_italics= 15;
                break;
        }
        if (RollDice(percent_to_bold)) word = toBold(word);
        if (RollDice(percent_to_italics)) word = toItalics(word);
        SendWord(word);
    }

    void SendWord(string word)
    {
        float speed = Random.Range(min_speed, max_speed);
        float trajectory = Random.Range(-0, 0);
        GameObject message = Instantiate(text_prefab);
        switch (aggressiveness)
        {
            case enAggressiveness.Low:
                min_speed = 2;
                max_speed = 5;
                trajectory = Random.Range(-0, 0);
                break;
            case enAggressiveness.Moderate:
                min_speed = 5;
                max_speed = 10;
                trajectory = Random.Range(-5, 5);
                break;
            case enAggressiveness.High:
                min_speed = 5;
                max_speed = 10;
                trajectory = Random.Range(-10, 10);
                break;
            case enAggressiveness.VeryHigh:
                min_speed = 10;
                max_speed = 15;
                trajectory = Random.Range(-20, 20);
                break;
        }
        message.GetComponent<TextBullet>().speed = speed;
        message.GetComponent<TextBullet>().angle_off_zero = trajectory;
        message.GetComponent<TextBullet>().direction = new Vector2(0,-1);
        message.GetComponent<TextMesh>().text = word;
        Vector2 position = new Vector2(Random.Range(x_min, x_max), Random.Range(y_min, y_max));
        message.transform.position = position;
        Destroy(message, 10);
    }
}
