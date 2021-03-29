using Elsa.Services;
using Elsa.Results;
using Elsa.Services.Models;
using System;

namespace Elsa.Test.Activities
{
    public class CustomActivityOne : Activity
    {
        private readonly IWorkflowExpressionEvaluator expressionEvaluator;

        public CustomActivityOne(IWorkflowExpressionEvaluator expressionEvaluator)
        {
            this.expressionEvaluator = expressionEvaluator;
        }

        public string Input1
        {
            get => GetState<string>();
            set => SetState(value);
        }

        public string Input2
        {
            get => GetState<string>();
            set => SetState(value);
        }

        protected override ActivityExecutionResult OnExecute(WorkflowExecutionContext context)
        {
            string output = Input1 + ", " + Input2;

            //Console.WriteLine(output);
            Output.SetVariable("CvdOutput", output);
            return Done();
        }
    }

}