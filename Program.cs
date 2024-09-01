using System;

class Program
{
    static void Main()
    {
        // Step 1: Define Criteria
        string[] criteria = { "Market Reach", "Cost Efficiency", "Brand Alignment", "Time to Implement" };

        // Step 2: Pairwise Comparison Matrix (Saaty Scale)
        double[,] pairwiseMatrix = {
            { 1, 1.0/3.0, 5, 3 },  // Market Reach
            { 3, 1, 7, 5 },  // Cost Efficiency
            { 1.0/5.0, 1.0/7.0, 1, 1.0/3.0 },  // Brand Alignment
            { 1.0/3.0, 1.0/5.0, 3, 1 }  // Time to Implement
        };

        // Step 3: Calculate Priority Vector (Normalized Eigenvector)
        double[] priorityVector = CalculatePriorityVector(pairwiseMatrix, criteria.Length);

        // Step 4: Display the priority vector
        Console.WriteLine("Priority Vector:");
        for (int i = 0; i < criteria.Length; i++)
        {
            Console.WriteLine($"{criteria[i]}: {priorityVector[i]:F4}");
        }

        // Step 5: Perform Marketing Strategy Evaluation based on weighted criteria
        // Example scores for three marketing strategies on the criteria
        double[] strategy1Scores = { 0.8, 0.6, 0.7, 0.9 };
        double[] strategy2Scores = { 0.9, 0.7, 0.8, 0.6 };
        double[] strategy3Scores = { 0.7, 0.8, 0.6, 0.7 };

        double strategy1FinalScore = CalculateFinalScore(strategy1Scores, priorityVector);
        double strategy2FinalScore = CalculateFinalScore(strategy2Scores, priorityVector);
        double strategy3FinalScore = CalculateFinalScore(strategy3Scores, priorityVector);

        Console.WriteLine($"\nStrategy 1 Final Score: {strategy1FinalScore:F4}");
        Console.WriteLine($"Strategy 2 Final Score: {strategy2FinalScore:F4}");
        Console.WriteLine($"Strategy 3 Final Score: {strategy3FinalScore:F4}");

        // Step 6: Determine the best marketing strategy
        string bestStrategy = DetermineBestStrategy(strategy1FinalScore, strategy2FinalScore, strategy3FinalScore);
        Console.WriteLine($"\nThe best marketing strategy to implement is: {bestStrategy}");
    }

    static double[] CalculatePriorityVector(double[,] matrix, int size)
    {
        double[] sumOfColumns = new double[size];
        double[] normalizedMatrixSum = new double[size];
        double[] priorityVector = new double[size];

        // Calculate the sum of each column
        for (int j = 0; j < size; j++)
        {
            for (int i = 0; i < size; i++)
            {
                sumOfColumns[j] += matrix[i, j];
            }
        }

        // Normalize the matrix
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                normalizedMatrixSum[i] += (matrix[i, j] / sumOfColumns[j]);
            }
            // Calculate the priority vector by averaging the rows of the normalized matrix
            priorityVector[i] = normalizedMatrixSum[i] / size;
        }

        return priorityVector;
    }

    static double CalculateFinalScore(double[] scores, double[] priorityVector)
    {
        double finalScore = 0.0;
        for (int i = 0; i < scores.Length; i++)
        {
            finalScore += scores[i] * priorityVector[i];
        }
        return finalScore;
    }

    static string DetermineBestStrategy(double strategy1Score, double strategy2Score, double strategy3Score)
    {
        if (strategy1Score > strategy2Score && strategy1Score > strategy3Score)
        {
            return "Strategy 1";
        }
        else if (strategy2Score > strategy1Score && strategy2Score > strategy3Score)
        {
            return "Strategy 2";
        }
        else
        {
            return "Strategy 3";
        }
    }
}