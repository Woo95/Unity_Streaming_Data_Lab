
/*
This RPG data streaming assignment was created by Fernando Restituto with 
pixel RPG characters created by Sean Browning.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


#region Assignment Instructions

/*  Hello!  Welcome to your first lab :)

Wax on, wax off.

    The development of saving and loading systems shares much in common with that of networked gameplay development.  
    Both involve developing around data which is packaged and passed into (or gotten from) a stream.  
    Thus, prior to attacking the problems of development for networked games, you will strengthen your abilities to develop solutions using the easier to work with HD saving/loading frameworks.

    Try to understand not just the framework tools, but also, 
    seek to familiarize yourself with how we are able to break data down, pass it into a stream and then rebuild it from another stream.


Lab Part 1

    Begin by exploring the UI elements that you are presented with upon hitting play.
    You can roll a new party, view party stats and hit a save and load button, both of which do nothing.
    You are challenged to create the functions that will save and load the party data which is being displayed on screen for you.

    Below, a SavePartyButtonPressed and a LoadPartyButtonPressed function are provided for you.
    Both are being called by the internal systems when the respective button is hit.
    You must code the save/load functionality.
    Access to Party Character data is provided via demo usage in the save and load functions.

    The PartyCharacter class members are defined as follows.  */

public partial class PartyCharacter
{
	public int classID;

	public int health;
	public int mana;

	public int strength;
	public int agility;
	public int wisdom;

	public LinkedList<int> equipment;
}


/*
    Access to the on screen party data can be achieved via …..

    Once you have loaded party data from the HD, you can have it loaded on screen via …...

    These are the stream reader/writer that I want you to use.
    https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter
    https://docs.microsoft.com/en-us/dotnet/api/system.io.streamreader

    Alright, that’s all you need to get started on the first part of this assignment, here are your functions, good luck and journey well!
*/


#endregion


#region Assignment Part 1

static public class AssignmentPart1
{
	static private string partyDataFilePath = "partyData.txt";

	static public void SavePartyButtonPressed()
	{
		using (StreamWriter sw = new StreamWriter(partyDataFilePath))
		{
			foreach (PartyCharacter pc in GameContent.partyCharacters)
			{
				sw.WriteLine($"{pc.classID},{pc.health},{pc.mana},{pc.strength},{pc.agility},{pc.wisdom}");

				foreach (int value in pc.equipment)
				{
					sw.WriteLine($",{value}");
				}
				sw.WriteLine(); // change to next line
			}
		}
	}

