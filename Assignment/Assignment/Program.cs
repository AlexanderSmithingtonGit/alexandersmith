using System;
using System.Reflection.Metadata;

namespace Assignment
{
    public struct Contestant
    {
        public string lastName;
        public string firstName;
        public string interest;
    }
    public struct Question
    {
        public string question;
        public string answer1;
        public string answer2;
        public string answer3;
        public string answer4;
        public char correctAns;
    }

    internal class Program
    {
        public static Contestant[] contestants = new Contestant[35];
        public static List<Question> questions = new List<Question>();

        public static int temp = -1;
        static void Main(string[] args)
        {
            //Puts contestants into array from file first thing. then sorts them by last name and reverses it to be alphabetical
            ReadContestants();
            Array.Sort(contestants, (x, y) => y.lastName.CompareTo(x.lastName));
            Array.Reverse(contestants);

            //Puts question from file into List.
            ReadQuestions();

            //loop that repeats menu unless user enters exit numer (0)
            do
            {
                MainMenu();
            } while (temp != 0);

        }

        public static void MainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("__        ___            __        __          _         _        \r\n\\ \\      / / |__   ___   \\ \\      / /_ _ _ __ | |_ ___  | |_ ___  \r\n \\ \\ /\\ / /| '_ \\ / _ \\   \\ \\ /\\ / / _` | '_ \\| __/ __| | __/ _ \\ \r\n  \\ V  V / | | | | (_) |   \\ V  V / (_| | | | | |_\\__ \\ | || (_) |\r\n _ \\_/\\_/  |_| |_|\\___/_  __\\_/\\_/ \\__,_|_| |_|\\__|___/  \\__\\___/ \r\n| |__   ___    __ _  |  \\/  (_) | (_) ___  _ __   __ _ _ __ ___   \r\n| '_ \\ / _ \\  / _` | | |\\/| | | | | |/ _ \\| '_ \\ / _` | '__/ _ \\  \r\n| |_) |  __/ | (_| | | |  | | | | | | (_) | | | | (_| | | |  __/  \r\n|_.__/ \\___|  \\__,_| |_|  |_|_|_|_|_|\\___/|_| |_|\\__,_|_|  \\___|  \n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Main Menu");
            Console.WriteLine("Select from the numbers below");
            Console.WriteLine("1\tList Contestants");
            Console.WriteLine("2\tEdit Interest");
            Console.WriteLine("3\tPlay");
            Console.WriteLine("0\tExit");
            bool inputCheck = true; //boolean for handling invalid input
            while (inputCheck)
            {
                inputCheck = false;
                temp = Convert.ToInt32(Console.ReadLine());
                if (temp == 1)
                {
                    List();
                }
                else if (temp == 2)
                {
                    Edit();
                }
                else if (temp == 3)
                {
                    Play();
                }
                else if (temp == 0)
                {
                    Exit();
                }
                else
                {
                    Console.WriteLine("Invalid Input");
                    inputCheck = true; //set boolean to true if invalid input is entered so that it repeats loop
                }
            }
            

        }

