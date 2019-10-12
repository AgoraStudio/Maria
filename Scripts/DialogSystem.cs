using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public Vector3 Position;
    public GameObject DialogBox;
    public Text CharacterNameTextBox;
    public Text DialogTextBox;
    public GameObject Canvas;
    public Dialog[] Dialogs;
    private GameObject MainCaracter;
    private bool DetectingSpace;
    private bool ShouldPlaying;
    private int DialogIndex;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<PlayerStatus>() != null)
            {
                MainCaracter = player;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Position, MainCaracter.transform.position) < 2f)
        {
            ShouldPlaying = true;
        }
        Dialog();

        if (Input.GetKeyUp(KeyCode.Space) && DetectingSpace)
        {
            DialogIndex++;
        }
    }

    private void Dialog()
    {
        CharacterMove characterMove = MainCaracter.GetComponent<CharacterMove>();
        if (DialogIndex >= Dialogs.Length)
        {
            SetTowerActivity(true);
            characterMove.CanMove = true;
            DetectingSpace = false;
            Destroy(Canvas);
        }
        if (ShouldPlaying)
        {
            if (DialogIndex > 0)
            {
                if (Dialogs[DialogIndex - 1].Character != null)
                {
                    Dialogs[DialogIndex - 1].Character.SetActive(false);
                }
            }

            if (DialogIndex >= Dialogs.Length) return;

            if (Dialogs[DialogIndex].Character != null)
            {
                Dialogs[DialogIndex].Character.SetActive(true);
            }
            if (!Dialogs[DialogIndex].EventInvoked)
            {
                Dialogs[DialogIndex].Event.Invoke();
                Dialogs[DialogIndex].EventInvoked = true;
            }
            CharacterNameTextBox.text = Dialogs[DialogIndex].CharacterName;
            DialogTextBox.text = Dialogs[DialogIndex].DialogText;
            DetectingSpace = true;
            DialogBox.SetActive(true);
            characterMove.CanMove = false;
            SetTowerActivity(false);
        }
    }

    public static void SetTowerActivity(bool Activity)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            TowerAttack towerAttack = enemy.GetComponent<TowerAttack>();
            if (towerAttack != null)
            {
                towerAttack.CanAttack = Activity;
            }
        }
    }
}
