using System;
using System.Collections.Generic;
using System.IO;

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

    public void Execute(bool pass = true)
    {
        Status = pass ? "Passed" : "Failed";
        Timestamp = DateTime.Now;
    }

    public string GetLogEntry()
    {
        return $"{Timestamp:u} | {StepName} | {Status}";
    }
}

class DeviceValidator
{
    private List<ValidationStep> steps = new List<ValidationStep>();
    private string logFilePath = "validation_log.txt";

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

        using (StreamWriter writer = new StreamWriter(logFilePath, append: true))
        {
            writer.WriteLine("=== Validation Run: " + DateTime.Now.ToString("u") + " ===");

            foreach (var step in steps)
            {
                Console.WriteLine($"Executing: {step.StepName}");

                // Simulate success/failure based on user input
                Console.Write("Did this step pass? (y/n): ");
                var input = Console.ReadLine();
                bool passed = input?.Trim().ToLower() == "y";

                step.Execute(passed);
                Console.WriteLine(step.GetLogEntry());

                writer.WriteLine(step.GetLogEntry());
            }

            writer.WriteLine();
        }

        Console.WriteLine("\nValidation complete. Log saved to 'validation_log.txt'\n");
    }
}

class Program
{
    static void Main(string[] args)
    {
        DeviceValidator validator = new DeviceValidator();
        validator.RunValidation();
    }
}