	static public void LoadPartyButtonPressed()
	{
		GameContent.partyCharacters.Clear();

		if (File.Exists(partyDataFilePath))
		{
			using (StreamReader sr = new StreamReader(partyDataFilePath))
			{
				string line;

				while ((line = sr.ReadLine()) != null)
				{
					string[] data = line.Split(',');

					if (data.Length == 6)
					{
						int classID = int.Parse(data[0]);
						int health = int.Parse(data[1]);
						int mana = int.Parse(data[2]);
						int strength = int.Parse(data[3]);
						int agility = int.Parse(data[4]);
						int wisdom = int.Parse(data[5]);

						PartyCharacter pc = new PartyCharacter(classID, health, mana, strength, agility, wisdom);

						while ((line = sr.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
						{
							int equipmentValue = int.Parse(line.TrimStart(','));
							pc.equipment.AddLast(equipmentValue);
						}

						GameContent.partyCharacters.AddLast(pc);
					}
				}
			}
		}
		else
			Debug.Log("Failed to load file");

		GameContent.RefreshUI();
	}
}
#endregion

#region Assignment Part 2

//  Before Proceeding!
//  To inform the internal systems that you are proceeding onto the second part of this assignment,
//  change the below value of AssignmentConfiguration.PartOfAssignmentInDevelopment from 1 to 2.
//  This will enable the needed UI/function calls for your to proceed with your assignment.
static public class AssignmentConfiguration
	{
		public const int PartOfAssignmentThatIsInDevelopment = 2;
	}

	/*

	In this part of the assignment you are challenged to expand on the functionality that you have already created.  
		You are being challenged to save, load and manage multiple parties.
		You are being challenged to identify each party via a string name (a member of the Party class).

	To aid you in this challenge, the UI has been altered.  

		The load button has been replaced with a drop down list.  
		When this load party drop down list is changed, LoadPartyDropDownChanged(string selectedName) will be called.  
		When this drop down is created, it will be populated with the return value of GetListOfPartyNames().

		GameStart() is called when the program starts.

		For quality of life, a new SavePartyButtonPressed() has been provided to you below.

		An new/delete button has been added, you will also find below NewPartyButtonPressed() and DeletePartyButtonPressed()

	Again, you are being challenged to develop the ability to save and load multiple parties.
		This challenge is different from the previous.
		In the above challenge, what you had to develop was much more directly named.
		With this challenge however, there is a much more predicate process required.
		Let me ask you,
			What do you need to program to produce the saving, loading and management of multiple parties?
			What are the variables that you will need to declare?
			What are the things that you will need to do?  
		So much of development is just breaking problems down into smaller parts.
		Take the time to name each part of what you will create and then, do it.

	Good luck, journey well.

	*/

static public class AssignmentPart2
{

	static List<string> listOfPartyNames;

	static private string partyDataFilePath = "partyData.txt";

	static public void GameStart()
	{
		listOfPartyNames = new List<string>();
		StreamWriter sw = new StreamWriter(partyDataFilePath); // refreshes partyData whenever restarting unity play
		GameContent.RefreshUI();
	}

	static public List<string> GetListOfPartyNames()
	{
		return listOfPartyNames;
	}

	static public void LoadPartyDropDownChanged(string selectedName)
	{
		if (!listOfPartyNames.Contains(selectedName)) // if selectedParty is not contained in the dropdown
			return;

		GameContent.partyCharacters.Clear();

		if (File.Exists(partyDataFilePath))
		{
			using (StreamReader sr = new StreamReader(partyDataFilePath))
			{
				string line;
				int playerInParty = 0;
				while ((line = sr.ReadLine()) != null)
				{
					string[] data = line.Split(',');
					
					if (data[0] == selectedName && data.Length == 2)
					{
						playerInParty = int.Parse(data[1]);
						break;
					}
				}
				while ((line = sr.ReadLine()) != null && playerInParty > 0)
				{
					string[] data = line.Split(',');

					int classID = int.Parse(data[0]);
					int health = int.Parse(data[1]);
					int mana = int.Parse(data[2]);
					int strength = int.Parse(data[3]);
					int agility = int.Parse(data[4]);
					int wisdom = int.Parse(data[5]);

					PartyCharacter pc = new PartyCharacter(classID, health, mana, strength, agility, wisdom);

					int equipmentCount = int.Parse(data[6]);

					for (int i = 7; i < 7 + equipmentCount; i++)
					{
						int equip = int.Parse(data[i]);
						pc.equipment.AddLast(equip);
					}

					GameContent.partyCharacters.AddLast(pc);

					playerInParty--;
				}
			}
		}
		else
		{
			Debug.Log("Failed to load file");
		}
		GameContent.RefreshUI();
	}

	static public bool SavePartyButtonPressed()
	{
		string newPartyName = GameContent.GetPartyNameFromInput();

		if (string.IsNullOrEmpty(newPartyName)) // if string is empty 
		{
			Debug.Log("Failed to save - empty input");
			return false;
		}
		if (listOfPartyNames.Contains(newPartyName)) // if name is duplicate
		{
			Debug.Log("Failed to save - party name exist");
			return false;
		}

		listOfPartyNames.Add(newPartyName);

		using (StreamWriter sw = new StreamWriter(partyDataFilePath, true)) // true makes the txt continuous from the previous txt
		{
			sw.WriteLine($"{newPartyName}, {GameContent.partyCharacters.Count}");
			foreach (PartyCharacter pc in GameContent.partyCharacters)
			{
				string characterInfo = $"{pc.classID},{pc.health},{pc.mana},{pc.strength},{pc.agility},{pc.wisdom}";

				characterInfo += $",{pc.equipment.Count}";

				foreach (int equip in pc.equipment)
					characterInfo += $",{equip}";

				sw.WriteLine(characterInfo);
			}
		}

		GameContent.RefreshUI();

		Debug.Log(newPartyName + " has been added");

		return true;
	}

	static public bool DeletePartyButtonPressed(string partyNameToDelete)
	{
		if (string.IsNullOrEmpty(partyNameToDelete) || !listOfPartyNames.Contains(partyNameToDelete))
			return false; // if the party name is empty or not found in the list, skip


		listOfPartyNames.Remove(partyNameToDelete);

		string originalFilePath = "partyData.txt";
		string temporaryFilePath = "temp.txt";

		using (StreamReader sr = new StreamReader(originalFilePath))
		using (StreamWriter sw = new StreamWriter(temporaryFilePath))
		{
			string line;
			int playerInDeleteParty = 0;
			while ((line = sr.ReadLine()) != null)
			{
				string[] data = line.Split(',');
				if (data[0] == partyNameToDelete && data.Length == 2) // if partyNameToDelete from the txt has been found
				{
					playerInDeleteParty = int.Parse(data[1]);
					for (int i=0; i< playerInDeleteParty; i++) // deletes the player in the party that has to be deleted
					{
						line = sr.ReadLine();
					}
					continue;
				}
				sw.WriteLine(line);
			}
		}
		File.Delete(originalFilePath);
		File.Move(temporaryFilePath, originalFilePath); // move temp.txt to partyData.txt

		GameContent.RefreshUI();

		Debug.Log(partyNameToDelete + " has been deleted");

		return true;
	}
}
#endregion