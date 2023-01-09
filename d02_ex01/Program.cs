using d02_ex01.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace d02_ex01
{
    class Program
    {
        private const string _incorrectData = "Input error. Check the input data and repeat the request.";
        private static List<Task> _tasks = new();

        static void Main(string[] args)
        {
            bool inProccess = true;
            while (inProccess)
            {
                Console.WriteLine("Write new command");
                var inputStr = Console.ReadLine();

                if (string.IsNullOrEmpty(inputStr))
                {
                    ShowIncorrectData();
                    continue;
                }

                switch (inputStr.ToLower())
                {
                    case "add":
                        var newTask = AddTask();
                        if (newTask != null)
                        {
                            _tasks.Add(newTask);
                        }
                        break;
                    case "list":
                        if(_tasks.Count == 0)
                        {
                            Console.WriteLine("The task list is still empty.");
                        }
                        else
                        {
                            foreach (var task in _tasks)
                            {
                                Console.WriteLine(task);
                                Console.WriteLine();
                            }
                        }
                        break;
                    case "done":
                    case "wontdo":
                        string title = ReadTitleString();
                        if (string.IsNullOrEmpty(title))
                        {
                            continue;
                        }

                        var findedTask = _tasks.FirstOrDefault(x => x.Title == title);
                        if(findedTask == null)
                        {
                            Console.WriteLine("Input error. The task with this title was not found");
                            continue;
                        }
                        if(inputStr.ToLower() == "done")
                        {
                            findedTask.TaskState = TaskState.Completed;
                            Console.WriteLine($"The task [{findedTask.Title}] is completed!");
                        }
                        else
                        {
                            findedTask.TaskState = TaskState.Irrelevant;
                            Console.WriteLine($"The task [{findedTask.Title}] is no longer relevant!");
                        }
                        break;
                    case "quit":
                    case "q":
                        inProccess = false;
                        break;
                    default:
                        ShowIncorrectData();
                        continue;
                }
            }
        }

        private static string ReadTitleString()
        {
            Console.WriteLine("Enter a title");
            string title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
            {
                ShowIncorrectData();
            }

            return title;
        }

        private static void ShowIncorrectData()
        {
            Console.WriteLine(_incorrectData);
            Console.ReadKey();
        }

        private static Task AddTask()
        {
            string title = SetRequiredStrParameter("Enter a title");
            string summary = SetRequiredStrParameter("Enter a description");

            DateTime? dueDate = null;
            Console.WriteLine("Enter the deadline");
            string dueDateStr = Console.ReadLine();

            if (!string.IsNullOrEmpty(dueDateStr) && DateTime.TryParse(dueDateStr, out DateTime parsedDate))
            {
                dueDate = parsedDate;
            }

            TaskType? taskType = null;
            bool isParsed = false;
            string inputStr;
            while (!isParsed)
            {
                Console.WriteLine("Enter the type");
                inputStr = Console.ReadLine();

                if (string.IsNullOrEmpty(inputStr))
                {
                    ShowIncorrectData();
                }
                else if (!Enum.TryParse(inputStr, out TaskType typeResult))
                {
                    ShowIncorrectData();
                }
                else
                {
                    isParsed = true;
                    taskType = typeResult;
                }
            }

            TaskPriority taskPriority;
            Console.WriteLine("Assign the priority");
            inputStr = Console.ReadLine();

            if (string.IsNullOrEmpty(inputStr) || !Enum.TryParse(inputStr, out TaskPriority priorityResult))
            {
                taskPriority = default;
            }
            else
            {
                taskPriority = priorityResult;
            }

            return new()
            {
                Title = title,
                Summary = summary,
                DueDate = dueDate,
                TaskType = taskType.Value,
                TaskPriority = taskPriority,
                TaskState = TaskState.New
            };
        }

        private static string SetRequiredStrParameter(string msgForShow)
        {
            string temp = string.Empty;
            while (string.IsNullOrEmpty(temp))
            {
                Console.WriteLine(msgForShow);
                temp = Console.ReadLine();

                if (string.IsNullOrEmpty(temp))
                {
                    ShowIncorrectData();
                }
            }

            return temp;
        }
    }
}