        public static void List()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("  ____            _            _              _       \r\n / ___|___  _ __ | |_ ___  ___| |_ __ _ _ __ | |_ ___ \r\n| |   / _ \\| '_ \\| __/ _ \\/ __| __/ _` | '_ \\| __/ __|\r\n| |__| (_) | | | | ||  __/\\__ \\ || (_| | | | | |_\\__ \\\r\n \\____\\___/|_| |_|\\__\\___||___/\\__\\__,_|_| |_|\\__|___/\n\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("First Name".PadRight(17));
            Console.Write("Last Name".PadRight(17));
            Console.WriteLine("Interest".PadLeft(17));

            Console.CursorVisible = false; //cursor hidden so that it doesnt follow names being written out; otherwise can look weird
            for (int i = 0; i < contestants.Length; i++)
            {
                Thread.Sleep(25);
                Console.Write(contestants[i].firstName.PadRight(17));
                Console.Write(contestants[i].lastName.PadRight(17));
                Console.WriteLine(contestants[i].interest.PadLeft(17));
            }
            Console.CursorVisible = true;

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        public static void Edit()
        {
            Console.Clear();
            Console.WriteLine("Enter Name of contestant to edit:");
            //when searching, converts everything to lowercase so it is case insensitive.
            string search = Console.ReadLine().ToLower();
            bool found = false; //boolean for checking if name is accurate
            for (int i = 0; i < contestants.Length; i++)
            {
                if (search == contestants[i].firstName.ToLower() + " " + contestants[i].lastName.ToLower())
                {
                    Console.WriteLine("Enter updated interest:");
                    contestants[i].interest = Console.ReadLine();
                    found = true;
                }
            }
            if (!found) //if name isn't accurate it notifys user
            {
                Console.WriteLine("Couldn't find entered user.");
            }
            else if (found) // if it finds the name, it saves the new interest to the file.
            {
                StreamWriter sw = new StreamWriter(@"Millionaire.txt");
                for (int i = 0; i < contestants.Length; i++)
                {
                    sw.WriteLine(contestants[i].firstName);

                    sw.WriteLine(contestants[i].lastName);

                    sw.WriteLine(contestants[i].interest);
                }
                sw.Close();

            }


            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        public static void Play()
        {
            Console.Clear();
            Random rnd = new Random();
            Console.WriteLine("Generating Finalists");
            Contestant[] finalists = new Contestant[10];
            List<int> PastInts = new List<int>(); //List for keeping track of past numbers, to stop duplicates
            for (int i = 0; i < finalists.Length; i++)
            {
                int index = 0;
                bool newrand = true; //boolean for if we need a new random number
                while (newrand)
                {
                    index = rnd.Next(1, contestants.Length);
                    newrand = false;
                    for (int j = 0; j < PastInts.Count; j++)
                    {
                        if (PastInts[j] == index) //if our random number is a duplicate, we need a new random number
                        {
                            newrand = true;
                        }
                    }
                }
                
                PastInts.Add(index); //adds random number to list of people already selected
                finalists[i] = contestants[index]; //adds contestant at random index to finalist array.
            }


            Console.Write("First Name".PadRight(17));
            Console.Write("Last Name".PadRight(17));
            Console.WriteLine("Interest".PadLeft(17));

            Console.CursorVisible = false;
            foreach (Contestant finalist in finalists)
            {
                Thread.Sleep(25);
                Console.Write(finalist.firstName.PadRight(17));
                Console.Write(finalist.lastName.PadRight(17));
                Console.WriteLine(finalist.interest.PadLeft(17));
            }
            Console.CursorVisible = true;

            int playerIndex = rnd.Next(0, finalists.Length);
            Contestant player = finalists[playerIndex];
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"You are playing as {player.firstName} {player.lastName}");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
            Console.Clear();
            int checkpoint = 0;
            int moneyLevel = 0;
            bool incorrect = false;
            bool won = false;
            List<int> PastQuestions = new List<int>(); //list to make sure questions arent selected twice
            //uses same structure as checking duplicates in finalist selection as it does for questions.
            for (int i = 0; i < questions.Count + 1 && !incorrect; i++)
            {

                switch (i + 1)
                {
                    case 1:
                        moneyLevel = 100;
                        break;
                    case 2:
                        moneyLevel = 200;
                        break;
                    case 3:
                        moneyLevel = 300;
                        break;
                    case 4:
                        moneyLevel = 500;
                        break;
                    case 5:
                        moneyLevel = 1000;
                        break;
                    case 6:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You have reached the $1000 checkpoint!");
                        Console.ForegroundColor = ConsoleColor.White;
                        checkpoint = 1000;
                        moneyLevel = 2000;
                        break;
                    case 7:
                        moneyLevel = 4000;
                        break;
                    case 8:
                        moneyLevel = 8000;
                        break;
                    case 9:
                        moneyLevel = 16000;
                        break;
                    case 10:
                        moneyLevel = 32000;
                        break;
                    case 11:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You have reached the $32000 checkpoint!");
                        Console.ForegroundColor = ConsoleColor.White;
                        checkpoint = 32000;
                        moneyLevel = 64000;
                        break;
                    case 12:
                        moneyLevel = 125000;
                        break;
                    case 13:
                        moneyLevel = 250000;
                        break;
                    case 14:
                        moneyLevel = 500000;
                        break;
                    case 15:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("This is the 1 million dollar question.");
                        moneyLevel = 1000000;
                        break;
                    case 16:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("You have won 1 million dollars!");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("   8         8         8         8   \r\n d8 8e     d8 8e     d8 8e     d8 8e \r\nC88       C88       C88       C88    \r\n Y8 8b     Y8 8b     Y8 8b     Y8 8b \r\n    88D       88D       88D       88D\r\n \"8 8P     \"8 8P     \"8 8P     \"8 8P \r\n   8         8         8         8   \n\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        checkpoint = 1000000;
                        won = true;
                        break;
                }

                if (!won)
                {
                    int questionIndex = 0;
                    bool newrand = true;
                    while (newrand)
                    {
                        questionIndex = rnd.Next(0, questions.Count);
                        newrand = false;
                        for (int j = 0; j < PastQuestions.Count; j++)
                        {
                            if (PastQuestions[j] == questionIndex)
                            {
                                newrand = true;
                            }
                        }
                    }
                    PastQuestions.Add(questionIndex);
                    Question question = questions[questionIndex];
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"For: ${moneyLevel}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"\tSafety: ${checkpoint}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"Question: {i + 1}");
                    Console.WriteLine($"{question.question}");
                    Console.WriteLine($"A {question.answer1}");
                    Console.WriteLine($"B {question.answer2}");
                    Console.WriteLine($"C {question.answer3}");
                    Console.WriteLine($"D {question.answer4}");
                    char Guess = Convert.ToChar(Console.ReadLine().ToUpper());
                    if (Guess == question.correctAns)
                    {
                        Console.WriteLine("Correct");
                    }
                    else
                    {
                        incorrect = true;
                        Console.WriteLine("Incorrect");
                    }
                    Console.WriteLine("Press any key to continue. . .");
                    Console.ReadKey();
                    Console.Clear();
                }
                
            }
            

            Console.WriteLine($"You walk away with ${checkpoint}");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        public static void Exit()
        {
            Console.Clear();
            Console.WriteLine("Exiting");
            Thread.Sleep(1000);
        }

        static void ReadContestants() //Read from file of class members and puts into contestant array
        {
            string line;
            StreamReader sr = new StreamReader(@"Millionaire.txt");

            for (int i = 0; i < contestants.Length; i++)
            {
                line = sr.ReadLine();
                contestants[i].firstName = line;
                line = sr.ReadLine();
                contestants[i].lastName = line;
                line = sr.ReadLine();
                contestants[i].interest = line;
            }
            sr.Close();
        }

        static void ReadQuestions() //Read from file of questions and puts into question list
        {
            string line;
            StreamReader sr = new StreamReader(@"Questions.txt");
            const int questionAmount = 15; //simple way to adjust how many questions it will check for
            for (int i = 0; i <= questionAmount - 1; i++)
            {                   
                Question temp = new Question();
                line = sr.ReadLine();
                temp.question = line;
                line = sr.ReadLine();
                temp.answer1 = line;
                line = sr.ReadLine();
                temp.answer2 = line;
                line = sr.ReadLine();
                temp.answer3 = line;
                line = sr.ReadLine();
                temp.answer4 = line;
                line = sr.ReadLine();
                temp.correctAns = Convert.ToChar(line);
                
                questions.Add(temp);
            }
            
            sr.Close();
        }
    }
}
