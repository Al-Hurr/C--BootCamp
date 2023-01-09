
using System;

namespace d02_ex01.Tasks
{
    class Task
    {
        private string _title;
        public string Title { get => _title; set => _title = value; }

        private string _summary;
        public string Summary { get => _summary; set => _summary = value; }

        private DateTime? _dueDate;
        public DateTime? DueDate { get => _dueDate; set => _dueDate = value; }

        private TaskType _taskType;
        public TaskType TaskType { get => _taskType; set => _taskType = value; }

        private TaskPriority _taskPriority;
        public TaskPriority TaskPriority { get => _taskPriority; set => _taskPriority = value; }

        private TaskState _taskState;
        public TaskState TaskState { get => _taskState; set => _taskState = value; }

        public override string ToString()
        {
            return $"- {_title}\n[{_taskType}] [{_taskState}]\nPriority: " +
                $"{_taskPriority}{(_dueDate == null ? "" : ", Due till " + _dueDate.Value.ToString("dd.MM.yyyy"))}\n{_summary}";
        }
    }
}
