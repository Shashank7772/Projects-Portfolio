// the ourAnimals array will store the following: 
string animalSpecies = "";
string animalID = "";
string animalAge = "";
string animalPhysicalDescription = "";
string animalPersonalityDescription = "";
string animalNickname = "";

// variables that support data entry
int maxPets = 8;
string? readResult;
string menuSelection = "";

// array used to store runtime data, there is no persisted data
string[,] ourAnimals = new string[maxPets, 6];


// create some initial ourAnimals array entries
for (int i = 0; i < maxPets; i++)
{
    switch (i)
    {
        case 0:
            {
                animalSpecies = "dog";
                animalID = "d1";
                animalAge = "2";
                animalPhysicalDescription = "medium sized cream colored female golden retriever weighing about 65 pounds. housebroken.";
                animalPersonalityDescription = "loves to have her belly rubbed and likes to chase her tail. gives lots of kisses.";
                animalNickname = "lola";
                break;
            }
        case 1:
            {
                animalSpecies = "dog";
                animalID = "d2";
                animalAge = "9";
                animalPhysicalDescription = "large reddish-brown male golden retriever weighing about 85 pounds. housebroken.";
                animalPersonalityDescription = "loves to have his ears rubbed when he greets you at the door, or at any time! loves to lean-in and give doggy hugs.";
                animalNickname = "loki";
                break;
            }
        case 2:
            {
                animalSpecies = "cat";
                animalID = "c3";
                animalAge = "1";
                animalPhysicalDescription = "small white female weighing about 8 pounds. litter box trained.";
                animalPersonalityDescription = "friendly";
                animalNickname = "Puss";
                break;
            }
        case 3:
            {
                animalSpecies = "cat";
                animalID = "c4";
                animalAge = "?";
                animalPhysicalDescription = "";
                animalPersonalityDescription = "";
                animalNickname = "";
                break;
            }
        default:
            {
                animalSpecies = "";
                animalID = "";
                animalAge = "";
                animalPhysicalDescription = "";
                animalPersonalityDescription = "";
                animalNickname = "";
                break;
            }
    }

    ourAnimals[i, 0] = "ID #: " + animalID;
    ourAnimals[i, 1] = "Species: " + animalSpecies;
    ourAnimals[i, 2] = "Age: " + animalAge;
    ourAnimals[i, 3] = "Nickname: " + animalNickname;
    ourAnimals[i, 4] = "Physical description: " + animalPhysicalDescription;
    ourAnimals[i, 5] = "Personality: " + animalPersonalityDescription;
}

// display the top-level menu options

