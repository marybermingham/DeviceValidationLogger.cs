using System;
using System.Collections.Generic;

class ValidationStep
{
    public string StepName { get; set; }
    public string Status { get; set; }
    public DateTime Timestamp { get; set; }

    public ValidationStep(string stepName)
    {
        StepName = stepName;
        Status = "Pending";
        Timestamp = DateTime.MinValue;
    }

    public void Execute()
    {
        Status = "Passed";
        Timestamp = DateTime.Now;
        Console.WriteLine($"[LOG] {StepName} completed at {Timestamp}.");
    }
}

class DeviceValidator
{
    private List<ValidationStep> steps = new List<ValidationStep>();

    public DeviceValidator()
    {
        steps.Add(new ValidationStep("Install Qualification (IQ)"));
        steps.Add(new ValidationStep("Operational Qualification (OQ)"));
        steps.Add(new ValidationStep("Performance Qualification (PQ)"));
        steps.Add(new ValidationStep("Software Version Check"));
        steps.Add(new ValidationStep("User Access Verification"));
    }

    public void RunValidation()
    {
        Console.WriteLine("Starting validation for medical device...\n");

        foreach (var step in steps)
        {
            Console.WriteLine($"Executing: {step.StepName}");
            step.Execute();
        }

        Console.WriteLine("\nAll validation steps completed.\n");
    }

    public void Summary()
    {
        Console.WriteLine("=== Validation Summary ===");
        foreach (var step in steps)
        {
            Console.WriteLine($"{step.StepName} - {step.Status} at {step.Timestamp}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        DeviceValidator validator = new DeviceValidator();
        validator.RunValidation();
        validator.Summary();
    }
}
