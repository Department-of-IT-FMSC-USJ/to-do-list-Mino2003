using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    class Task
    {
        private int id;
        private string name;
        private string description;
        private DateTime date;
        private string status;
        private Task next;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Status { get => status; set => status = value; }
        internal Task Next { get => next; set => next = value; }

        public Task(int id, string name, string description, DateTime date)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            Status = "To Do";
            Next = null;
        }
    }

    class LinkedList
    {
        public Task Head { get; set; }

        public void InsertSorted(Task task)
        {
            if (Head == null || task.Date < Head.Date)
            {
                task.Next = Head;
                Head = task;
            }
            else
            {
                Task current = Head;
                while (current.Next != null && current.Next.Date < task.Date)
                {
                    current = current.Next;
                }
                task.Next = current.Next;
                current.Next = task;
            }
        }

        public void Push(Task task)
        {
            task.Next = Head;
            Head = task;
        }

        public Task Pop()
        {
            if (Head == null) return null;
            Task task = Head;
            Head = Head.Next;
            return task;
        }

        public void Enqueue(Task task)
        {
            if (Head == null)
            {
                Head = task;
            }
            else
            {
                Task current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = task;
            }
        }
    }

    class TaskManager
    {
        private LinkedList todoTasks = new LinkedList();
        private LinkedList inProgressTasks = new LinkedList();
        private LinkedList completedTasks = new LinkedList();

        public void AddTask(int id, string name, string description, DateTime date)
        {
            Task task = new Task(id, name, description, date);
            todoTasks.InsertSorted(task);
        }

        public void StartTask(int id)
        {
            Task prev = null, current = todoTasks.Head;
            while (current != null)
            {
                if (current.Id == id)
                {
                    if (prev != null)
                    {
                        prev.Next = current.Next;
                    }
                    else
                    {
                        todoTasks.Head = current.Next;
                    }
                    current.Status = "In Progress";
                    current.Next = null;
                    inProgressTasks.Push(current);
                    return;
                }
                prev = current;
                current = current.Next;
            }
        }

        public void CompleteTask()
        {
            Task task = inProgressTasks.Pop();
            if (task != null)
            {
                task.Status = "Completed";
                task.Next = null;
                completedTasks.Enqueue(task);
            }
        }
    }
}