do
{
    Console.Clear();

    Console.WriteLine("Welcome to the Contoso PetFriends app. Your main menu options are:");
    Console.WriteLine(" 1. List all of our current pet information");
    Console.WriteLine(" 2. Add a new animal friend to the ourAnimals array");
    Console.WriteLine(" 3. Ensure animal ages and physical descriptions are complete");
    Console.WriteLine(" 4. Ensure animal nicknames and personality descriptions are complete");
    Console.WriteLine(" 5. Edit an animal’s age");
    Console.WriteLine(" 6. Edit an animal’s personality description");
    Console.WriteLine(" 7. Display all cats with a specified characteristic");
    Console.WriteLine(" 8. Display all dogs with a specified characteristic");
    Console.WriteLine();
    Console.WriteLine("Enter your selection number (or type Exit to exit the program)");

    readResult = Console.ReadLine();
    if (readResult != null)
    {
        menuSelection = readResult.ToLower();
    }

    Console.WriteLine($"You selected menu option {menuSelection}.");
    Console.WriteLine("Press the Enter key to continue");
    // pause code execution
    readResult = Console.ReadLine();

    switch (menuSelection)
    {
        case "1":
            {
                //This option displays all the information about the pets
                for (int i = 0; i < maxPets; i++)
                {
                    if (ourAnimals[i, 0] != "ID #: ")
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            Console.WriteLine(ourAnimals[i, j]);
                        }
                    }
                    Console.WriteLine("\n");
                }
                readResult = Console.ReadLine();
                break;
            }
        case "2":
            {
                string anotherPet = "y";
                int petCount = 0;
                for (int i = 0; i < maxPets; i++)
                {
                    if (ourAnimals[i, 0] == "ID #: ")
                    {
                        petCount++;
                    }

                }
                if (petCount < maxPets)
                {
                    Console.WriteLine("We currently have " + petCount + " and we can manage " + (maxPets - petCount) + " more");
                }
                while (anotherPet == "y" && petCount < maxPets)
                {
                    bool validEntry = false;
                    //get the species dog or cat, a valid animal speies is required
                    do
                    {
                        Console.WriteLine("Enter dog or cat to begin a new entry");
                        readResult = Console.ReadLine();
                        if (readResult != null)
                        {
                            animalSpecies = readResult.ToLower();
                            if (animalSpecies != "cat" && animalSpecies != "dog")
                            {
                                validEntry = false;
                            }
                            else
                            {
                                validEntry = true;
                            }
                        }
                    } while (validEntry == false);
                    animalID = animalSpecies.Substring(0, 1) + (petCount + 1).ToString();
                    //To get the pet's age, can be ? at entry
                    do
                    {
                        int petAge;
                        Console.WriteLine("Enter the age of your pet or \"?\" if unknown");
                        readResult = Console.ReadLine();
                        if (readResult != null)
                        {
                            animalAge = readResult.ToLower();
                            if (animalAge != "?")
                                validEntry = int.TryParse(animalAge, out petAge);
                            else
                                validEntry = true;
                        }
                    } while (validEntry == false);
                    //Get a description of the physical appearance of the cat or dog
                    do
                    {
                        Console.WriteLine("Enter a physical description of the pet (size, color, gender, weight, housebroken)");
                        readResult = Console.ReadLine();
                        if (readResult != null)
                        {
                            animalPhysicalDescription = readResult.ToLower();
                            if (animalPhysicalDescription == "")
                            {
                                animalPhysicalDescription = "tbd";
                            }
                        }
                    } while (animalPhysicalDescription == "");
                    //To get the personality discription of the pet
                    do
                    {
                        Console.WriteLine("Enter the animal's (cat/dog) peronality description like is it friendly, outgoing etc");
                        readResult = Console.ReadLine();
                        if (readResult != null)
                        {
                            animalPersonalityDescription = readResult.ToLower();
                            if (animalPersonalityDescription == "")
                            {
                                animalPersonalityDescription = "tbd";
                            }
                        }
                    } while (animalPersonalityDescription == "");
                    // get the pet's nickname. animalNickname can be blank.
                    do
                    {
                        Console.WriteLine("Enter a nickname for the pet");
                        readResult = Console.ReadLine();
                        if (readResult != null)
                        {
                            animalNickname = readResult.ToLower();
                            if (animalNickname == "")
                            {
                                animalNickname = "tbd";
                            }
                        }
                    } while (animalNickname == "");

                    // store the pet information in the ourAnimals array (zero based)
                    ourAnimals[petCount, 0] = "ID #: " + animalID;
                    ourAnimals[petCount, 1] = "Species: " + animalSpecies;
                    ourAnimals[petCount, 2] = "Age: " + animalAge;
                    ourAnimals[petCount, 3] = "Nickname: " + animalNickname;
                    ourAnimals[petCount, 4] = "Physical description: " + animalPhysicalDescription;
                    ourAnimals[petCount, 5] = "Personality: " + animalPersonalityDescription;

                    //Increments the pet counter whenever there is a correct species name
                    petCount++;
                    if (petCount < maxPets)
                    {
                        Console.Write("Do you want to add another pet (y/n) : ");
                        do
                        {
                            readResult = Console.ReadLine();
                            if (readResult != null)
                            {
                                anotherPet = readResult.ToLower();
                            }

                        } while (anotherPet != "n" && anotherPet != "y");
                    }
                }
                if (petCount >= maxPets)
                {
                    Console.WriteLine("The max capacity of pets has been reached, no more pets can be accomodated");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadKey();
                }
                break;
            }

        case "3":
            {
                // Ensure animal ages and physical descriptions are complete
                bool validEntry = true;
                for (int i = 0; i < maxPets; i++)
                {
                    if (ourAnimals[i, 0] != "ID #: ")
                    {
                        // Check and update animalAge
                        if (ourAnimals[i,2] == "Age: " || ourAnimals[i, 2] == "Age: ?")
                        {
                            do
                            {
                            Console.WriteLine($"Enter an age for {ourAnimals[i,0]}");
                            readResult = Console.ReadLine();
                            if (int.TryParse(readResult, out int age))
                            {
                                ourAnimals[i, 2] = "Age: " + age;
                                validEntry = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid age format. Age not updated.");
                            }
                            }while(validEntry == true);
                        }

                        // Check and update animalPhysicalDescription
                        if (ourAnimals[i, 4] == "Physical description: ")
                        {
                            Console.WriteLine($"Animal {ourAnimals[i, 0]} Enter a valid physical description for the animal");
                            readResult = Console.ReadLine();
                            while (string.IsNullOrEmpty(readResult))
                            {
                                Console.WriteLine("Enter something (not empty):");
                                readResult = Console.ReadLine();
                            } 
                            // Now 'userInput' contains a non-empty string
                            ourAnimals[i,4] = "Physical description: " + readResult;
                        }
                    }
                }
                Console.WriteLine("Animal age and description are updated");
                Console.WriteLine("Press the Enter key to continue.");
                readResult = Console.ReadLine();
                break;
            }
        case "4":
            {
                for(int i=0;i<maxPets;i++)
                {   
                    //To check if the animal petname section is empty and fill it if it is
                    if(ourAnimals[i,0] != "ID #: ")
                    {
                        if(ourAnimals[i,3] == "" || ourAnimals[i,3] == "Nickname: ")
                        {
                            do
                            {
                                Console.WriteLine($"Enter the nickname for {ourAnimals[i,0]}");
                                readResult = Console.ReadLine();
                            }while(string.IsNullOrEmpty(readResult));
                            ourAnimals[i,3] = "Nickname: "+readResult.ToLower();
                        }
                    }
                    //To check if the animal the animal personality description is complete or not
                    if(ourAnimals[i,0] != "ID #: ")
                    {
                        if(ourAnimals[i,5] == "Personality: " || ourAnimals[i,5] == "Personality: tbd")
                        {
                            do
                            {
                            Console.WriteLine("Please enter a valid personality for "+ourAnimals[i,0]);
                            readResult = Console.ReadLine();
                            }while(string.IsNullOrEmpty(readResult));
                            ourAnimals[i,5] = "Personality: "+readResult.ToLower();
                        }
                    }
                }
                break;
            }
        case "5":
            {
                Console.WriteLine("edit animal age");
                readResult = Console.ReadLine();
                break;
            }
        case "6":
            {
                Console.WriteLine("edit animal personality");
                readResult = Console.ReadLine();
                break;
            }
        case "7":
            {
                Console.WriteLine("display all cats with speified character");
                readResult = Console.ReadLine();
                break;
            }
        case "8":
            {
                Console.WriteLine("display all dogs with speified character");
                readResult = Console.ReadLine();
                break;
            }
        case "exit":
            break;
        default:
            {
                Console.WriteLine("Please enter a valid choice");
                readResult = Console.ReadLine();
                break;
            }
    }
} while (menuSelection != "exit");
