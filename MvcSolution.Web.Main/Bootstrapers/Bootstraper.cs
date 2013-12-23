using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace MvcSolution.Web.Main.Bootstrapers
{
    public class Bootstraper
    {
        private readonly List<IBootstraperTask> _tasks = new List<IBootstraperTask>();

        public List<IBootstraperTask> Tasks
        {
            get { return _tasks; }
        }

        public Bootstraper()
        {
            this.Tasks.Add(new InitializeIocTask(new UnityContainer()));
            this.Tasks.Add(new RegisterValidationAdaptersTask());
            this.Tasks.Add(new RegisterRoutesTask());
        }

        public void Run()
        {
            foreach (var task in this.Tasks)
            {
                task.Execute();
            }
        }
    }
}