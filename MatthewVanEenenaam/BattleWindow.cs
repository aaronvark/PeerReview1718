using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleWindow : GenericWindow
{
    public Image[] decorations;
    public GameObject actionsGroup;
    public Text monsterLabel;
    public GenericBattleAction[] actions;//allows multiple actions to the battle window
    public bool nextActionPlayer = true;
    //chances of run succes
    [Range(0, .9f)]
    public float runOdds = .3f;
    public RectTransform windowRect;
    public RectTransform monsterRect;
    public delegate void BattleOver(bool playerWin);
    public BattleOver battleOverCallback;
    public string enemyQuestName;
    public GameObject playerFlag;

    private QuestManager theQm;
    private ShakeManager shakeManager;
    private MusicController theMc;

    private Actor player;
    private Actor monster;
    private System.Random rand = new System.Random();

    protected override void Awake()
    {
        shakeManager = GetComponent<ShakeManager>();
        theQm = FindObjectOfType<QuestManager>();
        theMc = FindObjectOfType<MusicController>();
        base.Awake();
    }

    public override void Open()
    {
        base.Open();
        //random background
        foreach(var decoration in decorations)
        {
            decoration.enabled = rand.NextDouble() >= .5;
        }

        actionsGroup.SetActive(false);
    }

    //start battle message
    public void StartBattle(Actor target1,Actor target2)
    {
        player = target1;
        monster = target2;
        theMc.SwitchTrack(1);
        DisplayMessage("A "+monster.name+" apeared");
        StartCoroutine(NextAction());
        UpdateMonsterLabel();
    }

    //looping actions
    public void OnAction(GenericBattleAction action, Actor target1,Actor traget2)
    {

        action.Action(target1, traget2);//player atk monster

        DisplayMessage(action.ToString());
        actionsGroup.SetActive(false);

        UpdatePlayerStats();
        UpdateMonsterLabel();

        StartCoroutine(NextAction());
    }

    //self explanitory
    public void OnPlayerAction(int id)
    {
        switch(id)
        {
            case 1:
                StartCoroutine(OnRun());
                    break;
            default:
            var action = actions[id];
            OnAction(action, player, monster);
            shakeManager.Shake(monsterRect, .5f, 1);
                break;
        }
        nextActionPlayer = false;
    }

    //self explanitory
    public void OnMonsterAction()
    {
        var action = actions[0];
        OnAction(action, monster, player);
        nextActionPlayer = true;
        shakeManager.Shake(windowRect, 1f, 2);
    }

    //message at battle
    void DisplayMessage(string text)
    {
        var messageWindow = manager.Open((int)Windows.MessageWindow - 1,false)as MessageWindow;
        messageWindow.text = text;
    }

    IEnumerator NextAction()
    {//set actions true after 2 seconds of battle start
        yield return new WaitForSeconds(2);

        if(!player.alive || !monster.alive)
        {
            StartCoroutine(OnBattleOver());
        }
        else
        {
            if (nextActionPlayer)
            {
                actionsGroup.SetActive(true);
                OnFocus();
            }
            else
            {
                OnMonsterAction();
            }
        }
        
        
    }

    void UpdatePlayerStats()
    {
        ((StatsWindow)manager.GetWindow((int)Windows.StatsWindow - 1)).UpdateStats();

    }

    private void UpdateMonsterLabel()
    {
        monsterLabel.text = monster.name + " HP " + monster.health.ToString("D2");

    }

    //run command 
    IEnumerator OnRun()
    {
        actionsGroup.SetActive(false);

        var chance = Random.Range(0, 1f);
        if(chance < runOdds)
        {
            DisplayMessage("You where able to run away");
            yield return new WaitForSeconds(2);
            if (battleOverCallback != null)
                battleOverCallback(player.alive);
        }
        else
        {
            DisplayMessage("You where not able to run away");
            StartCoroutine(NextAction());
        }
    }

    IEnumerator OnBattleOver()
    {
        var message = (player.alive ? player.name : monster.name) + " has won the battle";
        var gold = Random.Range(0, monster.gold);
        if(gold > 0 && player.alive)
        {
            theQm.enemyKilled = enemyQuestName;
            message += " " + player.name + " recieves $" + gold + ".";
            playerFlag.GetComponent<PlayerFlags>().money += gold;
            player.IncreaseGold(gold);
            UpdatePlayerStats();

        }

        DisplayMessage(message);
        theMc.SwitchTrack(0);
        yield return new WaitForSeconds(2);

        if(battleOverCallback != null)
            battleOverCallback(player.alive);

       

    }

}
