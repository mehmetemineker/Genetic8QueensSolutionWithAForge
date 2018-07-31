using AForge.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic8QueensSolutionWithAForge
{
    class Program
    {
        static void Main(string[] args)
        {
            int populationSize = 140;
            int queensCount = 50;
            var fitnessFunction = new MyProblemFitness();
            var chromosome = new PermutationChromosome(queensCount);
            var selection = new EliteSelection();

            Population population = new Population(populationSize, chromosome, fitnessFunction, selection);

            int i = 1;

            while (true)
            {
                population.RunEpoch();

                ushort[] bestValue = ((PermutationChromosome)population.BestChromosome).Value;

                if (population.BestChromosome.Fitness == queensCount * (queensCount - 1) / 2)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(i + " - F = " + population.BestChromosome.Fitness + " " + string.Join(",", bestValue));
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                }
                else
                {
                    Console.WriteLine(i + " - F = " + population.BestChromosome.Fitness + " " + string.Join(",", bestValue));
                }

                i++;
            }

            Console.ReadLine();
        }
    }

    public class MyProblemChromosome : PermutationChromosome
    {
        public MyProblemChromosome(int length)
            : base(length)
        {

        }
    }

    public class MyProblemFitness : IFitnessFunction
    {
        public double Evaluate(IChromosome chromosome)
        {
            var genes = ((PermutationChromosome)chromosome).Value;

            double result = 0;

            for (int x1 = 0; x1 < genes.Length - 1; x1++)
            {
                int y1 = genes[x1];

                int sagdakiVezirSayisi = genes.Length - 1 - x1;

                for (int x2 = x1 + 1; x2 < genes.Length; x2++)
                {
                    int y2 = genes[x2];

                    if (y1 == y2 || x1 - y1 == x2 - y2 || x1 + y1 == x2 + y2)
                    {
                        sagdakiVezirSayisi -= 1;
                    }
                }

                result += sagdakiVezirSayisi;
            }


            return result;
        }
    }
}
