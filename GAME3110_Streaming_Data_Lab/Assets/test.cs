

//#region Assignment Part 1
//using System.IO;

//static public class AssignmentPart1
//{
//	static private string partyDataFilePath = "partyData.txt";

//	static public void SavePartyButtonPressed()
//	{
//		using (StreamWriter sw = new StreamWriter(partyDataFilePath))
//		{
//			foreach (PartyCharacter pc in GameContent.partyCharacters)
//			{
//				sw.WriteLine($"{pc.classID},{pc.health},{pc.mana},{pc.strength},{pc.agility},{pc.wisdom}");

//				foreach (int value in pc.equipment)
//				{
//					sw.WriteLine($",{value}");
//				}
//				sw.WriteLine(); // change to next line
//			}
//		}
//	}

//	static public void LoadPartyButtonPressed()
//	{
//		GameContent.partyCharacters.Clear();

//		if (File.Exists(partyDataFilePath))
//		{
//			using (StreamReader sr = new StreamReader(partyDataFilePath))
//			{
//				string line;

//				while ((line = sr.ReadLine()) != null)
//				{
//					string[] data = line.Split(',');

//					if (data.Length == 6)
//					{
//						int classID = int.Parse(data[0]);
//						int health = int.Parse(data[1]);
//						int mana = int.Parse(data[2]);
//						int strength = int.Parse(data[3]);
//						int agility = int.Parse(data[4]);
//						int wisdom = int.Parse(data[5]);

//						PartyCharacter pc = new PartyCharacter(classID, health, mana, strength, agility, wisdom);

//						while ((line = sr.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
//						{
//							int equipmentValue = int.Parse(line.TrimStart(','));
//							pc.equipment.AddLast(equipmentValue);
//						}

//						GameContent.partyCharacters.AddLast(pc);
//					}
//				}
//			}
//		}
//		else
//			Debug.Log("Failed to load file");

//		GameContent.RefreshUI();
//	}
//}

//#endregion