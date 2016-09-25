using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageSystem : MonoBehaviour {

    public GameObject text_prefab;
    public float random_spawn_range;
    public float minimum_time_between_spawns;

    public int max_spawn_percent;
    public int max_word_speed;
    public int max_degree_trajectory_rotation;
    public int max_percent_to_bold;
    public int max_percent_to_italicize;

    public int percent_to_spawn;
    public int current_trajectory_rotation;
    public int percent_to_bold;
    public int percent_to_italicize;
    public int current_word_speed;

    public int percent_of_general_word;
    public int percent_of_negative_word;
    public int percent_of_helpless_word;

    List<string> general_word_list = new List<string> { "SILLY", "NO", "MOVE ON", "JUST STOP", "WHY" };
    List<string> instructional_word_list = new List<string> { "STRAIGHTER", "CROOKED", "BY COLOR", "GRID", "TOUCH", "TAP", "STACK" };
    List<string> negative_word_list = new List<string> { "EW", "STOP", "WRONG", "ALWAYS WRONG", "BAD", "DUMB", "STUPID", "WONT STOP", "CANT STOP", "PLEASE STOP", "FILTHY" };
    List<string> helpless_word_list = new List<string> { "please", "...", "just", "so tired", "go away", "leave me alone", "wait", "help" };

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        max_spawn_percent = Mathf.Clamp(max_spawn_percent, 0, 100);
        max_degree_trajectory_rotation = Mathf.Clamp(max_degree_trajectory_rotation, -180, 180);
        max_percent_to_bold = Mathf.Clamp(max_percent_to_bold, 0, 100);
        max_percent_to_italicize = Mathf.Clamp(max_percent_to_italicize, 0, 100);

        percent_to_spawn = Mathf.Clamp(percent_to_spawn, 0, max_spawn_percent);
        current_trajectory_rotation = Mathf.Clamp(current_trajectory_rotation, -max_degree_trajectory_rotation, max_degree_trajectory_rotation);
        percent_to_bold = Mathf.Clamp(percent_to_bold, 0, max_percent_to_bold);
        percent_to_italicize = Mathf.Clamp(percent_to_italicize, 0, max_percent_to_italicize);
        current_word_speed = Mathf.Clamp(current_word_speed, 0, max_word_speed);

        if(RollDice(percent_to_spawn))
        {
            SpawnWord();
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

    bool RollDice(int percent_chance)
    {
        return (Random.Range(0,100) < percent_chance);
    }

    string GetRandomWord()
    {
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
            return instructional_word_list[Random.Range(0, instructional_word_list.Count)];
        }
    }

    void SpawnWord()
    {
        string word = GetRandomWord();
        if (RollDice(percent_to_bold))
        {
            word = toBold(word);
        }
        else if (RollDice(percent_to_italicize))
        {
            word = toItalics(word);
        }
    }

    void SendWord(string word)
    {

    }
}
