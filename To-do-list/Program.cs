using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using static c;

string fileName = "tasklist.json";
string json = File.ReadAllText(fileName);
List<task> list = JsonSerializer.Deserialize<List<task>>(json)!;//reading from file

List<task>tasklist= new List<task>();

 //List < task > tasklist = JsonSerializer.Deserialize<List<task>>(jsonString)!;
c cobj = new c();
while (true)
{
    
    int[]minMax = cobj.number(list);
    Console.WriteLine("Welcome to TodoLy");
    Console.WriteLine($"You have {minMax[0]} tasks todo and {minMax[1]} tasks are done! ");
    Console.WriteLine($"Pick an option\n" +
        "(1) Show Task List(by date or project)\n" +
        "(2) Add New Task\n" +
        "(3) Edit task(Update , mark as done ,remove)\n" +
        "(4) Save and Quit");
    Console.WriteLine("__________________________________________________________________________________________");
    string a = Console.ReadLine();
    bool isInt = int.TryParse(a, out int option);
    if (isInt) {
        if (option == 4)
        {
            cobj.save(list, tasklist);
            break;
        }
        if (option == 2)
        {


            cobj.add(tasklist);
        }
        if (option == 3)
        {
            cobj.edit(list, tasklist);
        }
        if (option == 1)
        {
            cobj.show(list);
        }
    }
    
    
    Console.WriteLine("__________________________________________________________________________________________");
}


public class c
{
    public void show(List<task> list)//showing the task data
    {
        //string fileName = "tasklist.json";
        //string jsonString = File.ReadAllText(fileName);
        //foreach(t t in list) { Console.WriteLine(t.task_no);
        //    Console.WriteLine(t.task_Title);
        //    Console.WriteLine(t.task_no);
        //}
       // Console.WriteLine(jsonString);
       List<task>list1= list.OrderBy(list=>list.Due_date).ThenBy(list=>list.category).ToList();// sorting duedate  and  category
        Console.WriteLine("task_no".PadRight(10) + " " + "task_Title".PadRight(10) + "  " + " Due_date".PadRight(10) + "   " + "category".PadRight(12) + " " + "mark");
        Console.WriteLine("__________________________________________________________________________________________");
        foreach (var i in list1)
        {

            Console.WriteLine(i.task_no.ToString().PadRight(14) + i.task_Title.PadRight(10) + i.Due_date.ToString(("dd-MM-yyyy")).PadRight(15) + i.category.PadRight(10) + i.mark);
        }
    }
    public void add(List<task> tasklist)//adding new tasks
    {
      
        task taskobj = new task();
        Console.WriteLine("Enter task_no:");
        string number = Console.ReadLine();
        bool isInt = int.TryParse(number, out int value);
        taskobj.task_no = value;
        Console.WriteLine("Enter task-title:");
        taskobj.task_Title = Console.ReadLine();
        Console.WriteLine("Enter due date: ");
        string input = Console.ReadLine();
        DateTime dt = Convert.ToDateTime(input);
        taskobj.Due_date = dt;
        Console.WriteLine("Enter category: ");
        taskobj.category = Console.ReadLine();
       
        
        tasklist.Add(taskobj);

    }
    public void edit(List<task> list, List<task> tasklist)// editing the task

    {
        foreach (task e in tasklist)
        {
            list.Add(e);
        }
        Console.WriteLine(" Pick an Option:\n" +
            "(1) Update\n" +
            "(2) Mark as done\n" +
            "(3) Remove");
        string b= Console.ReadLine();
        bool isInt = int.TryParse(b, out int option1);
        if (isInt)
        {
            if (option1 == 1)// updating the task
        {
            Console.WriteLine("Enter the task_Title:");
            string title = Console.ReadLine();
            Console.WriteLine("What do u want to update:\n" +
                "(1) Category\n" +
                "(2) Task Title\n" +
                "(3) Due date\n" +
                "(4) to remove the mark as done");
            string option2 = Console.ReadLine();

            
            
                foreach (task task in list)
                {
                    if (task.task_Title == title&&option2=="1")
                    {


                        Console.WriteLine("Update category");
                        string input = Console.ReadLine();
                        task.category = input;
                       
                    }
                     else if (task.task_Title == title && option2=="2")
                    {
                        Console.WriteLine("Update Task_Title ");
                        string input = Console.ReadLine();
                        task.task_Title = input;
                      
                    }
                    else if (task.task_Title == title && option2=="3")
                    {
                        Console.WriteLine("Update Due_date ");
                        string input = Console.ReadLine();
                        DateTime dt = Convert.ToDateTime(input);
                        task.Due_date = dt;
                        
                    }
                    else if (task.task_Title == title && option2 == "4")
                    {
                        task.mark = null;
                    }

                }
           


        }
            if (option1 == 2)// mark as done
            {
            Console.WriteLine("Enter the task_Title u want to mark as done:");
            string title = Console.ReadLine();
            foreach(var task in list)
            {
                if (task.task_Title == title)
                {
                    task.mark = "done";
                }
            }
        }
            if(option1 == 3)// removing the task
            {
            Console.WriteLine("Enter the task_no:");
            string no = Console.ReadLine();
            bool isInt1 = int.TryParse(no,out int value);

            //foreach (task task in tasklist)
            //{
            //    if (task.task_no == value)
            //    {
            //        int indexToRemove = value ;

            //        task.RemoveAt(indexToRemove);
            //    }
            //}
            //for (int i = 0; i < tasklist.Capacity; i++)
            //{
            //    if (i.taskobj.task_no == value)
            //    {
            //        tasklist.Remove(i);
            //    }
            //}
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].task_no == value)
                {
                    list.RemoveAt(i);
                }
            }
        }
      }  
    }
      public void save(List<task> list,List<task> tasklist)//save and quiting
    {
        foreach (task e in tasklist)
        {
            list.Add(e);
        }

        bool i=true;
        while (i) {
            //Console.WriteLine("PRESS S to save" +
            //"PRESS quit to quit");
            //string option = Console.ReadLine();
            //if (option == "s")
            //{

                string fileName = "tasklist.json";
                string jsonString = JsonSerializer.Serialize(list);
                File.WriteAllText(fileName, jsonString);

               // Console.WriteLine(File.ReadAllText(fileName));
            //}
            //if (option == "quit")
            break; 
        }
        
        
    }
    public int[] number(List<task> list)//updating the  no.of tasks and  marking as done
    {
        int[] minMax = new int[2];
        foreach(var i in list)
        {
            minMax[0] = minMax[0] + 1;
            if (i.mark == "done")
            {
                minMax[1] = minMax[1] + 1;
            }
        }

        return minMax;
    }
}

public class task
{
    public int task_no { get; set; }
    public string task_Title { get; set; }
    public DateTime Due_date { get; set; }
    public string category { get; set; }
    public string mark { get; set; }
}
public class t
{
    public int task_no { get; set; }
    public string task_Title { get; set; }
    public DateTime Due_date { get; set; }
    public string category { get; set; }
    public string mark { get; set; }
}